using StringConverter.Models;
using StringConverter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StringConverter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NormalCodePage : ContentPage
    {
        public NormalCodePage(int functionCode, bool isEncoded, History history = null)
        {
            InitializeComponent();
            NormalCodeViewModel viewModel = new NormalCodeViewModel();
            viewModel.Navigation = Navigation;
            viewModel.FunctionCode = functionCode;
            viewModel.IsEncoded = isEncoded;
            viewModel.History = history;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            ((BaseViewModel)BindingContext).OnLoadAsync();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            ((BaseViewModel)BindingContext).UnLoadAsync();
            base.OnDisappearing();
        }
    }
}