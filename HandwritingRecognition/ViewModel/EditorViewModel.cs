using HandwritingRecognition.Data;
using Microsoft.Win32;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Packaging;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Xml.Serialization;

namespace HandwritingRecognition.ViewModel
{
    internal class EditorViewModel : INotifyPropertyChanged
    {
        private EmploymentHistory history;
        private bool hasUnsavedChanges;
        private string filePath;
        private SerializationFormat format;

        public EditorViewModel()
        {
            Instance = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static EditorViewModel Instance { get; set; } = null;

        public bool HasUnsavedChanges
        {
            get => hasUnsavedChanges;
            set
            {
                hasUnsavedChanges = value;
                OnPropertyChanged();
            }
        }

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

        private void History_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasUnsavedChanges = true;
        }

        private void History_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            HasUnsavedChanges = true;
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
                    JsonSerializer.Serialize(file, history);
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
                    History = JsonSerializer.Deserialize<EmploymentHistory>(file) ?? new();
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

        public bool CreateConfig()
        {
            if (SaveIfUnsaved())
            {
                History = new();
                HasUnsavedChanges = false;
                return true;
            }

            return false;
        }

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

        public bool ImportImages(string[]? images = null)
        {
            if (images == null)
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
                    images = dialog.FileNames;
                }
                else
                {
                    return false;
                }
            }

            // TODO

            return true;
        }

        public bool ImportPDF(string? path = null)
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

        private enum SerializationFormat
        {
            Xml,
            Json,
        }
    }
}
