using HandwritingRecognition.Data;
using System;
using System.Diagnostics;

namespace HandwritingRecognition.ComputerVision.Processing
{
    internal class DefaultImageRecognitionPipeline : IImagePipeline
    {
        private const string MagicScript = "G:\\Work\\Projects\\Cloned\\shiftlab_ocr\\interop.py";

        private IResultParser _parser;

        public Task ProcessAsync(string[] imagePaths, EmploymentHistory target, IProgress<ProgressInfo> progress)
        {
            return Task.Run(() =>
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                {
                    Arguments = MagicScript,
                    FileName = "python",
                    // can only redirect STDIO when UseShellExecute=false
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                };
                using var process = new Process()
                {
                    StartInfo = processStartInfo,
                };
                if (!process.Start())
                {
                    Console.WriteLine("Process failed to start.");

                    return;
                }

                for (int i = 0; i < imagePaths.Length; i++)
                {
                    progress.Report(new(imagePaths.Length, i, $"Processing image {i}..."));
                    string? image = imagePaths[i];
                    process.StandardInput.WriteLine(image);
                    string? response = GetResponse(process);
                }
            });
        }

        private string GetResponse(Process p)
        {
            string? response = p.StandardOutput.ReadLine();
            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("Failed to get response.");
                throw new InvalidOperationException();
            }

            return response;
        }
    }
}
