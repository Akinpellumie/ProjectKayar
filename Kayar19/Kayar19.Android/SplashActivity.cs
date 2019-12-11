using Android.App;
using Android.OS;
using Kayar19.Droid;

namespace XF_SplashScreen.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(1000);
            StartActivity(typeof(MainActivity));
        }
    }
}