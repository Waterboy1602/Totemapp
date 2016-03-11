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
		Button totems;
		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			db = DatabaseHelper.GetInstance (this);
	
			SetContentView (Resource.Layout.Main);

			totems = FindViewById<Button> (Resource.Id.totems);
			Button totemBepalen = FindViewById<Button> (Resource.Id.totemBepalen);
			Button profielen = FindViewById<Button> (Resource.Id.profielen);

			if(db.GetPreference("tips").value.Equals("true")) {
				ShowTipDialog ();
			}

			totems.Click += (sender, eventArgs) => GoToActivity("totems");
			totemBepalen.Click += (sender, eventArgs) => GoToActivity("bepalen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
		}

		private void GoToActivity(string a) {
			if(a.Equals("totems")) {
				TotemLijst();
			} else if(a.Equals("bepalen")) {
				Intent intent = new Intent(this, typeof(EigenschappenActivity));
				StartActivity (intent);
			} else if(a.Equals("profielen")) {
				Intent intent = new Intent(this, typeof(ProfielenActivity));
				StartActivity (intent);
			}
		}

		private void TotemLijst() {
			PopupMenu menu = new PopupMenu (this, totems);
			menu.Inflate (Resource.Menu.Popup);
			menu.Menu.Add(0,1,1,"Zoeken op totem");
			menu.Menu.Add(0,2,2,"Zoeken op eigenschappen");
			menu.MenuItemClick += (s1, arg1) => {
				if(arg1.Item.TitleFormatted.ToString().Equals("Zoeken op totem")) {
					Intent intent = new Intent(this, typeof(AllTotemsActivity));
					StartActivity (intent);				
				}
				else {
					Intent intent = new Intent(this, typeof(AllEigenschappenActivity));
					StartActivity (intent);				
				}
			};

			menu.Show ();
		}

		public void ShowTipDialog() {
			var dialog = TipDialog.NewInstance();
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