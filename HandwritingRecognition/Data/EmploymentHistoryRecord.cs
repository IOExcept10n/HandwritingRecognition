using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    public class EmploymentHistoryRecord : INotifyPropertyChanged
    {
        private DateOnly? employmentDate;
        private DateOnly? expellDate;
        private string? printInfo;
        private string? position;
        private string? orderInfo;
        private string? details;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DateOnly? EmploymentDate
        {
            get => employmentDate;
            set
            {
                employmentDate = value;
                OnPropertyChanged();
            }
        }

        public DateOnly? ExpellDate
        {
            get => expellDate;
            set
            {
                expellDate = value;
                OnPropertyChanged();
            }
        }

        public string? PrintInfo
        {
            get => printInfo;
            set
            {
                printInfo = value;
                OnPropertyChanged();
            }
        }

        public string? Position
        {
            get => position;
            set
            {
                position = value;
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

        public string? Details
        {
            get => details;
            set
            {
                details = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
