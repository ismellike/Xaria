using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Xaria
{
    /// <summary>
    /// The android activity
    /// </summary>
    /// <seealso cref="Microsoft.Xna.Framework.AndroidGameActivity" />
    [Activity(Label = "Xaria"
            , MainLauncher = true
            , Icon = "@drawable/icon"
            , Theme = "@style/Theme.Splash"
            , AlwaysRetainTaskState = true
            , LaunchMode = LaunchMode.SingleInstance
            , ScreenOrientation = ScreenOrientation.Portrait 
            , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        /// <summary>
        /// Android activity creation event
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new Game1();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

