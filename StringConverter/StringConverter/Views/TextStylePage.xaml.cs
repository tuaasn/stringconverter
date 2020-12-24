using StringConverter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StringConverter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextStylePage : ContentPage
    {
        public TextStylePage()
        {
            InitializeComponent();
            TextStyleViewModel viewModel = new TextStyleViewModel();
            BindingContext = viewModel;
        }
    }
}