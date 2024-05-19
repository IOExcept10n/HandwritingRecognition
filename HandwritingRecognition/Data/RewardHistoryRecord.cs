// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HandwritingRecognition.Data
{
    /// <summary>
    /// Gets or sets the information about the employee rewards.
    /// </summary>
    public class RewardHistoryRecord : INotifyPropertyChanged
    {
        private DateOnly? date;
        private string? stampInfo;
        private string? details;
        private string? orderInfo;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the date or the reward giving.
        /// </summary>
        public DateOnly? Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the information about the stamp.
        /// </summary>
        public string? StampInfo
        {
            get => stampInfo;
            set
            {
                stampInfo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the details about the reward.
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

        /// <summary>
        /// Gets or sets the info about reward order.
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

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
