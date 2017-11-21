using Android.App;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
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
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity, ISensorEventListener
    {
        public static SensorManager sensorManager;
        private Sensor accelerometer;
        private Sensor magnetometer;
        public static float[] accelValues = new float[3];
        public static float[] magnetoValues = new float[3];

        /// <summary>
        /// Android activity creation event
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new Game1();
            SetContentView((View)g.Services.GetService(typeof(View)));
            sensorManager = (SensorManager)GetSystemService(Android.Content.Context.SensorService);
            accelerometer = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
            magnetometer = sensorManager.GetDefaultSensor(SensorType.MagneticField);
            sensorManager.RegisterListener(this, accelerometer, SensorDelay.Game);
            sensorManager.RegisterListener(this, magnetometer, SensorDelay.Game);
            g.Run();
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            switch (e.Sensor.Type)
            {
                case SensorType.Accelerometer:
                    e.Values.CopyTo(accelValues, 0);
                    break;

                case SensorType.MagneticField:
                    e.Values.CopyTo(magnetoValues, 0);
                    break;
            }
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if(keyCode == Keycode.Back)
            {
                return true;
            }
            return false;
        }
    }
}

