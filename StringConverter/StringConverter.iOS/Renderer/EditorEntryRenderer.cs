using StringConverter.Controls;
using StringConverter.iOS.Renderer;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EditEntry), typeof(EditorEntryRenderer))]
namespace StringConverter.iOS.Renderer
{
    public class EditorEntryRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                CreateControl();
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "BorderColorProperty" && Element != null)
            {
                CreateControl();
            }
        }
        private void CreateControl()
        {
            var view = (EditEntry)Element;

            //Control.View = new UIView(new CGRect(0f, 0f, 9f, 20f));
            //Control.LeftViewMode = UITextFieldViewMode.Always;

            Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
            Control.ReturnKeyType = UIReturnKeyType.Done;
            // Radius for the curves  
            Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
            // Thickness of the Border Color  
            Control.Layer.BorderColor = view.BorderColor.ToCGColor();
            // Thickness of the Border Width  
            Control.Layer.BorderWidth = view.BorderWidth;
            Control.ClipsToBounds = true;
        }
    }
}