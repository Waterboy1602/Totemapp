using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Java.Lang;

namespace Totem {
	[Activity (Label = "Totems")]
	public class ProfielTotemsActivity : Activity {
		TotemAdapter totemAdapter;
		ListView allTotemListView;

		//list of Totem objects
		List<Totem> totemList;

		Database db;
		Toast mToast;
		string profileName;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			ActionBar mActionBar = ActionBar;
			mActionBar.SetDisplayShowTitleEnabled(true);
			mActionBar.SetDisplayShowHomeEnabled(false);

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			db = DatabaseHelper.GetInstance (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			profileName = Intent.GetStringExtra ("profileName");
			totemList = db.GetTotemsFromProfiel (profileName);

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			allTotemListView.ItemClick += TotemClick;
			allTotemListView.ItemLongClick += TotemLongClick;

			CustomFontTextView title = mCustomView.FindViewById<CustomFontTextView> (Resource.Id.title);
			title.Text = "Totems voor " + profileName;

			ImageButton back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			ImageButton search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.Visibility = ViewStates.Gone;

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);
		}

		protected override void OnRestart() {
			base.OnRestart ();
			totemList = db.GetTotemsFromProfiel (profileName);
			totemAdapter.UpdateData (totemList);
			totemAdapter.NotifyDataSetChanged ();
		}

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		private void TotemClick(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			detailActivity.PutExtra ("profileName", profileName);
			StartActivity (detailActivity);
		}

		private void TotemLongClick(object sender, AdapterView.ItemLongClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetMessage (item.title + " verwijderen uit profiel " + profileName + "?");
			alert.SetPositiveButton ("Ja", (senderAlert, args) => {
				db.DeleteTotemFromProfile(item.nid, profileName);
				mToast.SetText(item.title + " verwijderd");
				mToast.Show();
				if(totemList.Count == 1) {
					base.OnBackPressed();
				} else {
					totemList = db.GetTotemsFromProfiel(profileName);
					totemAdapter.UpdateData (totemList);
					totemAdapter.NotifyDataSetChanged ();
				}
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {

			});

			Dialog dialog = alert.Create();
			dialog.Show();
		}
	}
}