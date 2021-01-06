using Android.Widget;
using StringConverter.Dependency;
using StringConverter.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(Toast_Android))]
namespace StringConverter.Droid.Dependency
{
    public class Toast_Android : IToast
    {
        void IToast.Toast(string message)
        {
            //TextView textView = new TextView(Android.App.Application.Context);
            //textView.SetText(message, TextView.BufferType.Normal);
            //textView.SetTextColor(Android.Graphics.Color.White);
            //textView.TextAlignment = Android.Views.TextAlignment.TextEnd;
            //textView.Alpha = 0.6f;
            var toast = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            //toast.View = textView;
            //toast.View.SetBackgroundResource(Resource.Drawable.ToastView);
            toast.Show();
        }
    }
}