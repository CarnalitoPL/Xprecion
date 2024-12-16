using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
using PdfSharp.Pdf;
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
        private string seccionActual = string.Empty;
        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "MEDICO"
            seccionActual = "Medicos";
            CargarDatos("SELECT * FROM vta_medico_areamedico");
        }

        private void btnAreasMedicas_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "AREAS_MEDICAS"
            seccionActual = "AreasMedicas";
            CargarDatos("SELECT * FROM AREA_MEDICo");
        }

        private void btnRayosX_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "TIPOS_RAYOS_X"
            seccionActual = "RayosX";
            CargarDatos("SELECT * FROM TIPO_DE_RAYOS_X");
        }

        private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cargar los registros de la tabla "PACIENTES"
            seccionActual = "Pacientes";
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
        private void ImprimirMedicos()
        {
            // Establece la conexión con la base de datos
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                conn.Open();
                string query = "SELECT * FROM vta_medico_areamedico";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crea el documento PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Listado de Médicos y Áreas Médicas";

                // Crea una página en orientación horizontal (landscape)
                PdfPage page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;  // Establece la orientación a horizontal
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Fuente para el texto
                XFont headerFont = new XFont("Arial", 16);  // Encabezado con tamaño grande
                XFont normalFont = new XFont("Arial", 12);  // Texto normal

                // Posición inicial para el encabezado
                int yPosition = 40;

                // Título centrado
                gfx.DrawString("Listado de Médicos", headerFont, XBrushes.DarkBlue, new XRect(0, yPosition, page.Width, page.Height), XStringFormats.TopCenter);
                yPosition += 40;

                // Línea separadora
                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 20;

                // Ancho de las celdas
                int cellWidth = 80;  // Ancho de la celda para "ID MEDICO"
                int nombreCellWidth = 120;  // Ancho de la celda para "Nombre"
                int apellidoPaCellWidth = 140;  // Ancho de la celda para "Apellido Paterno"
                int apellidoMaCellWidth = 160;  // Ancho de la celda para "Apellido Materno"
                int areaIdCellWidth = 50;  // Ancho de la celda para "ID AREA"
                int areaCellWidth = 120;  // Ancho de la celda para "Area"

                // Encabezado de la tabla
                gfx.DrawString("ID MEDICO", normalFont, XBrushes.Black, 40, yPosition);
                gfx.DrawString("Nombre", normalFont, XBrushes.Black, 180, yPosition);
                gfx.DrawString("Apellido Paterno", normalFont, XBrushes.Black, 300, yPosition);
                gfx.DrawString("Apellido Materno", normalFont, XBrushes.Black, 460, yPosition);
                gfx.DrawString("ID AREA", normalFont, XBrushes.Black, 620, yPosition);
                gfx.DrawString("Area", normalFont, XBrushes.Black, 750, yPosition);
                yPosition += 20;

                // Dibuja una línea debajo de los encabezados de la tabla
                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 10;

                // Escribir los datos de la tabla con bordes
                while (reader.Read())
                {
                    // Ajustamos el tamaño de las celdas y la posición del texto para centrarlo
                    int cellHeight = 20;
                    int padding = 5; // Espacio entre el texto y el borde de la celda

                    // Dibuja las celdas
                    gfx.DrawRectangle(XPens.Black, 40, yPosition, cellWidth, cellHeight); // Celda de ID MEDICO
                    gfx.DrawString(reader["ID_MEDICO"].ToString(), normalFont, XBrushes.Black, new XRect(40 + padding, yPosition, cellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 180, yPosition, nombreCellWidth, cellHeight); // Celda de Nombre
                    gfx.DrawString(reader["NOMBRE"].ToString(), normalFont, XBrushes.Black, new XRect(180 + padding, yPosition, nombreCellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 300, yPosition, apellidoPaCellWidth, cellHeight); // Celda de Apellido Paterno
                    gfx.DrawString(reader["APELLIDO_PA"].ToString(), normalFont, XBrushes.Black, new XRect(300 + padding, yPosition, apellidoPaCellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 460, yPosition, apellidoMaCellWidth, cellHeight); // Celda de Apellido Materno
                    gfx.DrawString(reader["APELLIDO_MA"].ToString(), normalFont, XBrushes.Black, new XRect(460 + padding, yPosition, apellidoMaCellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 620, yPosition, areaIdCellWidth, cellHeight); // Celda de ID AREA
                    gfx.DrawString(reader["ID_AREA"].ToString(), normalFont, XBrushes.Black, new XRect(620 + padding, yPosition, areaIdCellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 700, yPosition, areaCellWidth, cellHeight); // Celda de Area
                    gfx.DrawString(reader["AREA"].ToString(), normalFont, XBrushes.Black, new XRect(700 + padding, yPosition, areaCellWidth - padding, cellHeight), XStringFormats.Center);

                    yPosition += cellHeight + 5; // Ajuste del espacio entre filas

                    // Verifica si se ha alcanzado el final de la página
                    if (yPosition > page.Height - 40)
                    {
                        page = document.AddPage(); // Agrega una nueva página
                        page.Orientation = PdfSharp.PageOrientation.Landscape; // Mantiene la orientación horizontal
                        gfx = XGraphics.FromPdfPage(page); // Nueva instancia de XGraphics para la nueva página
                        yPosition = 40; // Reinicia la posición Y
                    }
                }

                // Guardar el documento
                string filePath = "vta_medico_areamedico.pdf";
                document.Save(filePath);

                // Cerrar el lector
                reader.Close();

                // Abrir el PDF automáticamente
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private void ImprimirAreasMedicas()
        {
            // Establece la conexión con la base de datos
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                conn.Open();
                string query = "SELECT * FROM AREA_MEDICO";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crea el documento PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Listado de Areas Médicas";

                // Crea una página
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Fuente para el texto
                XFont headerFont = new XFont("Arial", 16);  // Encabezado con tamaño grande
                XFont normalFont = new XFont("Arial", 12);  // Texto normal

                // Posición inicial para el encabezado
                int yPosition = 40;

                // Título centrado
                gfx.DrawString("Listado de Areas Médicas", headerFont, XBrushes.DarkBlue, new XRect(0, yPosition, page.Width, page.Height), XStringFormats.TopCenter);
                yPosition += 40;

                // Línea separadora
                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 20;

                // Encabezado de la tabla
                gfx.DrawString("ID AREA", normalFont, XBrushes.Black, 40, yPosition);
                gfx.DrawString("Area", normalFont, XBrushes.Black, 150, yPosition);
                yPosition += 20;

                // Dibuja una línea debajo de los encabezados de la tabla
                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 10;

                // Escribir los datos de la tabla con bordes
                while (reader.Read())
                {
                    // Ajustamos el tamaño de las celdas y la posición del texto para centrarlo
                    int cellWidth = 100; // Ancho de la celda para "ID AREA"

                    // Ajusta el ancho de la celda "Area" para que no se salga de la página
                    int availableWidth = (int)(page.Width - 40 - cellWidth - 20);  // Resta un margen extra para evitar desbordes
                    int areaCellWidth = availableWidth;

                    int cellHeight = 20;
                    int padding = 5; // Espacio entre el texto y el borde de la celda

                    // Dibuja las celdas
                    gfx.DrawRectangle(XPens.Black, 40, yPosition, cellWidth, cellHeight); // Celda de ID AREA
                    gfx.DrawString(reader["ID_AREA"].ToString(), normalFont, XBrushes.Black, new XRect(40 + padding, yPosition, cellWidth, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 150, yPosition, areaCellWidth, cellHeight); // Celda de Area (ajustada)
                    gfx.DrawString(reader["AREA"].ToString(), normalFont, XBrushes.Black, new XRect(150 + padding, yPosition, areaCellWidth, cellHeight), XStringFormats.Center);

                    yPosition += cellHeight + 5; // Ajuste del espacio entre filas

                    // Verifica si se ha alcanzado el final de la página
                    if (yPosition > page.Height - 40)
                    {
                        page = document.AddPage(); // Agrega una nueva página
                        gfx = XGraphics.FromPdfPage(page); // Nueva instancia de XGraphics para la nueva página
                        yPosition = 40; // Reinicia la posición Y
                    }
                }

                // Guardar el documento
                string filePath = "areamedicos.pdf";
                document.Save(filePath);

                // Cerrar el lector
                reader.Close();

                // Abrir el PDF automáticamente
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private void ImprimirRayosX()
        {
            // Establece la conexión con la base de datos
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                conn.Open();
                string query = "SELECT * FROM TIPO_DE_RAYOS_X";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crea el documento PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Listado de Tipos de Rayos X";

                // Crea una página en orientación horizontal (landscape)
                PdfPage page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Fuente para el texto
                XFont headerFont = new XFont("Arial", 16, XFontStyleEx.Bold); // Negrita
                XFont normalFont = new XFont("Arial", 12); // Regular

                // Posición inicial para el encabezado
                int yPosition = 40;

                // Título centrado
                gfx.DrawString("Listado de Tipos de Rayos X", headerFont, XBrushes.DarkBlue, new XRect(0, yPosition, page.Width, page.Height), XStringFormats.TopCenter);
                yPosition += 40;

                // Línea separadora
                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 20;

                // Ancho de las celdas
                int idCellWidth = 100;         // Ancho para "ID_TIPO_DE_RAYOS_X"
                int precioCellWidth = 150;     // Ancho para "PRECIOSERVICIO"
                int servicioCellWidth = 300;   // Ancho para "SERVICIO"

                // Encabezado de la tabla
                gfx.DrawString("ID", normalFont, XBrushes.Black, 85, yPosition);
                gfx.DrawString("Precio del Servicio", normalFont, XBrushes.Black, 180, yPosition);
                gfx.DrawString("Servicio", normalFont, XBrushes.Black, 450, yPosition);
                yPosition += 20;

                // Dibuja una línea debajo de los encabezados de la tabla
                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 10;

                // Escribir los datos de la tabla con bordes
                while (reader.Read())
                {
                    // Ajustamos el tamaño de las celdas y la posición del texto para centrarlo
                    int cellHeight = 20;
                    int padding = 5; // Espacio entre el texto y el borde de la celda

                    // Dibuja las celdas
                    gfx.DrawRectangle(XPens.Black, 40, yPosition, idCellWidth, cellHeight); // Celda de ID_TIPO_DE_RAYOS_X
                    gfx.DrawString(reader["ID_TIPO_RAYOS_X"].ToString(), normalFont, XBrushes.Black, new XRect(40 + padding, yPosition, idCellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 160, yPosition, precioCellWidth, cellHeight); // Celda de PRECIOSERVICIO
                    gfx.DrawString(reader["PRECIOSERVICIO"].ToString(), normalFont, XBrushes.Black, new XRect(160 + padding, yPosition, precioCellWidth - padding, cellHeight), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 320, yPosition, servicioCellWidth, cellHeight); // Celda de SERVICIO
                    gfx.DrawString(reader["SERVICIO"].ToString(), normalFont, XBrushes.Black, new XRect(320 + padding, yPosition, servicioCellWidth - padding, cellHeight), XStringFormats.Center);

                    yPosition += cellHeight + 5; // Ajuste del espacio entre filas

                    // Verifica si se ha alcanzado el final de la página
                    if (yPosition > page.Height - 40)
                    {
                        page = document.AddPage(); // Agrega una nueva página
                        page.Orientation = PdfSharp.PageOrientation.Landscape; // Mantiene la orientación horizontal
                        gfx = XGraphics.FromPdfPage(page); // Nueva instancia de XGraphics para la nueva página
                        yPosition = 40; // Reinicia la posición Y
                    }
                }

                // Guardar el documento
                string filePath = "tipo_de_rayos_x.pdf";
                document.Save(filePath);

                // Cerrar el lector
                reader.Close();

                // Abrir el PDF automáticamente
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private void ImprimirPacientes()
        {
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                conn.Open();
                string query = "SELECT * FROM REGISTRO_DE_PACIENTE";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfDocument document = new PdfDocument();
                document.Info.Title = "Registro de Pacientes";

                PdfPage page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont headerFont = new XFont("Arial", 14, XFontStyleEx.Bold);
                XFont normalFont = new XFont("Arial", 10);

                int yPosition = 40;

                gfx.DrawString("Registro de Pacientes", headerFont, XBrushes.DarkBlue, new XRect(0, yPosition, page.Width, page.Height), XStringFormats.TopCenter);
                yPosition += 40;

                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 20;

                // Ancho de las columnas actualizado
                int[] columnWidths = { 50, 100, 100, 80, 100, 180, 70, 100 }; // Dirección más pequeña (150)
                string[] headers = { "ID", "Nombre", "Apellido Paterno", "Apellido Materno", "Fecha Nacimiento", "Correo Electrónico", "Dirección", "Contacto" };

                int xPosition = 40;
                for (int i = 0; i < headers.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[i], 20);
                    gfx.DrawString(headers[i], normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[i] - 10, 20), XStringFormats.CenterLeft);
                    xPosition += columnWidths[i];
                }
                yPosition += 20;

                while (reader.Read())
                {
                    xPosition = 40;

                    int cellHeight = 20;

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[0], cellHeight);
                    gfx.DrawString(reader["ID_PACIENTE"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[0] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[0];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[1], cellHeight);
                    gfx.DrawString(reader["NOMBRE"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[1] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[1];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[2], cellHeight);
                    gfx.DrawString(reader["APELLIDO_PA"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[2] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[2];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[3], cellHeight);
                    gfx.DrawString(reader["APELLIDO_MA"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[3] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[3];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[4], cellHeight);
                    gfx.DrawString(Convert.ToDateTime(reader["FECHA_DE_NACIMIENTO"]).ToString("dd/MM/yyyy"), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[4] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[4];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[5], cellHeight);
                    gfx.DrawString(reader["CORREO_ELECTRONICO"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[5] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[5];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[6], cellHeight);
                    gfx.DrawString(reader["DIRECCION"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[6] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[6];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[7], cellHeight);
                    gfx.DrawString(reader["CONTACTO"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[7] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[7];

                    yPosition += cellHeight;

                    if (yPosition > page.Height - 40)
                    {
                        page = document.AddPage();
                        page.Orientation = PdfSharp.PageOrientation.Landscape;
                        gfx = XGraphics.FromPdfPage(page);
                        yPosition = 40;
                    }
                }

                string filePath = "registro_de_pacientes.pdf";
                document.Save(filePath);

                reader.Close();

                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }
        private void btnimprimir_Click(object sender, RoutedEventArgs e)
        {
            switch (seccionActual)
            {
                case "Medicos":
                    ImprimirMedicos();
                    break;

                case "AreasMedicas":
                    ImprimirAreasMedicas();
                    break;

                case "RayosX":
                    ImprimirRayosX();
                    break;

                case "Pacientes":
                    ImprimirPacientes();
                    break;

                default:
                    MessageBox.Show("No hay datos disponibles para imprimir.");
                    break;
            }
        }
    }
}
