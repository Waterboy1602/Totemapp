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

namespace Totem {
	[Activity (Label = "Totems")]			
	public class TotemsActivity : Activity {
		TotemAdapter totemAdapter;
		ListView totemListView;
		List<Totem> totemList;

		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Totems);

			db = DatabaseHelper.GetInstance (this);
	
			int[] totemIDs = Intent.GetIntArrayExtra ("totemIDs");
			int[] freqs = Intent.GetIntArrayExtra ("freqs");

			totemList = ConvertIDArrayToTotemList (totemIDs);

			totemAdapter = new TotemAdapter (this, totemList, freqs);
			totemListView = FindViewById<ListView> (Resource.Id.totem_list);
			totemListView.Adapter = totemAdapter;

			totemListView.ItemClick += TotemClick;
		}

		//fill totemList with Totem-objects whose ID is in totemIDs
		private List<Totem> ConvertIDArrayToTotemList(int[] totemIDs) {
			List<Totem> totemList = new List<Totem> ();
			foreach(int idx in totemIDs) {
				totemList.Add (db.GetTotemOnID (idx));
			}

			return totemList;
		}

		void TotemClick(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}
	}
}