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
            CargarRayos();

        }
        Clases.Conexion c;

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
        private void btngrabar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

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
