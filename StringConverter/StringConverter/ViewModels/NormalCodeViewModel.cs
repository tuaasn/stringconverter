using StringConverter.Dependency;
using StringConverter.Models;
using StringConverter.Resources;
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
            PasteCommand = new Command(ExecutePasteCommand);
            CleanCommand = new Command(ExecuteCleanCommand);
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
        public Command PasteCommand { get; set; }
        public Command CleanCommand { get; set; }
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
            DependencyService.Get<IToast>().Toast("Copied");
        }

        private async void ExecutePasteCommand()
        {
            SourceText = await Clipboard.GetTextAsync();
        }
        private void ExecuteCleanCommand()
        {
            SourceText = "";
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
                switch (FunctionCode)
                {
                    case 21:
                        return Resource.ReverseString;
                    case 22:
                        return Resource.UpperCaseString;
                    case 23:
                        return Resource.LowerCaseString;
                    case 24:
                        return Resource.TitleCaseString;
                }
                return isEncoded ? "Encode" : "Decode";
            }
        }
    }
}
