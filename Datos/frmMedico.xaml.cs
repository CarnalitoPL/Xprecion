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
using static System.Windows.Forms.MonthCalendar;

namespace Xprecion.Datos
{
    /// <summary>
    /// Lógica de interacción para frmMedico.xaml
    /// </summary>
    public partial class frmMedico : Window
    {
        public frmMedico()
        {
            InitializeComponent();
            cargarregion();
            cargarfolio();
        }
        Clases.Conexion c;
        Clases.ClMedico G;
        public void cargarfolio()
        {
            string query = "SELECT MAX(ID_MEDICO)+1 AS FOLIO FROM MEDICO;";
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
        private void cargarregion()
        {
            DataSet ds = new DataSet();
            Clases.ClAreaMedica s = new Clases.ClAreaMedica();
            c = new Clases.Conexion(s.buscartodos());
            ds = c.consultar();
            //
            CBArea.ItemsSource = ds.Tables[0].DefaultView;
            //
            CBArea.DisplayMemberPath = ds.Tables[0].Columns["AREA"].ToString();
            //
            CBArea.SelectedValuePath = ds.Tables[0].Columns["ID_AREA"].ToString();

        }
        private void LimpiarFormulario()
        {
            foreach (var control in jeje.Children)
            {
                if (control is TextBox textBox)
                    textBox.Text = string.Empty;
                else if (control is ComboBox comboBox)
                    comboBox.SelectedIndex = -1;
            }
        }
        private void buscar()
        {
            string query = "SELECT * FROM vta_medico_areamedico WHERE ID_MEDICO = @ID_MEDICO";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn); //pa hacer crud
                cmd.Parameters.AddWithValue("@ID_MEDICO", txtID.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        // Asignar valores a las propiedades de la clase
                        txtID.Text = reader["ID_MEDICO"].ToString();
                        txtNombre.Text = reader["NOMBRE"].ToString();
                        txtIDApellidoPa.Text = reader["APELLIDO_PA"].ToString();
                        txtApellidoMA.Text = reader["APELLIDO_MA"].ToString();
                        CBArea.Text = reader["AREA"].ToString();
                    }
                    else MessageBox.Show("No existe el area");
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        private void graba()
        {
            try
            {
                int idarea = int.Parse(CBArea.SelectedValue.ToString());
                Clases.ClMedico B = new Clases.ClMedico(byte.Parse(txtID.Text));
                DataSet ds = new DataSet();
                Clases.Conexion c = new Clases.Conexion(B.consultar());
                ds = c.consultar();
                G = new Clases.ClMedico(byte.Parse(txtID.Text), txtNombre.Text, txtIDApellidoPa.Text, txtApellidoMA.Text,idarea);
                c = new Clases.Conexion();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    MessageBox.Show(c.EJECUTAR(G.modificar(), G.ID_MEDICO1, G.NOMBRE1, G.APELLIDO_PA1, G.APELLIDO_MA1, G.ID_AREA1));
                    LimpiarFormulario();
                    cargarfolio();
                }

                else
                {

                    MessageBox.Show(c.EJECUTAR(G.grabar(), G.ID_MEDICO1, G.NOMBRE1, G.APELLIDO_PA1, G.APELLIDO_MA1, G.ID_AREA1));
                    LimpiarFormulario();
                    cargarfolio();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClMedico categ = new Clases.ClMedico();
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
            MessageBoxResult result = MessageBox.Show("Desea borrar el registro?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Verificar la respuesta del usuario

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Clases.ClMedico B = new Clases.ClMedico(byte.Parse(txtID.Text));
                    DataSet ds = new DataSet();
                    Clases.Conexion c = new Clases.Conexion(B.consultar());
                    ds = c.consultar();
                    G = new Clases.ClMedico(byte.Parse(txtID.Text));
                    c = new Clases.Conexion();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        MessageBox.Show(c.EJECUTAR(G.borrar(), B.ID_MEDICO1), "BORRAR");
                        LimpiarFormulario();
                        cargarfolio();
                    }

                    else
                    {
                        MessageBox.Show("No se encontro el registro");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }

            }

            else
            {
                // El usuario seleccionó "No".
                MessageBox.Show("Borrado de registro cancelado");
            }
        }

        private void btngrabar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
            cargarfolio();
        }

        private void MiInicio_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            {
                graba();
            }
        }
    }
}
