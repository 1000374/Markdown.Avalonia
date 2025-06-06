using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Avalonia.Platform;
using System.Collections.Generic;
using System;
using Markdown.Avalonia;
using System.Xml.Linq;

namespace TestMD.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (!Equals(_text, value))
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private string _edittingStyleXamlText;
        public string EdittingStyleXamlText
        {
            get => _edittingStyleXamlText;
            set
            {
                if (!Equals(_edittingStyleXamlText, value))
                {
                    _edittingStyleXamlText = value;
                    OnPropertyChanged(nameof(EdittingStyleXamlText));
                }
            }
        }

        private string _appendStyleXamlText;
        public string AppendStyleXamlText
        {
            get => _appendStyleXamlText;
            set
            {
                if (!Equals(_appendStyleXamlText, value))
                {
                    _appendStyleXamlText = value;
                    OnPropertyChanged(nameof(AppendStyleXamlText));
                }
            }
        }

        private StyleViewModel _selectedStyle;
        public StyleViewModel SelectedStyle
        {
            get => _selectedStyle;
            set
            {
                if (!Equals(_selectedStyle, value))
                {
                    _selectedStyle = value;
                    OnPropertyChanged(nameof(SelectedStyle));
                }
            }
        }

        private string _ErrorInfo;
        public string ErrorInfo
        {
            get => _ErrorInfo;
            set
            {
                if (!Equals(_ErrorInfo, value))
                {
                    _ErrorInfo = value;
                    OnPropertyChanged(nameof(_ErrorInfo));
                }
            }
        }

        public List<StyleViewModel> Styles { set; get; }

        public void XamlParseResult(string result) => ErrorInfo = result;

        public void TryParse() => AppendStyleXamlText = EdittingStyleXamlText;

        public MainWindowViewModel()
        {
            try
            {
                using (var stream = new FileStream("MainWindow.md", FileMode.Open))
                using (var reader = new StreamReader(stream))
                {
                    Text = reader.ReadToEnd();
                }
            }
            catch { }

            Styles = new List<StyleViewModel>
            {
                new StyleViewModel() { Name = nameof(MarkdownStyle.Standard) },
                new StyleViewModel() { Name = nameof(MarkdownStyle.SimpleTheme) },
                new StyleViewModel() { Name = nameof(MarkdownStyle.GithubLike) }
            };

            SelectedStyle = Styles[1];

            using (var strm = AssetLoader.Open(new Uri("avares://TestMD/Assets/XamlTemplate.txt")))
            using (var reader = new StreamReader(strm))
            {
                EdittingStyleXamlText = reader.ReadToEnd();
            }
        }
    }

    public class StyleViewModel
    {
        public string Name { get; set; }
    }
}
