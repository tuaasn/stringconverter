using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace StringConverter.Controls
{
    public class LabelExtension : Label
    {
        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create("ClickCommand", typeof(Command), typeof(LabelExtension), null, BindingMode.OneWay);
        public static readonly BindableProperty FunctionCodeProperty = BindableProperty.Create("FunctionCode", typeof(object), typeof(LabelExtension), null, BindingMode.OneWay);
        public Command ClickCommand
        {
            get
            {
                return (Command)GetValue(ClickCommandProperty);
            }
            set
            {
                SetValue(ClickCommandProperty, value);
            }
        }
        public object FunctionCode
        {
            get
            {
                return GetValue(FunctionCodeProperty);
            }
            set
            {
                SetValue(FunctionCodeProperty, value);
            }
        }

        private TapGestureRecognizer tapGesture;

        public LabelExtension()
        {
            tapGesture = new TapGestureRecognizer();
            tapGesture.Command = ClickCommand;
            GestureRecognizers.Add(tapGesture);
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(propertyName == "ClickCommand")
            {
                tapGesture.Command = ClickCommand;
            } 
            else if(propertyName == "FunctionCode")
            {
                tapGesture.CommandParameter = FunctionCode;
            }
        }
    }
}
