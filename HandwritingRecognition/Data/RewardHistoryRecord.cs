using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
