using StringConverter.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StringConverter.ViewModels
{
    public class TextStyleViewModel : BaseViewModel
    {
        private string sourceText;
        private string destinationText;
        public TextStyleViewModel()
        {
            ProcessCommand = new Command(ExecuteProcessCommand);
            CopyCommand = new Command(ExecuteCopyCommand);
        }

        public string SourceText
        {
            get => sourceText; set
            {
                SetProperty(ref sourceText, value);
            }
        }
        public string DestinationText
        {
            get => destinationText; set
            {
                SetProperty(ref destinationText, value);
            }
        }

        public Command ProcessCommand { get; set; }
        public Command CopyCommand { get; set; }
        public void ExecuteProcessCommand()
        {
            DestinationText = ConvertTool.ConvertAscii(SourceText);
        }
        private async void ExecuteCopyCommand()
        {
            await Clipboard.SetTextAsync(destinationText);
        }
    }
}
