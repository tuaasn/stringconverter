using StringConverter.Models;
using StringConverter.Services;
using StringConverter.Utility;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StringConverter.ViewModels
{
    public class NormalCodeViewModel : BaseViewModel
    {
        private string sourceText;
        private string destinationText;
        private bool isEncoded;
        protected History history;

        public NormalCodeViewModel()
        {
            ProcessCommand = new Command(ExecuteProcessCommand);
            CopyCommand = new Command(ExecuteCopyCommand);
        }
        public History History { get => history; set => SetProperty(ref history, value); }

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
        public virtual async void ExecuteProcessCommand()
        {
            DestinationText = ConvertTool.ConvertNormal(FunctionCode, SourceText);
            if (history == null) history = new History();
            history.SrcText = SourceText;
            history.DesText = destinationText;
            history.Method = FunctionCode;
            await SQLiteProvider.Instace.InsertOrUpdate(history);
        }
        private async void ExecuteCopyCommand()
        {
            await Clipboard.SetTextAsync(destinationText);
        }

        public override async Task OnLoadAsync()
        {
            IsBusy = true;
            if(history != null)
            {
                SourceText = history.SrcText;
                FunctionCode = history.Method;
                DestinationText = history.DesText;
            }
            else if (Clipboard.HasText)
            {
                SourceText = await Clipboard.GetTextAsync();
            }

            if (!SQLiteProvider.Instace.IsInit)
            {
                await SQLiteProvider.Instace.InitDatabase();
            }
            IsBusy = false;
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
