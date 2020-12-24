using StringConverter.Views;
using Xamarin.Forms;

namespace StringConverter.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ClickLabelCommand = new Command(ExecuteClickLabelCommand);
        }

        public Command ClickLabelCommand { get; set; }
        private void ExecuteClickLabelCommand(object obj)
        {
            if (obj == null) return;
            int function = 0;
            int.TryParse(obj.ToString(), out function);
            bool isEncoded = false;
            switch (function)
            {
                case 2:
                case 4:
                case 6:
                case 7:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 10:
                case 12:
                case 14:
                case 26:
                case 29:
                    isEncoded = true;
                    break;
                default:
                    break;
            }
            switch (function)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 28:
                case 29:
                    Navigation.PushModalAsync(new NormalCodePage(function, isEncoded));
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 25:
                case 26:
                    Navigation.PushModalAsync(new PasswordCodePage(function, isEncoded));
                    break;
                case 27:
                    Navigation.PushModalAsync(new TextStylePage());
                    break;
                default:
                    break;
            }
        }
    }
}
