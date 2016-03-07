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
		Database db;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
	
			SetContentView (Resource.Layout.Main);

			db = new Database (this);

			Button totems = FindViewById<Button> (Resource.Id.totems);
			Button totemBepalen = FindViewById<Button> (Resource.Id.totemBepalen);
			Button profielen = FindViewById<Button> (Resource.Id.profielen);

			totems.Click += (sender, eventArgs) => GoToActivity("totems");
			totemBepalen.Click += (sender, eventArgs) => GoToActivity("bepalen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
		}

		private void GoToActivity(string a) {
			if(a.Equals("totems")) {
				var intent = new Intent(this, typeof(AllTotemsActivity));
				intent.PutExtra ("totemIDs", db.AllTotemIDs());
				StartActivity (intent);
			} else if(a.Equals("bepalen")) {
				var intent = new Intent(this, typeof(EigenschappenActivity));
				StartActivity (intent);
			} else if(a.Equals("profielen")) {
				var intent = new Intent(this, typeof(ProfielenActivity));
				StartActivity (intent);
			}

		}
	}
}


