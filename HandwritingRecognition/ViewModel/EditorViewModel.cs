using HandwritingRecognition.ComputerVision;
using HandwritingRecognition.ComputerVision.Processing;
using HandwritingRecognition.Data;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Packaging;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace HandwritingRecognition.ViewModel
{
    /// <summary>
    /// Provides the access to the employees employment history editing. This class is intermediate between data models and UI.
    /// </summary>
    internal class EditorViewModel : INotifyPropertyChanged
    {
        private string? filePath;
        private SerializationFormat format;
        private bool hasUnsavedChanges;
        private EmploymentHistory history;
        private int imageIndex;


        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class.
        /// </summary>
        public EditorViewModel()
        {
            Instance = this;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        private enum SerializationFormat
        {
            Xml,
            Json,
        }

        /// <summary>
        /// Gets the main instance of an editor. It is used to simplify bindings between pages (yes, I know this is a bit bad solution).
        /// </summary>
        public static EditorViewModel Instance { get; set; } = null!;

        /// <summary>
        /// Gets the current imported images for preview.
        /// </summary>
        public BitmapImage CurrentImage => Images[imageIndex];

        /// <summary>
        /// Gets or sets a value indicating whether the instance has unsaved changes.
        /// </summary>
        public bool HasUnsavedChanges
        {
            get => hasUnsavedChanges;
            set
            {
                hasUnsavedChanges = value;
                OnPropertyChanged(nameof(WindowTitle));
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current edited instance of the employment history.
        /// </summary>
        public EmploymentHistory History
        {
            get => history;
            set
            {
                history = value;
                if (history != null)
                {
                    history.HistoryDefinition.PropertyChanged += History_PropertyChanged;
                    history.Info.PropertyChanged += History_PropertyChanged;
                    history.Jobs.CollectionChanged += History_CollectionChanged;
                    history.Rewards.CollectionChanged += History_CollectionChanged;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the service that provides asynchronous image recognition for the editing simplification.
        /// </summary>
        public IImagePipeline ImageRecognizer { get; private set; }

        /// <summary>
        /// Gets the list of currently loaded images.
        /// </summary>
        public ObservableCollection<BitmapImage> Images { get; } = [];

        /// <summary>
        /// Gets the current window title to display.
        /// </summary>
        public string? WindowTitle
        {
            get
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return "Handwriting Recognizer";
                }

                if (HasUnsavedChanges) return filePath + '*';
                return filePath;
            }
        }

        /// <summary>
        /// Creates a new configuration for edit.
        /// </summary>
        /// <returns><see langword="true"/> if the configuration was created successfully; otherwise <see langword="false"/>.</returns>
        public bool CreateConfig()
        {
            if (SaveIfUnsaved())
            {
                History = new();
                Images.Clear();
                filePath = null;
                HasUnsavedChanges = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Imports the images to recognize.
        /// </summary>
        /// <param name="imagePaths">Array of paths to recognize or <see langword="null"/> if user should select them in dialog.</param>
        /// <returns><see langword="true"/> if the images were loaded successfully, <see langword="false"/> otherwise.</returns>
        public async Task<bool> ImportImages(string[]? imagePaths = null)
        {
            if (imagePaths == null)
            {
                OpenFileDialog dialog = new()
                {
                    DefaultExt = "png",
                    Title = "Выберите изображения для распознавания",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Multiselect = true,
                    Filter = "Изображения PNG|*.png|Изображения JPEG|*.jpg;*.jpeg|Все файлы|*.*",
                };
                if (dialog.ShowDialog() == true)
                {
                    imagePaths = dialog.FileNames;
                }
                else
                {
                    return false;
                }
            }

            var progress = new Progress<ProgressInfo>();
            var task = ImageRecognizer.ProcessAsync(imagePaths, History, progress);

            foreach (var path in imagePaths)
            {
                Images.Add(LoadImage(path));
            }

            await task;

            return true;
        }

        [Obsolete]
        public async Task<bool> ImportPDF(string? path = null)
        {
            if (path == null)
            {
                OpenFileDialog dialog = new()
                {
                    DefaultExt = "pdf",
                    Title = "Выберите документ для распознавания",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Filter = "Документ Portable Document Format|*.pdf|Все файлы|*.*",
                };
                if (dialog.ShowDialog() == true)
                {
                    path = dialog.FileName;
                }
                else
                {
                    return false;
                }
            }

            // TODO

            return true;
        }

        /// <summary>
        /// Initializes a default instance of the image recognizer.
        /// </summary>
        public void InitializePipeline()
        {
            ImageRecognizer = new DefaultImageRecognitionPipeline();
        }

        /// <summary>
        /// Loads a configuration for edit.
        /// </summary>
        /// <returns><see langword="true"/> if the configuration was loaded successfully; otherwise <see langword="false"/>.</returns>
        public bool LoadConfig()
        {
            OpenFileDialog dialog = new()
            {
                DefaultExt = "xml",
                Title = "Выберите файл с информацией о трудовой книге",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "XML файл записи о трудовой книге|*.xml|JSON файл записи о трудовой книге|*.json|Все файлы|*.*",
            };
            if (dialog.ShowDialog() == true)
            {
                return Load(filePath = dialog.FileName);
            }

            return false;
        }

        /// <summary>
        /// Loads an image with its path for preview.
        /// </summary>
        /// <param name="path">Path of an image to load.</param>
        /// <returns>Image that is ready for display.</returns>
        public BitmapImage LoadImage(string path)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri(path);
            bitmapImage.EndInit();
            bitmapImage.Freeze(); // Make the BitmapImage thread-safe
            return bitmapImage;
        }

        /// <summary>
        /// Selects the next image for preview.
        /// </summary>
        public void NextImage()
        {
            OnPropertyChanged(nameof(CurrentImage));
            if (imageIndex + 1 >= Images.Count)
            {
                imageIndex = 0;
            }
        }

        /// <summary>
        /// Selects the previous image for preview.
        /// </summary>
        public void PreviousImage()
        {
            OnPropertyChanged(nameof(CurrentImage));
            if (imageIndex - 1 < 0)
            {
                imageIndex = Images.Count - 1;
            }
        }

        /// <summary>
        /// Saves a configuration in one of the specified formats.
        /// </summary>
        /// <returns><see langword="true"/> if the configuration was saved successfully; otherwise <see langword="false"/>.</returns>
        public bool SaveAs()
        {
            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "xml",
                OverwritePrompt = true,
                Title = "Сохранить запись",
                Filter = "XML файл записи о трудовой книге|*.xml|JSON файл записи о трудовой книге|*.json|Все файлы|*.*",
            };
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
                string ext = Path.GetExtension(filePath);
                if (ext == ".json")
                {
                    format = SerializationFormat.Json;
                }
                else
                {
                    format = SerializationFormat.Xml;
                }

                return Save(filePath);
            }

            return false;
        }

        /// <summary>
        /// Saves a configuration if it has unsaved changes.
        /// </summary>
        /// <returns><see langword="true"/> if the configuration was saved successfully; otherwise <see langword="false"/>.</returns>
        public bool SaveIfUnsaved()
        {
            if (!HasUnsavedChanges)
                return true;

            if (!string.IsNullOrEmpty(filePath) && Path.Exists(Path.GetDirectoryName(filePath)))
            {
                return Save(filePath);
            }

            return SaveAs();
        }

        private void History_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
        }

        private void History_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            HasUnsavedChanges = true;
        }


        private bool Load(string path)
        {
            using var file = File.OpenRead(path);
            try
            {
                if (Path.GetExtension(path) == ".xml")
                {
                    XmlSerializer serializer = new(typeof(EmploymentHistory));
                    History = (serializer.Deserialize(file) as EmploymentHistory) ?? new();
                }
                else if (Path.GetExtension(path) == ".json")
                {
                    History = JsonSerializer.CreateDefault().Deserialize<EmploymentHistory>(new JsonTextReader(new StreamReader(file))) ?? new();
                }
                else
                {
                    throw new FileFormatException("The file path was specified with incorrect format.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка при загрузке: {ex.Message}");
                return false;
            }

            HasUnsavedChanges = false;
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool Save(string path)
        {
            using var file = File.Create(path);
            try
            {
                if (format == SerializationFormat.Xml)
                {
                    XmlSerializer serializer = new(typeof(EmploymentHistory));
                    serializer.Serialize(file, history);
                }
                else
                {
                    JsonSerializer.Create().Serialize(new StreamWriter(file), history);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка при сохранении: {ex.Message}");
                return false;
            }

            HasUnsavedChanges = false;
            return true;
        }
    }
}
