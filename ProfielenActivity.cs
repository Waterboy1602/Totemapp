
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
	[Activity (Label = "ProfielenActivity")]			
	public class ProfielenActivity : Activity
	{
		ListView profielenListView;
		Database db;
		ProfielAdapter profielAdapter;
		Toast mToast;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Profielen);

			db = new Database (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			profielAdapter = new ProfielAdapter (this, db.GetProfielen());
			profielenListView = FindViewById<ListView> (Resource.Id.profielen_list);
			profielenListView.Adapter = profielAdapter;

			profielenListView.ItemClick += listView_ItemClick;
			profielenListView.ItemLongClick += listView_ItemLongClick;
		}

		private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			int pos = e.Position;
			var item = profielAdapter.GetItemAtPosition(pos);

			if (db.GetTotemIDsFromProfiel (item.name).Max () == 0) {
				mToast.SetText("Profiel " + item.name + " bevat geen totems");
				mToast.Show();
			} else {
				var totemsActivity = new Intent (this, typeof(AllTotemsActivity));
				totemsActivity.PutExtra ("profielTotems", db.GetTotemIDsFromProfiel (item.name));
				StartActivity (totemsActivity);
			}
		}

		private void listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			// Implement delete
		}

		//create options menu
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//options menu: add profile or delete all
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Resource.Id.voegProfielToe:
				AlertDialog.Builder alert = new AlertDialog.Builder (this);
				alert.SetTitle ("Naam");
				EditText input = new EditText (this); 
				input.InputType = Android.Text.InputTypes.TextFlagCapWords;
				ShowKeyboard (input);
				alert.SetView (input);
				alert.SetPositiveButton ("Ok", (sender, args) => {
					string value = input.Text;
					if(db.GetProfielNamen().Contains(value)) {
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
				db.ClearProfiles ();
				var main = new Intent (this, typeof(MainActivity));
				StartActivity (main);
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

