using StringConverter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StringConverter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.Navigation = Navigation;
            BindingContext = mainViewModel;
        }
    }
}