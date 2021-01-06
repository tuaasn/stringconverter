using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;

namespace StringConverter.Droid
{
    [Activity(Theme = "@style/StringConverter.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}