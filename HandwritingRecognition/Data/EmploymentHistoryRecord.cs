using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    public class EmploymentHistoryRecord
    {
        public DateOnly? EmploymentDate { get; set; }

        public DateOnly? ExpellDate { get; set; }

        public string? PrintInfo { get; set; }

        public string? Position { get; set; }

        public string? OrderInfo { get; set; }
    }
}
