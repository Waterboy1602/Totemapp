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

			Typeface Din = Typeface.CreateFromAsset(Assets,"fonts/DINPro-Regular.ttf");

			Button totems = FindViewById<Button> (Resource.Id.totems);
			Button eigenschappen = FindViewById<Button> (Resource.Id.eigenschappen);
			Button profielen = FindViewById<Button> (Resource.Id.profielen);
			Button checklist = FindViewById<Button> (Resource.Id.goede_totemisatie);
			checklist.Visibility = ViewStates.Gone;
			totems.SetTypeface (Din, 0);
			eigenschappen.SetTypeface (Din, 0);
			profielen.SetTypeface (Din, 0);

			if(db.GetPreference("tips").value.Equals("true")) {
				ShowTipDialog ();
			}

			totems.Click += (sender, eventArgs) => GoToActivity("totems");
			eigenschappen.Click += (sender, eventArgs) => GoToActivity("eigenschappen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
		}

		private void GoToActivity(string activity) {
			Intent intent = null;
			if(activity.Equals("totems")) {
				intent = new Intent(this, typeof(AllTotemsActivity));
			} else if(activity.Equals("eigenschappen")) {
				intent = new Intent(this, typeof(AllEigenschappenActivity));
			} else if(activity.Equals("profielen")) {
				intent = new Intent(this, typeof(ProfielenActivity));
			}
			StartActivity (intent);
		}

		public void ShowTipDialog() {
			var dialog = TipDialog.NewInstance(this);
			dialog.Show(FragmentManager, "dialog");
		}

		public override void OnBackPressed() {
			Intent StartMain = new Intent (Intent.ActionMain);
			StartMain.AddCategory (Intent.CategoryHome);
			StartMain.SetFlags (ActivityFlags.NewTask);
			StartActivity (StartMain);
		}
	}
}