// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.ComponentModel;
using System.Runtime.CompilerServices;

#pragma warning disable
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
