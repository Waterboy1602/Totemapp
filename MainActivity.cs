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

namespace Totem {
	[Activity (Label = "Totem", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppThemeNoAction")]
	public class MainActivity : Activity 	{
		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			db = DatabaseHelper.GetInstance (this);

			SetContentView (Resource.Layout.Main);

			Button totems = FindViewById<Button> (Resource.Id.totems);
			Button eigenschappen = FindViewById<Button> (Resource.Id.eigenschappen);
			Button profielen = FindViewById<Button> (Resource.Id.profielen);
			Button checklist = FindViewById<Button> (Resource.Id.checklist);

			ImageView berg = FindViewById<ImageView> (Resource.Id.berg);

			if(db.GetPreference("tips").value.Equals("true"))
				ShowTipDialog ();

			totems.Click += (sender, eventArgs) => GoToActivity("totems");
			eigenschappen.Click += (sender, eventArgs) => GoToActivity("eigenschappen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
			checklist.Click += (sender, eventArgs) => GoToActivity("checklist");

			berg.LongClick += (object sender, View.LongClickEventArgs e) => {
				Toast.MakeText (this, "Easter egg", ToastLength.Short).Show();
			};
		}

		private void GoToActivity(string activity) {
			Intent intent = null;
			switch (activity) {
			case "totems":
				intent = new Intent (this, typeof(AllTotemsActivity));
				break;
			case "eigenschappen":
				intent = new Intent (this, typeof(AllEigenschappenActivity));
				break;
			case "profielen":
				intent = new Intent (this, typeof(ProfielenActivity));
				break;
			case "checklist":
				intent = new Intent (this, typeof(ChecklistActivity));
				break;
			}
			StartActivity (intent);
		}

		public void ShowTipDialog() {
			var dialog = TipDialog.NewInstance(this);
			RunOnUiThread (() => {
				dialog.Show(FragmentManager, "dialog");
			} );
		}

		public override void OnBackPressed() {
			Intent StartMain = new Intent (Intent.ActionMain);
			StartMain.AddCategory (Intent.CategoryHome);
			StartMain.SetFlags (ActivityFlags.NewTask);
			StartActivity (StartMain);
		}
	}
}