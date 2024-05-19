using System.Windows.Media.Imaging;
using HandwritingRecognition.Data;

namespace HandwritingRecognition.ComputerVision.Processing
{
    /// <summary>
    /// Represents an interface for the image text recognition pipeline. It should perform image processing and then fill the target properties.
    /// </summary>
    internal interface IImagePipeline
    {
        /// <summary>
        /// Performs the images processing and fills up the employment history asynchronously.
        /// </summary>
        /// <param name="imagePaths">Array of paths to the images to process.</param>
        /// <param name="target">Target employment history to fill-up.</param>
        /// <param name="progress">Progress tracker (under development).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ProcessAsync(string[] imagePaths, EmploymentHistory target, IProgress<ProgressInfo> progress);
    }

    /// <summary>
    /// Represents the information about pending task to track progress.
    /// </summary>
    public readonly struct ProgressInfo
    {
        /// <summary>
        /// Total count of steps to complete an operation.
        /// </summary>
        public readonly int TotalCount;

        /// <summary>
        /// Number of the current step.
        /// </summary>
        public readonly int CurrentStep;

        /// <summary>
        /// Display information about the process.
        /// </summary>
        public readonly string Details;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressInfo"/> struct.
        /// </summary>
        /// <param name="totalCount">Total count of steps to complete an operation.</param>
        /// <param name="currentStep">Number of the current step.</param>
        /// <param name="details">Display information about the process.</param>
        public ProgressInfo(int totalCount, int currentStep, string details)
        {
            TotalCount = totalCount;
            CurrentStep = currentStep;
            Details = details;
        }
    }
}
