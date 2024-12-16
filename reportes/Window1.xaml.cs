using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Data;
using Xprecion.Clases;
using System.Diagnostics;

namespace Xprecion.reportes
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        public void GenerarPDF()
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
        private void Guardarporfavorya_Click(object sender, RoutedEventArgs e)
        {
            GenerarPDF();
        }
    }
}
