using StringConverter.Models;
using StringConverter.Services;
using StringConverter.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StringConverter.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private History selectHistory;
        private bool isLoad = false;

        public HistoryViewModel()
        {
            RemoveCommand = new Command(ExecuteRemoveCommand);
        }

        public List<History> Histories { get; set; }

        public History SelectHistory
        {
            get => selectHistory; set
            {
                selectHistory = value;
                if (value == null) return;
                bool isEncoded = false;
                switch (value.Method)
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
                switch (value.Method)
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
                    case 30:
                    case 31:
                        Navigation.PushAsync(new NormalCodePage(value.Method, isEncoded, value));
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 25:
                    case 26:
                        Navigation.PushAsync(new PasswordCodePage(value.Method, isEncoded, value));
                        break;
                    default:
                        break;
                }
            }
        }

        public Command RemoveCommand { get; set; }
        private async void ExecuteRemoveCommand(object obj)
        {
            History history = obj as History;
            if (history == null) return;
            await SQLiteProvider.Instace.Delete(history);
            Histories = await SQLiteProvider.Instace.GetAllHistory();
            OnPropertyChanged("Histories");
        }
        private async Task LoadData()
        {
            Histories = await SQLiteProvider.Instace.GetAllHistory();
            OnPropertyChanged("Histories");
        }
        public override async Task OnLoadAsync()
        {
            if (!SQLiteProvider.Instace.IsInit)
            {
                await SQLiteProvider.Instace.InitDatabase();
            }
            if (!isLoad)
            {
                await LoadData();
                isLoad = true;
            }
        }
    }
}
