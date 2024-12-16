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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Xprecion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MIAreaMedica_Click(object sender, RoutedEventArgs e)
        {
            Datos.Area_medica x = new Datos.Area_medica();
            x.Owner = this;
            x.Show();

        }

        private void MITipoDeRayosX_Click(object sender, RoutedEventArgs e)
        {
            Datos.frmTipoDeRayosX x = new Datos.frmTipoDeRayosX();
            x.Owner = this;
            x.Show();
        }

        private void MIMedico_Click(object sender, RoutedEventArgs e)
        {
            Datos.frmMedico x = new Datos.frmMedico();
            x.Owner = this;
            x.Show();
        }

        private void Registros_Click(object sender, RoutedEventArgs e)
        {
            Datos.UltimosRegistros x = new Datos.UltimosRegistros();
            x.Owner = this;
            x.Show();
        }

        private void MIPacientes_Click(object sender, RoutedEventArgs e)
        {
            Datos.frmPacientes x = new Datos.frmPacientes();
            x.Owner = this;
            x.Show();
        }

        private void miAgendarCita_Click(object sender, RoutedEventArgs e)
        {
            Datos.AgendarCita x = new Datos.AgendarCita();
            x.Owner = this;
            x.Show();
        }

        private void Citas_Activas_Click(object sender, RoutedEventArgs e)
        {
            Datos.UltimasCitas x = new Datos.UltimasCitas();
            x.Owner = this;
            x.Show();
        }

        private void miResultado_Click(object sender, RoutedEventArgs e)
        {
            Datos.frmResultadoCitas x = new Datos.frmResultadoCitas();
            x.Owner = this;
            x.Show();
        }
    }
}
