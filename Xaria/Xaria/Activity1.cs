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
    /// <seealso cref="Android.Hardware.ISensorEventListener" />
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
        /// <summary>
        /// The sensor manager
        /// </summary>
        public static SensorManager sensorManager;
        /// <summary>
        /// The accelerometer
        /// </summary>
        private Sensor accelerometer;
        /// <summary>
        /// The magnetometer
        /// </summary>
        private Sensor magnetometer;
        /// <summary>
        /// The accel values
        /// </summary>
        public static float[] accelValues = new float[3];
        /// <summary>
        /// The magneto values
        /// </summary>
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

        /// <summary>
        /// Called when the accuracy of a sensor has changed.
        /// </summary>
        /// <param name="sensor">To be added.</param>
        /// <param name="accuracy">The new accuracy of this sensor</param>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the accuracy of a sensor has changed.
        /// </para>
        /// <para tool="javadoc-to-mdoc">See <c><see cref="T:Android.Hardware.SensorManager" /></c>
        /// for details.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/hardware/SensorEventListener.html#onAccuracyChanged(android.hardware.Sensor, int)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 3" />
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        /// <summary>
        /// Called when sensor values have changed.
        /// </summary>
        /// <param name="e">the <c><see cref="T:Android.Hardware.SensorEvent" /></c>.</param>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when sensor values have changed.
        /// </para>
        /// <para tool="javadoc-to-mdoc">See <c><see cref="T:Android.Hardware.SensorManager" /></c>
        /// for details on possible sensor types.
        /// </para>
        /// <para tool="javadoc-to-mdoc">See also <c><see cref="T:Android.Hardware.SensorEvent" /></c>.
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <b>NOTE:</b>
        ///   </format> The application doesn't own the
        /// <c><see cref="T:Android.Hardware.SensorEvent" /></c>
        /// object passed as a parameter and therefore cannot hold on to it.
        /// The object may be part of an internal pool and may be reused by
        /// the framework.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/hardware/SensorEventListener.html#onSensorChanged(android.hardware.SensorEvent)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 3" />
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

        /// <summary>
        /// Called when a key was released and not handled by any of the views
        /// inside of the activity.
        /// </summary>
        /// <param name="keyCode">The value in event.getKeyCode().</param>
        /// <param name="e">Description of the key event.</param>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when a key was released and not handled by any of the views
        /// inside of the activity. So, for example, key presses while the cursor
        /// is inside a TextView will not trigger the event (unless it is a navigation
        /// to another object) because TextView handles its own key presses.
        /// </para>
        /// <para tool="javadoc-to-mdoc">The default implementation handles KEYCODE_BACK to stop the activity
        /// and go back.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/app/Activity.html#onKeyUp(int, android.view.KeyEvent)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        /// <altmember cref="M:Android.App.Activity.OnKeyDown(Android.Views.Keycode, Android.Views.KeyEvent)" />
        /// <altmember cref="T:Android.Views.KeyEvent" />
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