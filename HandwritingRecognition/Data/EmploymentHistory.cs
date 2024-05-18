using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    class EmploymentHistory : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public EmploymentHistoryDefinition HistoryDefinition { get; init; } = new();

        public EmployeeInfo Info { get; init; } = new();

        public SpecializationInfo Specialization { get; init; } = new();

        public ObservableCollection<EmploymentHistoryRecord> Jobs { get; init; } = [];

        public ObservableCollection<RewardHistoryRecord> Rewards { get; init; } = [];

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public class EmploymentHistoryDefinition : INotifyPropertyChanged
        {
            private string? series;
            private string? number;
            private DateOnly? withdrawDate;

            public event PropertyChangedEventHandler? PropertyChanged;

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

        public class EmployeeInfo : INotifyPropertyChanged
        {
            private string surname;
            private string? overrideSurname;
            private string name;
            private string? overrideName;
            private string? partonymic;
            private string? overridePatronymic;
            private DateOnly? birthDate;
            private PassportInfo? overrideReason;

            public event PropertyChangedEventHandler? PropertyChanged;

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

            public string? OverrideSurname
            {
                get => overrideSurname;
                set
                {
                    overrideSurname = value;
                    OnPropertyChanged();
                }
            }

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

            public string? OverrideName
            {
                get => overrideName;
                set
                {
                    overrideName = value;
                    OnPropertyChanged();
                }
            }

            public string? Partonymic
            {
                get => partonymic;
                set
                {
                    partonymic = value;
                    OnPropertyChanged();
                }
            }

            public string? OverridePatronymic
            {
                get => overridePatronymic;
                set
                {
                    overridePatronymic = value;
                    OnPropertyChanged();
                }
            }

            public DateOnly? BirthDate
            {
                get => birthDate;
                set
                {
                    birthDate = value;
                    OnPropertyChanged();
                }
            }

            public PassportInfo? OverrideReason
            {
                get => overrideReason;
                set
                {
                    overrideReason = value;
                    OnPropertyChanged();
                }
            }

            private void OnPropertyChanged([CallerMemberName] string? name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public class SpecializationInfo : INotifyPropertyChanged
        {
            private string? education;
            private string? profession;

            public event PropertyChangedEventHandler? PropertyChanged;

            public string? Education
            {
                get => education;
                set
                {
                    education = value;
                    OnPropertyChanged();
                }
            }

            public string? Profession
            {
                get => profession;
                set
                {
                    profession = value;
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
