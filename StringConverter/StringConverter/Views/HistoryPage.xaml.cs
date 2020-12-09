using StringConverter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StringConverter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            HistoryViewModel historyViewModel = new HistoryViewModel();
            historyViewModel.Navigation = Navigation;
            BindingContext = historyViewModel;
        }

        protected override void OnAppearing()
        {
            ((BaseViewModel)BindingContext).OnLoadAsync();
            base.OnAppearing();
        }
    }
}