using HandwritingRecognition.ViewModel;
using System.ComponentModel;
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
            editor = EditorViewModel.Instance = (EditorViewModel)DataContext;
            editor.InitializePipeline();
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            OpenCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            SaveAsCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));
            LoadImagesCommand.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
            NewFileCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            e.Cancel = !editor.SaveIfUnsaved();
        }
    }
}