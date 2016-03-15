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

namespace Totem {
	[Activity (Label = "Totems")]			
	public class AllTotemsActivity : Activity {
		TotemAdapter totemAdapter;
		ListView allTotemListView;

		List<Totem> totemList;

		Database db;
		EditText query;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			db = DatabaseHelper.GetInstance (this);

			totemList = db.GetTotems ();

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			query = FindViewById<EditText>(Resource.Id.totemQuery);
			LiveSearch ();

			allTotemListView.ItemClick += TotemClick;
		}

		//removes focus from search bar on resume
		protected override void OnResume () {
			base.OnResume ();
			query.ClearFocus ();
			query.SetCursorVisible(false);
		}

		//update list after every keystroke
		private void LiveSearch() {
			query.AfterTextChanged += (sender, args) => {
				Search();
			};
		}

		//shows only totems that are searched
		private void Search() {
			fullList = false;
			totemList = db.FindTotemOpNaam (query.Text);
			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView.Adapter = totemAdapter;
		}

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		private void TotemClick(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}
			
		//return to full list and empty search field when 'back' is pressed
		//this happens only when a search query is currently entered
		public override void OnBackPressed() { 
			if (fullList) {
				base.OnBackPressed ();
			} else {
				query.Text = "";
				fullList = true;
				totemAdapter = new TotemAdapter (this, db.GetTotems());
				allTotemListView.Adapter = totemAdapter;
			}
		}
	}
}