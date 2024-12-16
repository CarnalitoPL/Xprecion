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
using static Xprecion.Clases.ClGlobales;

namespace Xprecion.Datos
{
    /// <summary>
    /// Lógica de interacción para Area_medica.xaml
    /// </summary>
    public partial class Area_medica : Window
    {
        public Area_medica()
        {
            InitializeComponent();
            cargarfolio();
        }
        Clases.ClAreaMedica G;
        public void cargarfolio()
        {
            string query = "SELECT MAX(ID_AREA)+1 AS FOLIO FROM AREA_MEDICO;";
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
        private void LimpiarFormulario()
        {
            foreach (var control in GridAreaMedica.Children)
            {
                if (control is TextBox textBox)
                    textBox.Text = string.Empty;
            }
        }
        private void buscar()
        {
            string query = "SELECT * FROM AREA_MEDICO WHERE ID_AREA = @ID_AREA";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn); //pa hacer crud
                cmd.Parameters.AddWithValue("@ID_AREA", txtID.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        // Asignar valores a las propiedades de la clase
                        txtID.Text = reader["ID_AREA"].ToString();
                        txtArea.Text = reader["AREA"].ToString();
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
                Clases.ClAreaMedica B = new Clases.ClAreaMedica(byte.Parse(txtID.Text));
                DataSet ds = new DataSet();
                Clases.Conexion c = new Clases.Conexion(B.consultar());
                ds = c.consultar();
                G = new Clases.ClAreaMedica(byte.Parse(txtID.Text), txtArea.Text);
                c = new Clases.Conexion();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    MessageBox.Show(c.EJECUTAR(G.modificar(), G.ID_AREA1, G.AREA1));
                    LimpiarFormulario();
                    cargarfolio();
                }

                else
                {

                    MessageBox.Show(c.EJECUTAR(G.grabar(), G.AREA1));
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
            Clases.ClAreaMedica categ = new Clases.ClAreaMedica();
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

        private void btngrabar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
            cargarfolio();
        }

      /*  private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea borrar el registro?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Verificar la respuesta del usuario

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Clases.ClAreaMedica B = new Clases.ClAreaMedica(byte.Parse(txtID.Text));
                    DataSet ds = new DataSet();
                    Clases.Conexion c = new Clases.Conexion(B.consultar());
                    ds = c.consultar();
                    G = new Clases.ClAreaMedica(byte.Parse(txtID.Text));
                    c = new Clases.Conexion();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        MessageBox.Show(c.EJECUTAR(G.borrar(), B.ID_AREA1), "BORRAR");
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
        }*/

        private void MiInicio_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            {
                graba();
            }
        }
    }
}
