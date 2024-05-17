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
        public event PropertyChangedEventHandler? PropertyChanged;

        public string DocumentType { get; set; }

        public string? Series { get; set; }

        public string? Number { get; set; }

        public DateOnly? AcquireDate { get; set; }

        public string? GivenBy { get; set; }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
