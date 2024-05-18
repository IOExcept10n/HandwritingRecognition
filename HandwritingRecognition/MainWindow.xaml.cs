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
        public static RoutedCommand SaveAsXmlCommand = new("SaveConfigAsXml", typeof(MainWindow));
        public static RoutedCommand SaveAsJsonCommand = new("SaveConfigAsJson", typeof(MainWindow));
        public static RoutedCommand LoadImagesCommand = new("LoadImages", typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}