
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

namespace Totem
{
	[Activity (Label = "Totems")]			
	public class TotemsActivity : Activity
	{
		TotemAdapter totemAdapter;
		ListView totemListView;
		List<Totem> totemList;
		int[] totemIDs;
		int[] freqs;
		Database db;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Totems);

			db = new Database (this);
	
			totemIDs = Intent.GetIntArrayExtra ("totemIDs");
			freqs = Intent.GetIntArrayExtra ("freqs");

			//reverse frequency array to match it with the totems
			ReverseArray (freqs);

			totemList = new List<Totem> ();

			PopulateResultList ();

			totemAdapter = new TotemAdapter (this, totemList, freqs);
			totemListView = FindViewById<ListView> (Resource.Id.totem_list);
			totemListView.Adapter = totemAdapter;

			totemListView.ItemClick += listView_ItemClick;

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
			totemList.Reverse ();
		}

		void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}

		//return to MainActivity and not to EigenschappenActivity when 'back' is pressed
		public override void OnBackPressed() { 
			var intent = new Intent(this, typeof(MainActivity));
			StartActivity (intent);
		}
	}
}

