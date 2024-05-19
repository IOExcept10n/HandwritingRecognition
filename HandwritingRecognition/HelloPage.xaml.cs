// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HandwritingRecognition.ViewModel;

namespace HandwritingRecognition
{
    /// <summary>
    /// Логика взаимодействия для HelloPage.xaml
    /// </summary>
    public partial class HelloPage : Page
    {
        private EditorViewModel editor;

        public HelloPage()
        {
            InitializeComponent();
            editor = EditorViewModel.Instance;
        }

        private void ShowHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = @"https://github.com/IOExcept10n/HandwritingRecognition/blob/main/Help.md",
                UseShellExecute = true
            });
        }

        private void OpenEditorButton_Click(object sender, RoutedEventArgs e)
        {
            editor.CreateConfig();
            NavigateToEditor();
        }

        private void NavigateToEditor()
        {
            NavigationService.Navigate(new EditorPage());
        }

        private void LoadPNGButton_Click(object sender, RoutedEventArgs e)
        {
            editor.CreateConfig();
            var t = editor.ImportImages();
            if (!t.IsCompleted || !t.Result)
            {
                NavigateToEditor();
            }
        }

        private void LoadPDFButton_Click(object sender, RoutedEventArgs e)
        {
            editor.CreateConfig();
            var t = editor.ImportPDF();
            if (!t.IsCompleted || !t.Result)
            {
                NavigateToEditor();
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                editor.CreateConfig();
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                Task<bool> t;
                if (files.Length == 1 && Path.GetExtension(files[0]) == ".pdf")
                {
                    t = editor.ImportPDF(files[0]);
                }
                else
                {
                    t = editor.ImportImages(files);
                }

                if (!t.IsCompleted || !t.Result)
                {
                    NavigateToEditor();
                }
            }
        }
    }
}
