using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Data
{
    public class PassportInfo
    {
        public string DocumentType { get; set; }

        public string? Series { get; set; }

        public string? Number { get; set; }

        public DateOnly? AcquireDate { get; set; }

        public string? GivenBy { get; set; }
    }
}
