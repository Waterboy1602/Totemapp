
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
using Android.Views.InputMethods;

namespace Totem
{
	[Activity (Label = "Profielen")]			
	public class ProfielenActivity : Activity
	{
		ProfielAdapter profielAdapter;
		ListView profielenListView;

		Database db;
		Toast mToast;

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Profielen);

			db = DatabaseHelper.GetInstance (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);


			var profielen = db.GetProfielen ();
			if (profielen.Count == 0)
				FindViewById<TextView> (Resource.Id.empty_profiel).Visibility = ViewStates.Visible;
			profielAdapter = new ProfielAdapter (this, profielen);
			profielenListView = FindViewById<ListView> (Resource.Id.profielen_list);
			profielenListView.Adapter = profielAdapter;

			profielenListView.ItemClick += ProfielClick;
			profielenListView.ItemLongClick += ProfielLongClick;
		}

		private void ProfielClick(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = profielAdapter.GetItemAtPosition(pos);

			if (db.GetTotemIDsFromProfiel (item.name).Max () == 0) {
				mToast.SetText("Profiel " + item.name + " bevat geen totems");
				mToast.Show();
			} else {
				var totemsActivity = new Intent (this, typeof(ProfielTotemsActivity));
				totemsActivity.PutExtra ("profileName", item.name);
				StartActivity (totemsActivity);
			}
		}

		private void ProfielLongClick(object sender, AdapterView.ItemLongClickEventArgs e) {
			int pos = e.Position;
			var item = profielAdapter.GetItemAtPosition(pos);

			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetMessage ("Profiel " + item.name + " verwijderen?");
			alert.SetPositiveButton ("Ja", (senderAlert, args) => {
				db.DeleteProfile(item.name);
				mToast.SetText("Profiel " + item.name + " verwijderd");
				mToast.Show();
				Finish();
				StartActivity(Intent);
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {

			});

			Dialog dialog = alert.Create();
			dialog.Show();
		}

		//create options menu
		public override bool OnCreateOptionsMenu(IMenu menu) {
			MenuInflater.Inflate(Resource.Menu.profielMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//options menu: add profile or delete all
		public override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {
			case Resource.Id.voegProfielToe:
				AlertDialog.Builder alert = new AlertDialog.Builder (this);
				alert.SetTitle ("Naam");
				EditText input = new EditText (this); 
				input.InputType = Android.Text.InputTypes.TextFlagCapWords;
				ShowKeyboard (input);
				alert.SetView (input);
				alert.SetPositiveButton ("Ok", (sender, args) => {
					string value = input.Text;
					if(value.Replace("'", "").Equals("")) {
						mToast.SetText("Ongeldige naam");
						mToast.Show();
						HideKeyboard();					
					} else if(db.GetProfielNamen().Contains(value)) {
						input.Text = "";
						mToast.SetText("Profiel " + value + " bestaat al");
						mToast.Show();
						HideKeyboard(); 
					} else {
						db.AddProfile(value);
						HideKeyboard();

						//refresh list
						Finish();
						StartActivity (Intent);
					}
				});

				RunOnUiThread (() => {
					alert.Show();
				} );

				return true;

			case Resource.Id.Verwijder:
				AlertDialog.Builder alert1 = new AlertDialog.Builder (this);
				alert1.SetMessage ("Alle profielen verwijderen?");
				alert1.SetPositiveButton ("Ja", (senderAlert, args) => {
					db.ClearProfiles ();
					mToast.SetText("Alle profielen verwijderd");
					mToast.Show();
					var main = new Intent (this, typeof(MainActivity));
					StartActivity (main);
				});

				alert1.SetNegativeButton ("Nee", (senderAlert, args) => {

				});

				Dialog dialog = alert1.Create();
				dialog.Show();

				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		//helper
		public void ShowKeyboard(View pView) {
			pView.RequestFocus();

			InputMethodManager inputMethodManager = Application.GetSystemService(Context.InputMethodService) as InputMethodManager;
			inputMethodManager.ShowSoftInput(pView, ShowFlags.Forced);
			inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
		}

		//helper
		public void HideKeyboard() {
			InputMethodManager inputManager = (InputMethodManager)this.GetSystemService (Context.InputMethodService);
			inputManager.ToggleSoftInput (ShowFlags.Implicit, 0);
		}
	}
}