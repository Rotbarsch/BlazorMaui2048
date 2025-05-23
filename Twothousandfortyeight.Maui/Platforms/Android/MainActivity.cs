using Android.App;
using Android.Content.PM;
using Android.Views;

namespace Twothousandfortyeight.Maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public override bool OnKeyUp(Keycode keyCode, KeyEvent? e)
    {
        KeyHandler.KeyHandler.InvokeKeyUpEvent(keyCode, e);
        return true;
    }

}