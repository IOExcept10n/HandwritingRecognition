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
        public HelloPage()
        {
            InitializeComponent();
        }
        private void ShowHelp_Click(object sender, RoutedEventArgs e)
        {
            // TODO.
        }
        private void OpenEditorButton_Click(object sender, RoutedEventArgs e)
        {
            EditorViewModel.Instance.CreateConfig();
            NavigationService.Navigate(new EditorPage());
        }
    }
}
