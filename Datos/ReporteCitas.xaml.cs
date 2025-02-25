using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Lógica de interacción para ReporteCitas.xaml
    /// </summary>
    public partial class ReporteCitas : Window
    {
        private string conexionString = @"Data Source=PRADOROBOT\MSSQLSERVER01;Initial Catalog=Xprecion2.0;Integrated Security=True;TrustServerCertificate=True";

        public ReporteCitas()
        {
            InitializeComponent();
            CargarCitas();
        }

        private void CargarCitas(string fecha = "", string paciente = "", string medico = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexionString))
                {
                    conn.Open();
                    string consulta = "SELECT C.ID_CITAS, C.FECHA, C.HORA, C.ESTADOCITA, C.COMENTARIO, " +
                                      "P.NOMBRE + ' ' + P.APELLIDO_PA + ' ' + P.APELLIDO_MA AS PACIENTE, " +
                                      "M.NOMBRE + ' ' + M.APELLIDO_PA + ' ' + M.APELLIDO_MA AS MEDICO " +
                                      "FROM CITAS C " +
                                      "LEFT JOIN REGISTRO_DE_PACIENTE P ON C.ID_PACIENTE = P.ID_PACIENTE " +
                                      "LEFT JOIN MEDICO M ON C.ID_MEDICO = M.ID_MEDICO " +
                                      "WHERE (@fecha = '' OR C.FECHA = @fecha) " +
                                      "AND (@paciente = '' OR P.NOMBRE + ' ' + P.APELLIDO_PA + ' ' + P.APELLIDO_MA LIKE '%' + @paciente + '%') " +
                                      "AND (@medico = '' OR M.NOMBRE + ' ' + M.APELLIDO_PA + ' ' + M.APELLIDO_MA LIKE '%' + @medico + '%') " +
                                      "ORDER BY C.FECHA DESC";

                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        cmd.Parameters.AddWithValue("@fecha", string.IsNullOrEmpty(fecha) ? "" : fecha);
                        cmd.Parameters.AddWithValue("@paciente", paciente);
                        cmd.Parameters.AddWithValue("@medico", medico);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridCitas.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar citas: " + ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string fecha = dpFecha.SelectedDate.HasValue ? dpFecha.SelectedDate.Value.ToString("yyyy-MM-dd") : "";
            string paciente = txtPaciente.Text.Trim();
            string medico = txtMedico.Text.Trim();

            CargarCitas(fecha, paciente, medico);
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            ImprimirReporte();
        }

        private void ImprimirReporte()
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                FlowDocument doc = new FlowDocument();
                doc.PagePadding = new Thickness(50);
                doc.ColumnWidth = 500;

                Paragraph titulo = new Paragraph(new Run("Reporte de Citas"))
                {
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center
                };
                doc.Blocks.Add(titulo);

                Table table = new Table();
                table.CellSpacing = 5;

                for (int i = 0; i < dataGridCitas.Columns.Count; i++)
                {
                    table.Columns.Add(new TableColumn());
                }

                TableRow headerRow = new TableRow();
                foreach (var column in dataGridCitas.Columns)
                {
                    headerRow.Cells.Add(new TableCell(new Paragraph(new Run(column.Header.ToString())))
                    {
                        FontWeight = FontWeights.Bold,
                        TextAlignment = TextAlignment.Center,
                        BorderThickness = new Thickness(1),
                        BorderBrush = Brushes.Black
                    });
                }

                TableRowGroup headerGroup = new TableRowGroup();
                headerGroup.Rows.Add(headerRow);
                table.RowGroups.Add(headerGroup);

                TableRowGroup dataGroup = new TableRowGroup();
                foreach (DataRowView row in dataGridCitas.Items)
                {
                    TableRow dataRow = new TableRow();
                    foreach (var column in dataGridCitas.Columns)
                    {
                        string cellValue = row[column.DisplayIndex].ToString();
                        dataRow.Cells.Add(new TableCell(new Paragraph(new Run(cellValue)))
                        {
                            TextAlignment = TextAlignment.Center,
                            BorderThickness = new Thickness(1),
                            BorderBrush = Brushes.Black
                        });
                    }
                    dataGroup.Rows.Add(dataRow);
                }
                table.RowGroups.Add(dataGroup);

                doc.Blocks.Add(table);
                IDocumentPaginatorSource paginatorSource = doc;
                printDialog.PrintDocument(paginatorSource.DocumentPaginator, "Impresión de Citas");
            }
        }
    }
}
