using HandwritingRecognition.ViewModel;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand SaveCommand = new("SaveConfig", typeof(MainWindow));
        public static RoutedCommand OpenCommand = new("OpenConfig", typeof(MainWindow));
        public static RoutedCommand NewFileCommand = new("NewConfig", typeof(MainWindow));
        public static RoutedCommand SaveAsCommand = new("SaveConfigAs", typeof(MainWindow));
        public static RoutedCommand LoadImagesCommand = new("LoadImages", typeof(MainWindow));

        private readonly EditorViewModel editor;

        public MainWindow()
        {
            InitializeComponent();
            editor = EditorViewModel.Instance;
            editor.InitializePipeline();
        }
    }
}