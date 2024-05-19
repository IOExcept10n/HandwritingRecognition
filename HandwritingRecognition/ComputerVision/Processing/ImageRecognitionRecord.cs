// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.Runtime.Serialization;

namespace HandwritingRecognition.ComputerVision.Processing
{
    /// <summary>
    /// Represents a class for storing an image recognition record.
    /// </summary>
    public class ImageRecognitionRecord
    {
        /// <summary>
        /// Defines an enum for the label image classification.
        /// </summary>
        public enum LabelClass
        {
            /// <summary>
            /// Label for the name field.
            /// </summary>
            Name,

            /// <summary>
            /// Label for the surname field.
            /// </summary>
            Surname,

            /// <summary>
            /// Label for the stamp.
            /// </summary>
            Stamp,

            /// <summary>
            /// Label for the patronymic.
            /// </summary>
            Patronymic,

            /// <summary>
            /// Label for the birthdate.
            /// </summary>
            Birthdate,

            /// <summary>
            /// Label for the date of the history creation.
            /// </summary>
            [EnumMember(Value = "date-of-creation")]
            CreationDate,

            /// <summary>
            /// First page of the history.
            /// </summary>
            [EnumMember(Value = "first-page")]
            First_Page,

            /// <summary>
            /// Job history page.
            /// </summary>
            [EnumMember(Value = "job-history")]
            Job_History,

            /// <summary>
            /// Reward history page.
            /// </summary>
            [EnumMember(Value = "reward-history")]
            Reward_History,

            /// <summary>
            /// Simple string to represent a single text row in table.
            /// </summary>
            Str,
        }

        /// <summary>
        /// Gets or sets information about the record bounds.
        /// </summary>
        public int[] Hitbox { get; set; } = [];

        /// <summary>
        /// Gets or sets information about the record class.
        /// </summary>
        public LabelClass Class { get; set; }

        /// <summary>
        /// Gets or sets information about the text.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}