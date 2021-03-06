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
using SuperParser;

namespace SuperConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ParserWorker parser_E_Catalog;
        public MainWindow()
        {
            parser_E_Catalog = new ParserWorker(new E_CatalogParser());
            InitializeComponent();
        }
        

        private void BaseUpdate1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Warning w = new Warning();
            Visibility = Visibility.Hidden;
            w.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void BuildPC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Configurator conf = new Configurator();
            Visibility = Visibility.Hidden;
            conf.ShowDialog();
            Visibility = Visibility.Visible;
        }
       
    }
}
