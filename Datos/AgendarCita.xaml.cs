using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xprecion.Clases;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Xprecion.Datos
{
    /// <summary>
    /// Lógica de interacción para AgendarCita.xaml
    /// </summary>
    public partial class AgendarCita : Window
    {
        public AgendarCita()
        {
            InitializeComponent();
            cargarfolio();
            CargarRayos();

        }
        Clases.Conexion c;
        Clases.ClCitas G;
        public void cargarfolio()
        {
            string query = "SELECT MAX(ID_CITAS)+1 AS FOLIO FROM CITAS;";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    // Asignar valores a las propiedades de la clase
                    txtID.Text = reader["Folio"].ToString();
                }
                reader.Close();
            }
        }
        private void buscarMedico()
        {
            string query = "SELECT * FROM MEDICO WHERE ID_MEDICO = @ID_MEDICO";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_MEDICO", txtIDMedico.Text);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    // Asignar valores a las propiedades de la clase
                    txtIDMedico.Text = reader["ID_MEDICO"].ToString();
                    txtNombreMedico.Text = reader["NOMBRE"].ToString();

                }
                else MessageBox.Show("No existe el medico");
                reader.Close();
            }
        }
        private void buscar()
        {
            string query = @"
        SELECT 
            vta_citas.ID_CITAS, 
            vta_citas.FECHA, 
            vta_citas.HORA, 
            vta_citas.ESTADOCITA, 
            vta_citas.COMENTARIO, 
            vta_citas.SERVICIO, 
            vta_citas.ID_MEDICO AS ID_MEDICO_CITA, 
            MEDICO.NOMBRE AS NOMBRE_MEDICO, 
            vta_citas.ID_PACIENTE AS ID_PACIENTE_CITA, 
            REGISTRO_DE_PACIENTE.NOMBRE AS NOMBRE_PACIENTE
        FROM 
            vta_citas
        LEFT JOIN MEDICO ON vta_citas.ID_MEDICO = MEDICO.ID_MEDICO
        LEFT JOIN REGISTRO_DE_PACIENTE ON vta_citas.ID_PACIENTE = REGISTRO_DE_PACIENTE.ID_PACIENTE
        WHERE 
            vta_citas.ID_CITAS = @ID_CITAS";

            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_CITAS", txtID.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        txtID.Text = reader["ID_CITAS"].ToString();
                        DPFecha.Text = reader["FECHA"].ToString();
                        CBHora.Text = reader["HORA"].ToString();
                        ComboBEstado.IsChecked = Convert.ToBoolean(reader["ESTADOCITA"]);
                        txtComentario.Text = reader["COMENTARIO"].ToString();
                        CBTipoDeRayos.Text = reader["SERVICIO"].ToString();
                        txtIDMedico.Text = reader["ID_MEDICO_CITA"].ToString();
                        txtNombreMedico.Text = reader["NOMBRE_MEDICO"].ToString();
                        txtIDPaciente.Text = reader["ID_PACIENTE_CITA"].ToString();
                        txtNombrePaciente.Text = reader["NOMBRE_PACIENTE"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No existe el área");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void buscarPaciente()
        {
            string query = "SELECT * FROM REGISTRO_DE_PACIENTE WHERE ID_PACIENTE = @ID_PACIENTE";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_PACIENTE", txtIDPaciente.Text);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    // Asignar valores a las propiedades de la clase
                    txtIDPaciente.Text = reader["ID_PACIENTE"].ToString();
                    txtNombrePaciente.Text = reader["NOMBRE"].ToString();

                }
                else MessageBox.Show("No existe el medico");
                reader.Close();
            }
        }
        private void CargarRayos()
        {
            DataSet ds = new DataSet();
            Clases.ClTipoDeRayosX s = new Clases.ClTipoDeRayosX();
            c = new Clases.Conexion(s.buscartodos());
            ds = c.consultar();
            //
            CBTipoDeRayos.ItemsSource = ds.Tables[0].DefaultView;
            //
            CBTipoDeRayos.DisplayMemberPath = ds.Tables[0].Columns["SERVICIO"].ToString();
            //
            CBTipoDeRayos.SelectedValuePath = ds.Tables[0].Columns["ID_TIPO_RAYOS_X"].ToString();

        }
        private void LimpiarFormulario()
        {
            foreach (var control in GridAgendarCita.Children)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                else if (control is DatePicker datePicker)
                {
                    datePicker.SelectedDate = null;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.IsChecked = false;
                }
            }
        }
        private void graba()
        {
            try
            {
                bool checkbox = ComboBEstado.IsChecked ?? false;
                int idtipoderayos = int.Parse(CBTipoDeRayos.SelectedValue.ToString());
                DateTime fecha = DPFecha.SelectedDate.Value;
                Clases.ClCitas B = new Clases.ClCitas(byte.Parse(txtID.Text));
                DataSet ds = new DataSet();
                Clases.Conexion c = new Clases.Conexion(B.consultar());
                ds = c.consultar();
                G = new Clases.ClCitas(byte.Parse(txtID.Text), fecha, CBHora.Text, checkbox, txtComentario.Text, idtipoderayos, byte.Parse(txtIDMedico.Text), byte.Parse(txtIDPaciente.Text));
                c = new Clases.Conexion();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    MessageBox.Show(c.EJECUTAR(G.modificar(), G.ID_CITAS1, G.FECHA1, G.HORA1, G.ESTADOCITA1, G.COMENTARIO1, G.ID_TIPO_DE_RAYOS_X1, G.ID_MEDICO1, G.ID_PACIENTE1));
                    LimpiarFormulario();
                    cargarfolio();
                }

                else
                {

                    MessageBox.Show(c.EJECUTAR(G.grabar(), G.ID_CITAS1, G.FECHA1, G.HORA1, G.ESTADOCITA1, G.COMENTARIO1, G.ID_TIPO_DE_RAYOS_X1, G.ID_MEDICO1, G.ID_PACIENTE1));
                    LimpiarFormulario();
                    cargarfolio();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
        private void btngrabar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            graba();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClCitas categ = new Clases.ClCitas();
            Clases.Conexion con = new Clases.Conexion();
            if (con.Execute(categ.buscartodos(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    txtID.Text = con.FieldValue;
                    buscar();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MiInicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscarMedico_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClMedico emp = new Clases.ClMedico();
            Clases.Conexion con = new Clases.Conexion();
            if (con.Execute(emp.buscarMedico(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    txtIDMedico.Text = con.FieldValue;
                    buscarMedico();
                }
            }
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClPacientes emp = new Clases.ClPacientes();
            Clases.Conexion con = new Clases.Conexion();
            if (con.Execute(emp.buscarPaciente(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    txtIDPaciente.Text = con.FieldValue;
                    buscarPaciente();
                }
            }
        }

        private void txtComentario_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
