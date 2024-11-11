using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Forms;

namespace Xprecion.Datos
{
    /// <summary>
    /// Lógica de interacción para SearchForm.xaml
    /// </summary>
    public partial class SearchForm : Window
    {
        public SearchForm()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg.Items.Count > 1)
                {
                    int x = dg.Items.IndexOf(dg.SelectedItem);

                    var selectedRow = dg.Items[x] as DataRowView;
                    if (selectedRow != null)
                    {

                        mReturnValue = selectedRow.Row.ItemArray[0].ToString();
                        DialogResult = true;

                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
        private string mReturnValue;
        internal string ReturnValue
        {
            get
            {
                return mReturnValue;
            }
        }
        internal int mColNumber;
        internal DataSet mDS;

        private void gridformulario_Loaded(object sender, RoutedEventArgs e)
        {
            cmbMatch.Items.Add("Cualquier parte del Campo");
            cmbMatch.Items.Add("Total del Campo");
            cmbMatch.Items.Add("Inicio del campo");
            try
            {
                dg.ItemsSource = mDS.Tables["Table"].DefaultView;
                var collectionView = CollectionViewSource.GetDefaultView(dg.ItemsSource);
                collectionView.Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }


            //DataColumn dc;
            foreach (DataColumn dc in mDS.Tables[0].Columns)
            {
                cmbLookIn.Items.Add(dc.ColumnName);
            }

            DataGridTableStyle ts1 = new DataGridTableStyle();
            ts1.MappingName = "table";
            cmbLookIn.SelectedIndex = 0;
            cmbMatch.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFindWhat.Text))
                {
                    if ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex]
                        .DataType.ToString()) == "System.String")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        switch (cmbMatch.SelectedIndex)
                        {
                            case 0: //Any Part of the Field
                                dv.RowFilter = cmbLookIn.Text + " LIKE '%" + txtFindWhat.Text + "%'";
                                break;
                            case 1: //Whole Field
                                dv.RowFilter = cmbLookIn.Text + "='" + txtFindWhat.Text + "'";
                                break;
                            case 2: // Start of the Field
                                dv.RowFilter = cmbLookIn.Text + " LIKE '" + txtFindWhat.Text + "%'";
                                break;
                        }
                        dg.ItemsSource = dv;
                        dg.ItemsSource = mDS.Tables["Table"].DefaultView;
                    }
                    else if
                       ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex].DataType.ToString()) == "System.DateTime")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=#" + txtFindWhat.Text + "#";
                        dg.ItemsSource = dv;
                    }
                    else
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=" + txtFindWhat.Text;
                        dg.ItemsSource = dv;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Introduzca el valor a buscar");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }


        private void txtFindWhat_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFindWhat.Text))
                {
                    if ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex].DataType.ToString()) == "System.String")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        switch (cmbMatch.SelectedIndex)
                        {
                            case 0: //Cualquier parte del campo
                                dv.RowFilter = cmbLookIn.Text + " LIKE '%" + txtFindWhat.Text + "%'";
                                break;
                            case 1: //Total del campo
                                dv.RowFilter = cmbLookIn.Text + "='" + txtFindWhat.Text + "'";
                                break;
                            case 2: // Inicio del campo
                                dv.RowFilter = cmbLookIn.Text + " LIKE '" + txtFindWhat.Text + "%'";
                                break;
                        }
                        dg.ItemsSource = dv;
                    }
                    else if ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex].DataType.ToString()) == "System.DateTime")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=#" + txtFindWhat.Text + "#";
                        dg.ItemsSource = dv;
                    }
                    else
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=" + txtFindWhat.Text;
                        dg.ItemsSource = dv;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Introduzca el valor a buscar");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = mDS.Tables["Table"].DefaultView;
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dg.Items.Count > 1)
                {
                    // Obtener el elemento seleccionado
                    int x = dg.Items.IndexOf(dg.SelectedItem);
                    var selectedRow = dg.Items[x] as DataRowView;
                    if (selectedRow != null)
                    {
                        mReturnValue = selectedRow.Row.ItemArray[0].ToString();
                        DialogResult = true;

                    }
                }


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
