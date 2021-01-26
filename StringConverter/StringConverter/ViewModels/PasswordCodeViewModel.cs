using StringConverter.Services;
using StringConverter.Utility;
using System.Threading.Tasks;

namespace StringConverter.ViewModels
{
    public class PasswordCodeViewModel : NormalCodeViewModel
    {
        private string password;
        public string Password { get => password; set => SetProperty(ref password, value); }
        public override async void ExecuteProcessCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(password)) return;
                DestinationText = ConvertTool.ConvertIncludePassword(FunctionCode, SourceText, Password);
                if (history == null) history = new Models.History();
                history.SrcText = SourceText;
                history.DesText = DestinationText;
                history.Password = password;
                history.Method = FunctionCode;
                await SQLiteProvider.Instace.InsertOrUpdate(history);
                IsError = false;
            }
            catch
            {
                IsError = true;
            }

        }

        public override async Task OnLoadAsync()
        {
            if (history != null)
            {
                SourceText = history.SrcText;
                password = history.Password;
                FunctionCode = history.Method;
                DestinationText = history.DesText;
            }
            if (!SQLiteProvider.Instace.IsInit)
            {
                await SQLiteProvider.Instace.InitDatabase();
            }
        }
    }
}
