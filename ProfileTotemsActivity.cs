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

namespace Totem
{
	[Activity (Label = "Totems")]			
	public class ProfileTotemsActivity : Activity
	{
		TotemAdapter totemAdapter;
		ListView allTotemListView;

		//list of Totem objects
		List<Totem> totemList;

		//array of totem IDs
		int[] totemIDs;

		Database db;
		Toast mToast;
		string profileName;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			db = new Database (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			profileName = Intent.GetStringExtra ("profileName");
			totemIDs = db.GetTotemIDsFromProfiel(profileName);
			totemIDs = totemIDs.OrderBy (i => i).ToArray();

			totemList = new List<Totem> ();

			PopulateResultList ();

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			EditText query = FindViewById<EditText>(Resource.Id.query);
			query.Visibility = ViewStates.Gone;

			allTotemListView.ItemClick += listView_ItemClick;
			allTotemListView.ItemLongClick += listView_ItemLongClick;
		}

		//helper method to reverse an array
		private void ReverseArray(int [] arr) {
			for (int i = 0; i < arr.Length / 2; i++)
			{
				int tmp = arr[i];
				arr[i] = arr[arr.Length - i - 1];
				arr[arr.Length - i - 1] = tmp;
			}
		}

		//fill totemList with Totem-objects whose ID is in totemIDs
		//resulting list is reversed to order them descending by frequency
		private void PopulateResultList() {
			foreach(int idx in totemIDs) {
				totemList.Add (db.GetTotemOnID (idx));
			}
			totemList.RemoveAll(item => item == null);
			totemList.Reverse ();
		}

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}

		private void listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
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

