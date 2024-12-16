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
    /// Lógica de interacción para UltimasCitas.xaml
    /// </summary>
    public partial class UltimasCitas : Window
    {
        public UltimasCitas()
        {
            InitializeComponent();
        }
        private string seccionActual = string.Empty;
        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
            seccionActual = "Citas";
            CargarDatos("SELECT * FROM vta_citas");
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


        private void ImprimirCitas()
        {
            using (SqlConnection conn = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                conn.Open();
                string query = "SELECT * FROM vta_citas_imprimir";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfDocument document = new PdfDocument();
                document.Info.Title = "Reporte de Citas";

                PdfPage page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont headerFont = new XFont("Arial", 20, XFontStyleEx.Bold);
                XFont normalFont = new XFont("Arial", 10);

                int yPosition = 40;

                gfx.DrawString("Reporte de Citas", headerFont, XBrushes.DarkBlue, new XRect(0, yPosition, page.Width, page.Height), XStringFormats.TopCenter);
                yPosition += 40;

                gfx.DrawLine(XPens.Black, 40, yPosition, page.Width - 40, yPosition);
                yPosition += 20;

                // Ancho de las columnas ajustado
                int[] columnWidths = { 50, 60, 60, 50, 250, 100, 100, 100 }; // Ajustado para cada columna
                string[] headers = { "ID Cita", "Fecha", "Hora", "Estado", "Comentario", "Médico", "Paciente", "Servicio" };

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
                    gfx.DrawString(reader["ID_CITAS"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[0] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[0];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[1], cellHeight);
                    gfx.DrawString(Convert.ToDateTime(reader["FECHA"]).ToString("dd/MM/yyyy"), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[1] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[1];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[2], cellHeight);
                    gfx.DrawString(reader["HORA"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[2] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[2];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[3], cellHeight);
                    gfx.DrawString(reader["ESTADOCITA"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[3] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[3];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[4], cellHeight);
                    gfx.DrawString(reader["COMENTARIO"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[4] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[4];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[5], cellHeight);
                    gfx.DrawString(reader["MEDICO"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[5] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[5];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[6], cellHeight);
                    gfx.DrawString(reader["PACIENTE"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[6] - 10, cellHeight), XStringFormats.CenterLeft);
                    xPosition += columnWidths[6];

                    gfx.DrawRectangle(XPens.Black, xPosition, yPosition, columnWidths[7], cellHeight);
                    gfx.DrawString(reader["SERVICIO"].ToString(), normalFont, XBrushes.Black, new XRect(xPosition + 5, yPosition, columnWidths[7] - 10, cellHeight), XStringFormats.CenterLeft);
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

                string filePath = "reporte_citas.pdf";
                document.Save(filePath);

                reader.Close();

                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }

        }
        private void btnExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            switch (seccionActual)
            {
                case "Citas":
                    ImprimirCitas();
                    break;
                default:
                    MessageBox.Show("No hay datos disponibles para imprimir.");
                    break;
            }
        }
    }
}
