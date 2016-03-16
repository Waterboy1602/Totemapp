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

			db = DatabaseHelper.GetInstance (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			profileName = Intent.GetStringExtra ("profileName");
			int[] totemIDs = db.GetTotemIDsFromProfiel(profileName).OrderByDescending (i => i).ToArray();

			totemList = ConvertIDArrayToTotemList (totemIDs);

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			FindViewById<EditText>(Resource.Id.totemQuery).Visibility = ViewStates.Gone;

			allTotemListView.ItemClick += TotemClick;
			allTotemListView.ItemLongClick += TotemLongClick;
		}

		//fill totemList with Totem-objects whose ID is in totemIDs
		private List<Totem> ConvertIDArrayToTotemList(int[] totemIDs) {
			List<Totem> totemList = new List<Totem> ();
			foreach(int idx in totemIDs) {
				totemList.Add (db.GetTotemOnID (idx));
			}
			totemList.RemoveAll(item => item == null);

			return totemList;
		}

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		private void TotemClick(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			detailActivity.PutExtra ("hideButton", "true");
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
				Finish();
				StartActivity(Intent);
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {

			});

			Dialog dialog = alert.Create();
			dialog.Show();
		}
	}
}