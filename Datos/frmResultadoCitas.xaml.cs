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
    /// Lógica de interacción para frmResultadoCitas.xaml
    /// </summary>
    public partial class frmResultadoCitas : Window
    {
        public frmResultadoCitas()
        {
            InitializeComponent();
        }
        Clases.Conexion c;
        Clases.ClResultadoCitas G;
        public void cargarfolio()
        {
            string query = "SELECT MAX(ID_RESULTADOCITA)+1 AS FOLIO FROM RESULTADO_CITAS;";
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
            foreach (var control in GridResultado.Children)
            {
                if (control is TextBox textBox)
                    textBox.Text = string.Empty;
            }
        }
        private void buscar()
        {
            string query = "SELECT * FROM vta_resultado WHERE ID_RESULTADOCITA = @ID_RESULTADOCITA";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn); //pa hacer crud
                cmd.Parameters.AddWithValue("@ID_RESULTADOCITA", txtID.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        // Asignar valores a las propiedades de la clase
                        txtID.Text = reader["ID_RESULTADOCITA"].ToString();
                        txtIDCita.Text = reader["ID_CITAS"].ToString();
                        txtFecha.Text = reader["FECHA"].ToString();
                        txtNombrePaciente.Text = reader["NOMBRE"].ToString();
                        txtComentario.Text = reader["COMENTARIOFINAL"].ToString();
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
        private void buscarcitas()
        {
            string query = "SELECT * FROM vta_buscarcita WHERE ID_CITAS = @ID_CITAS";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_CITAS", txtIDCita.Text);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    // Asignar valores a las propiedades de la clase
                    txtIDCita.Text = reader["ID_CITAS"].ToString();
                    txtFecha.Text = reader["FECHA"].ToString();
                    txtNombrePaciente.Text = reader["NOMBRE"].ToString();

                }
                else MessageBox.Show("No existe el medico");
                reader.Close();
            }
        }
        private void graba()
        {
            try
            {
                Clases.ClResultadoCitas B = new Clases.ClResultadoCitas(byte.Parse(txtID.Text));
                DataSet ds = new DataSet();
                Clases.Conexion c = new Clases.Conexion(B.consultar());
                ds = c.consultar();
                G = new Clases.ClResultadoCitas(byte.Parse(txtID.Text), txtComentario.Text, (byte.Parse(txtIDCita.Text)));
                c = new Clases.Conexion();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    MessageBox.Show(c.EJECUTAR(G.modificar(), G.ID_RESULTADOCITA1, G.COMENTARIOFINAL1, G.ID_CITAS1));
                    LimpiarFormulario();
                    cargarfolio();
                }

                else
                {

                    MessageBox.Show(c.EJECUTAR(G.grabar(), G.ID_RESULTADOCITA1, G.COMENTARIOFINAL1, G.ID_CITAS1));
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
            cargarfolio();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            graba();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClResultadoCitas categ = new Clases.ClResultadoCitas();
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

        private void btnBuscarCIta_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClCitas categ = new Clases.ClCitas();
            Clases.Conexion con = new Clases.Conexion();
            if (con.Execute(categ.buscarcita(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    txtIDCita.Text = con.FieldValue;
                    buscarcitas();
                }
            }
        }

        private void MiInicio_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
