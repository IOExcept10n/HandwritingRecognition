using HandwritingRecognition.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.ComputerVision.Processing
{
    public interface IResultParser
    {
        void Parse(string input, EmploymentHistory history);
    }
}
