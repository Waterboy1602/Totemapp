using System;
using System.IO;
using System.Collections.Generic;

using SQLite;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Content.PM;

namespace Totem
{
	[Activity (Label = "Totem", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//RequestWindowFeature(WindowFeatures.NoTitle);
	
			SetContentView (Resource.Layout.Main);

			Button totems = FindViewById<Button> (Resource.Id.totems);
			Button totemBepalen = FindViewById<Button> (Resource.Id.totemBepalen);
			Button profielen = FindViewById<Button> (Resource.Id.profielen);

			totems.Click += (sender, eventArgs) => GoToActivity("totems");
			totemBepalen.Click += (sender, eventArgs) => GoToActivity("bepalen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
		}

		private void GoToActivity(string a) {
			Intent intent = null;
			if(a.Equals("totems")) {
				intent = new Intent(this, typeof(AllTotemsActivity));
			} else if(a.Equals("bepalen")) {
				intent = new Intent(this, typeof(EigenschappenActivity));
			} else if(a.Equals("profielen")) {
				intent = new Intent(this, typeof(ProfielenActivity));
			}
			StartActivity (intent);
		}
	}
}