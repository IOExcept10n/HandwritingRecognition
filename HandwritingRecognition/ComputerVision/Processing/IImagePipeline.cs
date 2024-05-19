using System.Windows.Media.Imaging;
using HandwritingRecognition.Data;

namespace HandwritingRecognition.ComputerVision.Processing
{
    internal interface IImagePipeline
    {
        Task ProcessAsync(string[] imagePaths, EmploymentHistory target, IProgress<ProgressInfo> progress);
    }

    public readonly struct ProgressInfo
    {
        public readonly int TotalCount;
        public readonly int CurrentStep;
        public readonly string Details;

        public ProgressInfo(int totalCount, int currentStep, string details)
        {
            TotalCount = totalCount;
            CurrentStep = currentStep;
            Details = details;
        }
    }
}
