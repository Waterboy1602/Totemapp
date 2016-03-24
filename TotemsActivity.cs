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

		CustomFontTextView title;
		ImageButton back;

		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Totems);

			ActionBar mActionBar = ActionBar;
			mActionBar.SetDisplayShowTitleEnabled(true);
			mActionBar.SetDisplayShowHomeEnabled(false);

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			db = DatabaseHelper.GetInstance (this);
	
			int[] totemIDs = Intent.GetIntArrayExtra ("totemIDs");
			int[] freqs = Intent.GetIntArrayExtra ("freqs");

			totemList = ConvertIDArrayToTotemList (totemIDs);

			totemAdapter = new TotemAdapter (this, totemList, freqs);
			totemListView = FindViewById<ListView> (Resource.Id.totem_list);
			totemListView.Adapter = totemAdapter;

			totemListView.ItemClick += TotemClick;

			title = mCustomView.FindViewById<CustomFontTextView> (Resource.Id.title);
			title.Text = "Totems";

			back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			ImageButton search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.Visibility = ViewStates.Gone;

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);
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