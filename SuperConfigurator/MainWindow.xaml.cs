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

namespace SuperConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BaseUpdate_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.Application.EnableVisualStyles();
            //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //System.Windows.Forms.Application.Run(new Form1());
            Form1 form = new Form1();
            Visibility = Visibility.Hidden;
            form.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void BuildPC_Click(object sender, RoutedEventArgs e)
        {
            Configurator conf = new Configurator();
            Visibility = Visibility.Hidden;
            conf.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
