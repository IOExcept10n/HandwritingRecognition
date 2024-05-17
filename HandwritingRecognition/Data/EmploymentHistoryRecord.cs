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
        public event PropertyChangedEventHandler? PropertyChanged;

        public DateOnly? EmploymentDate { get; set; }

        public DateOnly? ExpellDate { get; set; }

        public string? PrintInfo { get; set; }

        public string? Position { get; set; }

        public string? OrderInfo { get; set; }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
