using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    public class PassportInfo : INotifyPropertyChanged
    {
        private string documentType;
        private string? series;
        private string? number;
        private DateOnly? acquireDate;
        private string? givenBy;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string DocumentType
        {
            get => documentType;
            set
            {
                documentType = value;
                OnPropertyChanged();
            }
        }

        public string? Series
        {
            get => series;
            set
            {
                series = value;
                OnPropertyChanged();
            }
        }

        public string? Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public DateOnly? AcquireDate
        {
            get => acquireDate;
            set
            {
                acquireDate = value;
                OnPropertyChanged();
            }
        }

        public string? GivenBy
        {
            get => givenBy;
            set
            {
                givenBy = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
