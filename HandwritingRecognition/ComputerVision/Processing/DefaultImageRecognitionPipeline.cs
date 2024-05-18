using HandwritingRecognition.Data;
using System;
using System.Diagnostics;

namespace HandwritingRecognition.ComputerVision.Processing
{
    internal class DefaultImageRecognitionPipeline : IImagePipeline
    {
        private const string MagicScript = "G:\\Work\\Projects\\Cloned\\shiftlab_ocr\\interop.py";

        private IResultParser _parser;

        public async Task ProcessAsync(string[] imagePaths, EmploymentHistory target, IProgress<ProgressInfo> progress)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                Arguments = MagicScript,
                FileName = "python",
                // can only redirect STDIO when UseShellExecute=false
                UseShellExecute = false,
                RedirectStandardOutput = true,
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
            process.BeginOutputReadLine();
            for (int i = 0; i < imagePaths.Length; i++)
            {
                progress.Report(new(imagePaths.Length, i, $"Processing image {i}..."));
                string? image = imagePaths[i];
                await process.StandardInput.WriteLineAsync(image);
                string? response = await GetResponseAsync(process);
            }
        }

        private async Task<string> GetResponseAsync(Process p)
        {
            string? response = await p.StandardOutput.ReadLineAsync();
            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("Failed to get response.");
                throw new InvalidOperationException();
            }

            return response;
        }
    }
}
