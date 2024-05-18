using HandwritingRecognition.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HandwritingRecognition.ViewModel
{
    internal class EditorViewModel : INotifyPropertyChanged
    {
        private EmploymentHistory history;

        public event PropertyChangedEventHandler? PropertyChanged;

        public static EditorViewModel Instance { get; set; } = null;

        public EmploymentHistory History
        {
            get => history;
            set
            {
                history = value;
                // TODO
                OnPropertyChanged();
            }
        }

        public EditorViewModel()
        {
            Instance = this;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
