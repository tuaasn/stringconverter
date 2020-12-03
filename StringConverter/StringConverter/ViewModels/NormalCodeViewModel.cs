using StringConverter.Utility;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace StringConverter.ViewModels
{
    public class NormalCodeViewModel : BaseViewModel
    {
        private string sourceText;
        private string destinationText;
        private bool isEncoded;

        public NormalCodeViewModel()
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
        public int FunctionCode { get; set; }

        public Command ProcessCommand { get; set; }
        public Command CopyCommand { get; set; }
        public virtual void ExecuteProcessCommand()
        {
            DestinationText = ConvertTool.ConvertNormal(FunctionCode, SourceText);
        }
        private async void ExecuteCopyCommand()
        {
            await Clipboard.SetTextAsync(destinationText);
        }

        public override async Task OnLoadAsync()
        {
            if (Clipboard.HasText)
            {
                SourceText = await Clipboard.GetTextAsync();
            }
        }
        public bool IsEncoded
        {
            get => isEncoded; set
            {
                SetProperty(ref isEncoded, value);
            }
        }

        public string EncodedState
        {
            get
            {
                return isEncoded ? "Encode" : "Decode";
            }
        }
    }
}
