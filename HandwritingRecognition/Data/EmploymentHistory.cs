// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HandwritingRecognition.Data
{
    /// <summary>
    /// Represents the information about the employment history record.
    /// </summary>
    public class EmploymentHistory
    {
        /// <summary>
        /// Gets the definition of this record.
        /// </summary>
        public EmploymentHistoryDefinition HistoryDefinition { get; init; } = new();

        /// <summary>
        /// Gets the information about employee.
        /// </summary>
        public EmployeeInfo Info { get; init; } = new();

        /// <summary>
        /// Gets the list of jobs in which the employee has participated.
        /// </summary>
        public ObservableCollection<EmploymentHistoryRecord> Jobs { get; init; } = [];

        /// <summary>
        /// Gets the list of rewards given to employee.
        /// </summary>
        public ObservableCollection<RewardHistoryRecord> Rewards { get; init; } = [];

        /// <summary>
        /// Represents the information about the employee.
        /// </summary>
        public class EmployeeInfo : INotifyPropertyChanged
        {
            private DateOnly? birthDate;
            private string? education;
            private string name = string.Empty;
            private string? overrideName;
            private string? overridePatronymic;
            private PassportInfo? overrideReason;
            private string? overrideSurname;
            private string? patronymic;
            private string? profession;
            private string surname = string.Empty;

            /// <inheritdoc/>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Gets or sets the birth date of the employee.
            /// </summary>
            public DateOnly? BirthDate
            {
                get => birthDate;
                set
                {
                    birthDate = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the education info.
            /// </summary>
            public string? Education
            {
                get => education;
                set
                {
                    education = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the employee name.
            /// </summary>
            [Required]
            public string Name
            {
                get => name;
                set
                {
                    name = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the employee name after rename (if it has been).
            /// </summary>
            public string? OverrideName
            {
                get => overrideName;
                set
                {
                    overrideName = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the employee patronymic after rename (if it has been).
            /// </summary>
            public string? OverridePatronymic
            {
                get => overridePatronymic;
                set
                {
                    overridePatronymic = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the reason of the rename (full passport info).
            /// </summary>
            public PassportInfo? OverrideReason
            {
                get => overrideReason;
                set
                {
                    overrideReason = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the employee surname after rename (if it has been).
            /// </summary>
            public string? OverrideSurname
            {
                get => overrideSurname;
                set
                {
                    overrideSurname = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the employee patronymic.
            /// </summary>
            public string? Patronymic
            {
                get => patronymic;
                set
                {
                    patronymic = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the profession info.
            /// </summary>
            public string? Profession
            {
                get => profession;
                set
                {
                    profession = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the employee surname.
            /// </summary>
            [Required]
            public string Surname
            {
                get => surname;
                set
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }

            private void OnPropertyChanged([CallerMemberName] string? name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Represents the definition of the employment history record.
        /// </summary>
        public class EmploymentHistoryDefinition : INotifyPropertyChanged
        {
            private string? number;
            private string? series;
            private string? stampInfo;
            private DateOnly? withdrawDate;

            /// <inheritdoc/>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Gets or sets the number of the history record.
            /// </summary>
            public string? Number
            {
                get => number;
                set
                {
                    number = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the series of the history record.
            /// </summary>
            public string? Series
            {
                get => series;
                set
                {
                    series = value;
                    OnPropertyChanged();
                }
            }

            /// <summary>
            /// Gets or sets the info about the stamp on the record.
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
            /// Gets or sets the date when the record was made.
            /// </summary>
            public DateOnly? WithdrawDate
            {
                get => withdrawDate;
                set
                {
                    withdrawDate = value;
                    OnPropertyChanged();
                }
            }

            private void OnPropertyChanged([CallerMemberName] string? name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
