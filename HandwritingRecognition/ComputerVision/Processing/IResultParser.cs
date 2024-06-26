﻿// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using HandwritingRecognition.Data;

namespace HandwritingRecognition.ComputerVision.Processing
{
    /// <summary>
    /// Represents an interface for the recognition result parsing service.
    /// </summary>
    public interface IResultParser
    {
        /// <summary>
        /// Parses the recognition result record and adds the data into the history unit.
        /// </summary>
        /// <param name="input">Input recognition result.</param>
        /// <param name="history">The instance of the <see cref="EmploymentHistory"/> class to set values to.</param>
        /// <param name="currentPageLabel">Determines in whether page the record was found.</param>
        void Parse(ImageRecognitionRecord input, EmploymentHistory history, ImageRecognitionRecord.LabelClass currentPageLabel);
    }
}
