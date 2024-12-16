using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para frmPacientes.xaml
    /// </summary>
    public partial class frmPacientes : Window
    {
        public frmPacientes()
        {
            InitializeComponent();
            cargarfolio();
        }
        Clases.Conexion c;
        Clases.ClPacientes G;
        public void cargarfolio()
        {
            string query = "SELECT MAX(ID_PACIENTE)+1 AS FOLIO FROM REGISTRO_DE_PACIENTE;";
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
            foreach (var control in GridPaciente.Children)
            {
                if (control is TextBox textBox)
                    textBox.Text = string.Empty;
                if (control is DatePicker datePicker)
                    datePicker.SelectedDate = null;
            }
        }
        private void buscar()
        {
            string query = "SELECT * FROM REGISTRO_DE_PACIENTE WHERE ID_PACIENTE = @ID_PACIENTE";
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                SqlCommand cmd = new SqlCommand(query, conn); //pa hacer crud
                cmd.Parameters.AddWithValue("@ID_PACIENTE", txtID.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        // Asignar valores a las propiedades de la clase
                        txtID.Text = reader["ID_PACIENTE"].ToString();
                        txtNombre.Text = reader["NOMBRE"].ToString();
                        txtIDApellidoPa.Text = reader["APELLIDO_PA"].ToString();
                        txtApellidoMA.Text = reader["APELLIDO_MA"].ToString();
                        DPFechaDeNacimiento.Text = reader["FECHA_DE_NACIMIENTO"].ToString();
                        txtCorreo.Text = reader["CORREO_ELECTRONICO"].ToString();
                        txtDireccion.Text = reader["DIRECCION"].ToString();
                        txtContacto.Text = reader["CONTACTO"].ToString();
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
                // Validar correo electrónico
                if (!IsValidEmail(txtCorreo.Text))
                {
                    MessageBox.Show("Por favor ingrese un correo electrónico válido.");
                    return;  // No continuar si el correo no es válido
                }

                DateTime fechaNacimiento = DPFechaDeNacimiento.SelectedDate.Value;
                Clases.ClPacientes B = new Clases.ClPacientes(byte.Parse(txtID.Text));
                DataSet ds = new DataSet();
                Clases.Conexion c = new Clases.Conexion(B.consultar());
                ds = c.consultar();
                G = new Clases.ClPacientes(byte.Parse(txtID.Text), txtNombre.Text, txtIDApellidoPa.Text, txtApellidoMA.Text, fechaNacimiento, txtCorreo.Text, txtDireccion.Text, txtContacto.Text);
                c = new Clases.Conexion();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    MessageBox.Show(c.EJECUTAR(G.modificar(),G.ID_PACIENTE1, G.NOMBRE1, G.APELLIDO_PA1, G.APELLIDO_MA1, G.FECHA_DE_NACIMIENTO1, G.CORREO_ELECTRONICO1, G.DIRECCION1, G.CONTACTO1));
                    LimpiarFormulario();
                    cargarfolio();
                }

                else
                {

                    MessageBox.Show(c.EJECUTAR(G.grabar(), G.ID_PACIENTE1, G.NOMBRE1, G.APELLIDO_PA1, G.APELLIDO_MA1, G.FECHA_DE_NACIMIENTO1, G.CORREO_ELECTRONICO1, G.DIRECCION1, G.CONTACTO1));
                    LimpiarFormulario();
                    cargarfolio();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
        private void btngrabar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
            cargarfolio();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Clases.ClPacientes categ = new Clases.ClPacientes();
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

       /* private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea borrar el registro?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Verificar la respuesta del usuario

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Clases.ClPacientes B = new Clases.ClPacientes(byte.Parse(txtID.Text));
                    DataSet ds = new DataSet();
                    Clases.Conexion c = new Clases.Conexion(B.consultar());
                    ds = c.consultar();
                    G = new Clases.ClPacientes(byte.Parse(txtID.Text));
                    c = new Clases.Conexion();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        MessageBox.Show(c.EJECUTAR(G.borrar(), B.ID_PACIENTE1), "BORRAR");
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

        private void MIInicio_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtContacto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            {
                graba();
            }
        }
    }
}
