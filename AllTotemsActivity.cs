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

		//list of Totem objects
		List<Totem> totemList;

		//array of totem IDs
		int[] totemIDs;

		Database db;
		EditText query;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			db = new Database (this);

			totemIDs = Intent.GetIntArrayExtra ("profielTotems");
			if(totemIDs == null) totemIDs = db.AllTotemIDs ();

			totemList = new List<Totem> ();

			PopulateResultList ();

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			query = FindViewById<EditText>(Resource.Id.query);
			LiveSearch ();

			allTotemListView.ItemClick += listView_ItemClick;

		}

		//removes focus from search bar on resume
		protected override void OnResume ()
		{
			base.OnResume ();
			query.ClearFocus ();
			query.SetCursorVisible(false);
		}

		//update list after every keystroke
		private void LiveSearch() {
			
			query.AfterTextChanged += (sender, args) =>
			{
				Search();
			};
		}

		//shows only totems that are searched
		private void Search() {
			fullList = false;
			totemList = db.FindTotemOpNaam (query.Text);
			totemList.Reverse ();
			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView.Adapter = totemAdapter;
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

		//hides the keyboard
		private void HideKeyboard() {
			InputMethodManager inputManager = (InputMethodManager)this.GetSystemService (Context.InputMethodService);
			inputManager.HideSoftInputFromWindow (this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
		}
			
		//return to full list and empty search field when 'back' is pressed
		//this happens only when a search query is currently entered
		public override void OnBackPressed() { 
			if (fullList) {
				base.OnBackPressed ();
			} else {
				query.Text = "";
				fullList = true;
				PopulateResultList ();
				totemAdapter = new TotemAdapter (this, totemList);
				allTotemListView.Adapter = totemAdapter;
			}
		}
	}
}

