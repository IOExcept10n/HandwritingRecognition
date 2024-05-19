using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using HandwritingRecognition.Data;
using Newtonsoft.Json;

namespace HandwritingRecognition.ComputerVision.Processing
{
    /// <summary>
    /// Represents a default service for the image recognition. Due to time limits we decided to make it client-server based.
    /// </summary>
    internal class DefaultImageRecognitionPipeline : IImagePipeline
    {
        private const string Host = "localhost";
        private const int Port = 42513;
        private static readonly HttpClient Client = new();
        private static readonly IResultParser Parser = new ResultParser();

        /// <inheritdoc/>
        public async Task ProcessAsync(string[] imagePaths, EmploymentHistory target, IProgress<ProgressInfo> progress)
        {
            try
            {
                var args = Encoding.UTF8.GetBytes(string.Join(';', imagePaths));
                string url = $"http://{Host}:{Port}/process";
                HttpContent content = new ByteArrayContent(args);
                var response = await Client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, ImageRecognitionRecord[]>>(responseString) ?? [];
                foreach (var image in imagePaths)
                {
                    if (result.TryGetValue(image, out var records))
                    {
                        ImageRecognitionRecord.LabelClass pageClass;
                        if (records.Any(x => x.Class == ImageRecognitionRecord.LabelClass.First_Page))
                        {
                            pageClass = ImageRecognitionRecord.LabelClass.First_Page;
                        }
                        else if (records.Any(x => x.Class == ImageRecognitionRecord.LabelClass.Reward_History))
                        {
                            pageClass = ImageRecognitionRecord.LabelClass.Reward_History;
                        }
                        else
                        {
                            pageClass = ImageRecognitionRecord.LabelClass.Job_History;
                        }

                        foreach (var record in records)
                        {
                            Parser.Parse(record, target, pageClass);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при распознавании текста: {ex.Message}.");
            }
        }
    }
}