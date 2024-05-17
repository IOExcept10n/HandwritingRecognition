using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    class EmploymentHistory
    {
        public EmploymentHistoryDefinition HistoryDefinition { get; init; } = new();

        public EmployeeInfo Info { get; init; } = new();

        public SpecializationInfo Specialization { get; init; } = new();

        public ObservableCollection<EmploymentHistoryRecord> Jobs { get; init; } = [];

        public ObservableCollection<RewardHistoryRecord> Rewards { get; init; } = [];

        public class EmploymentHistoryDefinition
        {
            public string? Series { get; set; }

            public string? Number { get; set; }

            public DateOnly? WithdrawDate { get; set; }
        }

        public class EmployeeInfo
        {
            [Required]
            public string Surname { get; set; }

            public string? OverrideSurname { get; set; }

            [Required]
            public string Name { get; set; }

            public string? OverrideName { get; set; }

            public string? Partonymic { get; set; }

            public string? OverridePatronymic { get; set; }

            public DateOnly? BirthDate { get; set; }

            public PassportInfo? OverrideReason { get; set; }
        }

        public class SpecializationInfo
        {
            public string? Education { get; set; }

            public string? Profession { get; set; }
        }
    }
}
