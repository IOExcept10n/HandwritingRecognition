using HandwritingRecognition.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HandwritingRecognition.ViewModel
{
    internal class EditorViewModel : INotifyPropertyChanged
    {
        private EmploymentHistory history;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
