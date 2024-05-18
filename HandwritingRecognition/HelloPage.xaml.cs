using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HandwritingRecognition.ViewModel;

namespace HandwritingRecognition
{
    /// <summary>
    /// Логика взаимодействия для HelloPage.xaml
    /// </summary>
    public partial class HelloPage : Page
    {
        private EditorViewModel editor;

        public HelloPage()
        {
            InitializeComponent();
            editor = EditorViewModel.Instance;
        }

        private void ShowHelp_Click(object sender, RoutedEventArgs e)
        {
            // TODO.
        }

        private void OpenEditorButton_Click(object sender, RoutedEventArgs e)
        {
            editor.CreateConfig();
            NavigateToEditor();
        }

        private void NavigateToEditor()
        {
            NavigationService.Navigate(new EditorPage());
        }

        private void LoadPNGButton_Click(object sender, RoutedEventArgs e)
        {
            if (editor.ImportImages())
            {
                NavigateToEditor();
            }
        }

        private void LoadPDFButton_Click(object sender, RoutedEventArgs e)
        {
            if (editor.ImportPDF())
            {
                NavigateToEditor();
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                bool handled;
                if (files.Length == 1 && Path.GetExtension(files[0]) == ".pdf")
                {
                    handled = editor.ImportPDF(files[0]);
                }
                else
                {
                    handled = editor.ImportImages(files);
                }

                if (handled)
                {
                    NavigateToEditor();
                }
            }
        }
    }
}
