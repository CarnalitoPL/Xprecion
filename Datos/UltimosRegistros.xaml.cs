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

namespace Xprecion.Datos
{
    /// <summary>
    /// Lógica de interacción para UltimosRegistros.xaml
    /// </summary>
    public partial class UltimosRegistros : Window
    {
       

        public UltimosRegistros()
        {
            InitializeComponent();
        }

        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "MEDICO"
            CargarDatos("SELECT * FROM MEDICO");
        }

        private void btnAreasMedicas_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "AREAS_MEDICAS"
            CargarDatos("SELECT * FROM AREA_MEDICo");
        }

        private void btnRayosX_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "TIPOS_RAYOS_X"
            CargarDatos("SELECT * FROM TIPO_DE_RAYOS_X");
        }

        private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "PACIENTES"
            CargarDatos("SELECT * FROM REGISTRO_DE_PACIENTE");
        }

        private void CargarDatos(string consulta)
        {
            try
            {
                // Crear instancia de la clase Conexion
                Conexion conexion = new Conexion();

                // Establecer la consulta SQL a ejecutar
                conexion.sentencia1 = consulta;

                // Ejecutar la consulta y obtener los datos en un DataSet
                DataSet ds = conexion.consultar();

                // Asignar el DataSet al DataGrid
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    dgRegistros.ItemsSource = ds.Tables["Tabla"].DefaultView; // Esto cargará los registros de la tabla en el DataGrid.
                }
                else
                {
                    MessageBox.Show("No se encontraron registros.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
