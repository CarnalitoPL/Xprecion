﻿using System;
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
    }
}
