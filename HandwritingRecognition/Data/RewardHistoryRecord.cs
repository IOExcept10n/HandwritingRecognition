using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    public class RewardHistoryRecord : INotifyPropertyChanged
    {
        private DateOnly? date;
        private string? stampInfo;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DateOnly? Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public string? StampInfo { get => stampInfo; set => stampInfo = value; }

        public string? Details { get; set; }

        public string? OrderInfo { get; set; }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
