using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Totem {
	[Activity (Label = "Totems")]			
	public class ResultTotemsActivity : BaseActivity {
		TotemAdapter totemAdapter;
		ListView totemListView;
		List<Totem> totemList;

		TextView title;
		ImageButton back;

		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Totems);

			//Action bar
			InitializeActionBar (ActionBar);
			title = ActionBarTitle;
			back = ActionBarBack;

			db = DatabaseHelper.GetInstance (this);
	
			int[] totemIDs = Intent.GetIntArrayExtra ("totemIDs");
			int[] freqs = Intent.GetIntArrayExtra ("freqs");
			int selected = Intent.GetIntExtra ("selected", 0);

			totemList = ConvertIDArrayToTotemList (totemIDs);

			totemAdapter = new TotemAdapter (this, totemList, freqs, selected);
			totemListView = FindViewById<ListView> (Resource.Id.totem_list);
			totemListView.Adapter = totemAdapter;

			totemListView.ItemClick += ShowDetail;

			title.Text = "Totems";
		}

		//fill totemList with Totem-objects whose ID is in totemIDs
		List<Totem> ConvertIDArrayToTotemList(int[] totemIDs) {
			var list = new List<Totem> ();
			foreach(int idx in totemIDs)
				list.Add (db.GetTotemOnID (idx));

			return list;
		}

		void ShowDetail(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}

		//goes back to main screen when GoToMain is set to true
		//otherwise acts normal
		public override void OnBackPressed() {
			if (Intent.GetBooleanExtra ("GoToMain", false) == true) {
				var i = new Intent (this, typeof(MainActivity));
				i.SetFlags (ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				StartActivity (i);
			} else {
				base.OnBackPressed ();
			}
		}
	}
}