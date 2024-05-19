using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HandwritingRecognition.ViewModel;

namespace HandwritingRecognition
{
    /// <summary>
    /// Interaction logic for EditorPage.xaml.
    /// </summary>
    public partial class EditorPage : Page
    {
        private readonly EditorViewModel viewModel;

        public EditorPage()
        {
            InitializeComponent();
            viewModel = EditorViewModel.Instance;
        }

        private void SaveConfigHandled(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.SaveIfUnsaved();
        }

        private void OpenConfigHandled(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.LoadConfig();
        }

        private void SaveConfigAsHandled(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.SaveAs();
        }

        private void LoadImagesHandled(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.ImportImages();
        }

        private void NewFileHandled(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.CreateConfig();
        }

        private void ShowHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = @"https://github.com/IOExcept10n/HandwritingRecognition/blob/main/Help.md",
                UseShellExecute = true
            });
        }

        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveAs();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveIfUnsaved();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LoadConfig();
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CreateConfig();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveIfUnsaved();
        }

        private void ImportImages_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImportImages();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HelloPage());
        }

        private void DataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as DataGrid)!.SelectedItem = null;
        }
    }
}
