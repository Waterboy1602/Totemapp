
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
			reverseArray (freqs);

			totemList = new List<Totem> ();

			populateResultList ();

			totemAdapter = new TotemAdapter (this, totemList, freqs);
			totemListView = FindViewById<ListView> (Resource.Id.totem_list);
			totemListView.Adapter = totemAdapter;

			totemListView.ItemClick += listView_ItemClick;

		}

		private void reverseArray(int [] arr) {
			for (int i = 0; i < arr.Length / 2; i++)
			{
				int tmp = arr[i];
				arr[i] = arr[arr.Length - i - 1];
				arr[arr.Length - i - 1] = tmp;
			}
		}

		private void populateResultList() {
			foreach(int idx in totemIDs) {
				totemList.Add (db.getTotemOnID (idx));
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
			
		public override void OnBackPressed() { 
			var intent = new Intent(this, typeof(MainActivity));
			StartActivity (intent);
		}
	}
}

