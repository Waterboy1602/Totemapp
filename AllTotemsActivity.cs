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
	public class AllTotemsActivity : Activity
	{
		TotemAdapter totemAdapter;
		ListView allTotemListView;
		List<Totem> totemList;
		int[] totemIDs;
		Database db;
		EditText query;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			db = new Database (this);

			totemIDs = db.allTotemIDs ();

			totemList = new List<Totem> ();

			populateResultList ();

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			query = FindViewById<EditText>(Resource.Id.query);
			LiveSearch ();

			allTotemListView.ItemClick += listView_ItemClick;

		}

		protected override void OnResume ()
		{
			base.OnResume ();
			query.ClearFocus ();
			query.SetCursorVisible(false);
		}

		private void LiveSearch() {
			
			query.AfterTextChanged += (sender, args) =>
			{
				search();
			};
		}

		private void search() {
			fullList = false;
			totemList = db.findTotemOpNaam (query.Text);
			totemList.Reverse ();
			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView.Adapter = totemAdapter;
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

		private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}

		private void HideKeyboard() {
			InputMethodManager inputManager = (InputMethodManager)this.GetSystemService (Context.InputMethodService);
			inputManager.HideSoftInputFromWindow (this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
		}

		public override void OnBackPressed() { 
			if (fullList) {
				base.OnBackPressed ();
			} else {
				query.Text = "";
				fullList = true;
				populateResultList ();
				totemAdapter = new TotemAdapter (this, totemList);
				allTotemListView.Adapter = totemAdapter;
			}
		}
	}
}

