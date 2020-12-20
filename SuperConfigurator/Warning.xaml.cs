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
using SuperParser;

namespace SuperConfigurator
{
    /// <summary>
    /// Логика взаимодействия для Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        ParserWorker parser_E_Catalog;
        public Warning()
        {
            var sb = new StringBuilder();
            sb.Append("Внимание!");
            sb.AppendLine();
            sb.Append("Данная процедура может занять до 4 часов и потребует ресурсов вашего компьютера.");
            sb.AppendLine();
            sb.Append("Для того, чтобы продолжить, напишите в поле ниже слово Обновить.");
            LabelWarning = new Label();
            LabelWarning.Content = sb.ToString();
            parser_E_Catalog = new ParserWorker(new E_CatalogParser());
            ProgressBar = new ProgressBar();
            ProgressBar.Visibility = Visibility.Hidden;
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (Check.Text=="Обновить"|| Check.Text == "обновить")
            {
                ProgressBar.Visibility = Visibility.Visible;
                parser_E_Catalog.Settings = new E_CatalogSettings();
                parser_E_Catalog.Start();
                Change();
            }
        }
        public async void Change()
        {
            if (ProgressBar.Value == 161)
            {
                MessageBox.Show("База обновлена");
                Close();
                return;
            }
            else
            {
                Progress();
                await Task.Delay(1000);
                Change();
            }
        }
        public void Progress()
        {
            ProgressBar.Value=parser_E_Catalog.count+1;
        }

        private void ButtonNOOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
