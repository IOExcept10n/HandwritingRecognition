using HandwritingRecognition.ViewModel;
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

namespace HandwritingRecognition
{
    /// <summary>
    /// Логика взаимодействия для EditorPage.xaml
    /// </summary>
    public partial class EditorPage : Page
    {
        public EditorPage()
        {
            InitializeComponent();
            EditorViewModel.Instance.History = new();
            for (int i = 0; i < 10; i++)
            {
                EditorViewModel.Instance.History.Jobs.Add(new()
                {
                    EmploymentDate = new(2000, 01, 02),
                    ExpellDate = new(2001, 03, 02),
                    OrderInfo = "Lorem Ipsum",
                    Position = "Ipsum Lorem",
                    PrintInfo = "Iprem Losum",
                });
            }
        }

        private void SaveConfigHandled(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OpenConfigHandled(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveConfigAsXmlHandled(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveConfigAsJsonHandled(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void LoadImagesHandled(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void NewFileHandled(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void ShowHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFileAsJson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFileAsXml_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
