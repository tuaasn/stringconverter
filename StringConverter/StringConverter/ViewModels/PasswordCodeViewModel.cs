using StringConverter.Utility;
using System.Threading.Tasks;

namespace StringConverter.ViewModels
{
    public class PasswordCodeViewModel : NormalCodeViewModel
    {
        private string password;
        public string Password { get => password; set => SetProperty(ref password, value); }
        public override void ExecuteProcessCommand()
        {
            if (string.IsNullOrEmpty(password)) return;
            DestinationText = ConvertTool.ConvertIncludePassword(FunctionCode, SourceText, Password);
        }

        public override Task OnLoadAsync()
        {
            return Task.CompletedTask;
            //return base.OnLoadAsync();
        }
    }
}
