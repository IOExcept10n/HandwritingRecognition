using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    public class RewardHistoryRecord
    {
        public DateOnly? Date { get; set; }

        public string? StampInfo { get; set; }

        public string? Details { get; set; }

        public string? OrderInfo { get; set; }
    }
}
