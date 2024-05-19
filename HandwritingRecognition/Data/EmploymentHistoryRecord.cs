// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HandwritingRecognition.Data
{
    /// <summary>
    /// Gets the information about employee's previous job.
    /// </summary>
    public class EmploymentHistoryRecord : INotifyPropertyChanged
    {
        private DateOnly? employmentDate;
        private DateOnly? expellDate;
        private string? printInfo;
        private string? position;
        private string? orderInfo;
        private string? details;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the date of employment.
        /// </summary>
        public DateOnly? EmploymentDate
        {
            get => employmentDate;
            set
            {
                employmentDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the date of end of work.
        /// </summary>
        public DateOnly? ExpellDate
        {
            get => expellDate;
            set
            {
                expellDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the info about the stamp.
        /// </summary>
        public string? PrintInfo
        {
            get => printInfo;
            set
            {
                printInfo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets employee's role.
        /// </summary>
        public string? Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the information about employment order.
        /// </summary>
        public string? OrderInfo
        {
            get => orderInfo;
            set
            {
                orderInfo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the details about the job.
        /// </summary>
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
