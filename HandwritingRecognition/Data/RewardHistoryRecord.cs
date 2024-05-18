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
        private string? details;
        private string? orderInfo;

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

        public string? StampInfo
        {
            get => stampInfo;
            set
            {
                stampInfo = value;
                OnPropertyChanged();
            }
        }

        public string? Details
        {
            get => details;
            set
            {
                details = value;
                OnPropertyChanged();
            }
        }

        public string? OrderInfo
        {
            get => orderInfo;
            set
            {
                orderInfo = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
