using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Totem
{
	[Activity (Label = "Totem", MainLauncher = false, NoHistory = true, Theme = "@android:style/Theme.Material.Light.NoActionBar")]			
	public class SplashScreenActivity : Activity
	{
		private static int SPLASH_TIME = 2000;
		private Handler mHandler;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			RequestWindowFeature(WindowFeatures.NoTitle);
			SetContentView (Resource.Layout.SplashScreen);

			Java.Lang.Runnable mSplash = new Java.Lang.Runnable(() =>
				{
					var intent = new Intent(this, typeof(MainActivity));
					StartActivity(intent);
					Finish();
				});

			mHandler = new Handler ();
			mHandler.PostDelayed (mSplash, SPLASH_TIME);

		}
	}
}