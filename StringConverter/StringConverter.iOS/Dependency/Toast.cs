using StringConverter.Dependency;
using StringConverter.iOS.Dependency;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Toast_Ios))]
namespace StringConverter.iOS.Dependency
{
    public class Toast_Ios : IToast
    {
        public void Toast(string message)
        {
            var controller = UIApplication.SharedApplication.GetTopViewController();
            var toastLabel = new UITextView(new CoreGraphics.CGRect(controller.View.Frame.Size.Width / 16, controller.View.Frame.Size.Height - 150, controller.View.Frame.Size.Width * 7 / 8, 35));
            toastLabel.BackgroundColor = UIColor.Black.ColorWithAlpha((nfloat)0.6);
            toastLabel.TextColor = UIColor.White;
            toastLabel.TextAlignment = UITextAlignment.Center;
            toastLabel.Text = message;
            toastLabel.Alpha = (nfloat)1.0;
            toastLabel.Layer.CornerRadius = 18;
            toastLabel.ClipsToBounds = true;
            toastLabel.Font = UIFont.PreferredSubheadline;
            toastLabel.AdjustsFontForContentSizeCategory = true;
            controller.View.AddSubview(toastLabel);
            UIView.Animate(5.0, 0.1, UIViewAnimationOptions.CurveEaseOut, () =>
            {
                toastLabel.Alpha = (nfloat)0.0;
            },
            () =>
            {
                toastLabel.RemoveFromSuperview();
            });
        }
    }
    public static class UiViewControllerExt
    {
        public static UIViewController GetTopViewController(this UIApplication app)
        {
            var viewController = app.KeyWindow.RootViewController;
            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            return viewController;
        }
    }
}