// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using HandwritingRecognition.Data;

namespace HandwritingRecognition.ComputerVision.Processing
{
    /// <summary>
    /// Represents a default implementation for the <see cref="IResultParser"/> interface.
    /// </summary>
    internal class ResultParser : IResultParser
    {
        /// <inheritdoc/>
        public void Parse(ImageRecognitionRecord input, EmploymentHistory history, ImageRecognitionRecord.LabelClass currentPageLabel)
        {
            switch (input.Class)
            {
                case ImageRecognitionRecord.LabelClass.Name:
                    history.Info.Name = input.Text;
                    break;
                case ImageRecognitionRecord.LabelClass.Birthdate:
                    if (DateTime.TryParse(input.Text, out var r))
                    {
                        history.Info.BirthDate = new DateOnly(r.Year, r.Month, r.Day);
                    }

                    break;
                case ImageRecognitionRecord.LabelClass.Surname:
                    history.Info.Surname = input.Text;
                    break;
                case ImageRecognitionRecord.LabelClass.Stamp:
                    history.HistoryDefinition.StampInfo = input.Text;
                    break;
                case ImageRecognitionRecord.LabelClass.Patronymic:
                    history.Info.Patronymic = input.Text;
                    break;
                case ImageRecognitionRecord.LabelClass.CreationDate:
                    if (DateTime.TryParse(input.Text, out r))
                    {
                        history.HistoryDefinition.WithdrawDate = new DateOnly(r.Year, r.Month, r.Day);
                    }

                    break;
                case ImageRecognitionRecord.LabelClass.Str:
                    if (currentPageLabel == ImageRecognitionRecord.LabelClass.Reward_History)
                    {
                        history.Rewards.Add(new RewardHistoryRecord() { Details = input.Text });
                    }
                    else if (currentPageLabel == ImageRecognitionRecord.LabelClass.Job_History)
                    {
                        history.Jobs.Add(new EmploymentHistoryRecord() { Details = input.Text });
                    }

                    break;
            }
        }
    }
}
