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
    /// Lógica de interacción para frmTipoDeRayosX.xaml
    /// </summary>
    public partial class frmTipoDeRayosX : Window
    {
        public frmTipoDeRayosX()
        {
            InitializeComponent();
            cargarfolio();
        }
        Clases.ClTipoDeRayosX G;
        public void cargarfolio()
        {
            string query = "SELECT MAX(ID_TIPO_RAYOS_X)+1 AS FOLIO FROM TIPO_DE_RAYOS_X;";
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
        private void buscar()
        {
            string query = "SELECT * FROM TIPO_DE_RAYOS_X WHERE ID_TIPO_RAYOS_X = @ID_TIPO_RAYOS_X";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn); //pa hacer crud
                cmd.Parameters.AddWithValue("@ID_TIPO_RAYOS_X", txtID.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        // Asignar valores a las propiedades de la clase
                        txtID.Text = reader["ID_TIPO_RAYOS_X"].ToString();
                        txtPrecio.Text = reader["PRECIOSERVICIO"].ToString();
                        txtRayosX.Text = reader["SERVICIO"].ToString();
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
                Clases.ClTipoDeRayosX B = new Clases.ClTipoDeRayosX(byte.Parse(txtID.Text));
                DataSet ds = new DataSet();
                Clases.Conexion c = new Clases.Conexion(B.consultar());
                ds = c.consultar();
                G = new Clases.ClTipoDeRayosX(byte.Parse(txtID.Text), float.Parse(txtPrecio.Text), txtRayosX.Text );
                c = new Clases.Conexion();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    MessageBox.Show(c.EJECUTAR(G.modificar(),G.ID_TIPO_RAYOS_X1, G.PRECIOSERVICIO1, G.SERVICIO1));
                }

                else
                {

                    MessageBox.Show(c.EJECUTAR(G.grabar(), G.PRECIOSERVICIO1, G.SERVICIO1));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
        private void btngrabar_Click(object sender, RoutedEventArgs e)
        {
            {
                graba();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClTipoDeRayosX categ = new Clases.ClTipoDeRayosX();
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
                    Clases.ClTipoDeRayosX B = new Clases.ClTipoDeRayosX(byte.Parse(txtID.Text));
                    DataSet ds = new DataSet();
                    Clases.Conexion c = new Clases.Conexion(B.consultar());
                    ds = c.consultar();
                    G = new Clases.ClTipoDeRayosX(byte.Parse(txtID.Text));
                    c = new Clases.Conexion();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        MessageBox.Show(c.EJECUTAR(G.borrar(), B.ID_TIPO_RAYOS_X1), "BORRAR");
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
    }
}
