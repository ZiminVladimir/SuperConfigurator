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
using System.Collections.Generic;
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
            parser_E_Catalog.OnComplited += Parser_OnComplited;
            InitializeComponent();
        }
        public void Parser_OnComplited(object o) { MessageBox.Show("Работа завершена!"); }
        private void BaseUpdate_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.Application.EnableVisualStyles();
            //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //System.Windows.Forms.Application.Run(new Form1());
            //Form1 form = new Form1();
            //parser_E_Catalog.Settings = new E_CatalogSettings();
            Warning w = new Warning();
            Visibility = Visibility.Hidden;
            w.ShowDialog();
            Visibility = Visibility.Visible;
            //parser_E_Catalog.Start();
            //Change();
        }

        private void BuildPC_Click(object sender, RoutedEventArgs e)
        {
            Configurator conf = new Configurator();
            Visibility = Visibility.Hidden;
            conf.ShowDialog();
            Visibility = Visibility.Visible;
        }
        //public async void Change()
        //{
        //    if (ProgressBar.Value == 14)
        //        return;
        //    else
        //    {                
        //        if (parser_E_Catalog.IsActive)
        //            Progress();
        //        await Task.Delay(1000);
        //        Change();
        //    }
        //}
        //public void Progress()
        //{
        //    ProgressBar.Value++;
        //}
    }
}
