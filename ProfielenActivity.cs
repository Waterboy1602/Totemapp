
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

namespace Totem {
	[Activity (Label = "Profielen")]			
	public class ProfielenActivity : Activity {
		ProfielAdapter profielAdapter;
		ListView profielenListView;

		Database db;
		Toast mToast;

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Profielen);

			ActionBar mActionBar = ActionBar;

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

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

			CustomFontTextView title = mCustomView.FindViewById<CustomFontTextView> (Resource.Id.title);
			title.Text = "Profielen";

			ImageButton back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			ImageButton search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.Visibility = ViewStates.Gone;

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);
		}

		//updates data of the adapter and shows/hides the "empty"-message when needed
		private void UpdateList(List<Profiel> profielen) {
			var empty = FindViewById<TextView> (Resource.Id.empty_profiel);
			if (profielen.Count == 0) {
				empty.Visibility = ViewStates.Visible;
			} else {
				empty.Visibility = ViewStates.Gone;
			}
			profielAdapter.UpdateData(profielen);
			profielAdapter.NotifyDataSetChanged();
		}

		private void ProfielClick(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = profielAdapter.GetItemAtPosition(pos);

			if (db.GetTotemsFromProfiel (item.name).Count == 0) {
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
				UpdateList(db.GetProfielen());
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {});

			Dialog dialog = alert.Create();
			RunOnUiThread (() => {
				dialog.Show();
			} );
		}

		//create options menu
		public override bool OnCreateOptionsMenu(IMenu menu) {
			MenuInflater.Inflate(Resource.Menu.profielMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//options menu: add profile or delete all
		public override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {

			//add profile
			case Resource.Id.voegProfielToe:
				AlertDialog.Builder alert = new AlertDialog.Builder (this);
				alert.SetTitle ("Naam");
				EditText input = new EditText (this); 
				input.InputType = Android.Text.InputTypes.TextFlagCapWords;
				KeyboardHelper.ShowKeyboard (this, input);
				alert.SetView (input);
				alert.SetPositiveButton ("Ok", (sender, args) => {
					string value = input.Text;
					if(value.Replace("'", "").Replace(" ", "").Equals("")) {
						mToast.SetText("Ongeldige naam");
						mToast.Show();				
					} else if(db.GetProfielNamen().Contains(value)) {
						input.Text = "";
						mToast.SetText("Profiel " + value + " bestaat al");
						mToast.Show();
					} else {
						db.AddProfile(value);
						UpdateList(db.GetProfielen());
					}
				});

				AlertDialog d1 = alert.Create();

				d1.SetOnDismissListener(new MyOnDismissListener(this));

				//add profile when enter is clicked
				input.EditorAction += (sender, e) => {
					if (e.ActionId == ImeAction.Done) {
						d1.GetButton(-1).PerformClick();
					} else {
						e.Handled = false;
					}
				};

				RunOnUiThread (() => {
					d1.Show();
				});

				return true;

			//delete all profiles
			case Resource.Id.Verwijder:
				AlertDialog.Builder alert1 = new AlertDialog.Builder (this);
				alert1.SetMessage ("Alle profielen verwijderen?");
				alert1.SetPositiveButton ("Ja", (senderAlert, args) => {
					db.ClearProfiles ();
					mToast.SetText("Alle profielen verwijderd");
					mToast.Show();
					UpdateList(db.GetProfielen());
				});

				alert1.SetNegativeButton ("Nee", (senderAlert, args) => {});

				Dialog d2 = alert1.Create();

				RunOnUiThread (() => {
					d2.Show();
				} );

				return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}