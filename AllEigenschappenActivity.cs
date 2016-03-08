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
	[Activity (Label = "Totems", WindowSoftInputMode = Android.Views.SoftInput.AdjustPan)]			
	public class AllEigenschappenActivity : Activity
	{
		EigenschapAdapter eigenschapAdapter;
		ListView allEigenschappenListView;

		//list of Totem objects
		List<Eigenschap> eigenschappenList;
		Dictionary<string, bool> checkList;
		Dictionary<int, int> freqs;

		Database db;
		EditText query;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllEigenschappen);

			db = new Database (this);

			eigenschappenList = db.GetEigenschappen ();
			freqs = new Dictionary<int, int> ();
			checkList = new Dictionary<string, bool> ();

			foreach (Eigenschap e in eigenschappenList) {
				checkList.Add (e.tid, false);
			}

			eigenschapAdapter = new EigenschapAdapter (this, eigenschappenList, checkList);
			allEigenschappenListView = FindViewById<ListView> (Resource.Id.all_eigenschappen_list);
			allEigenschappenListView.Adapter = eigenschapAdapter;

			Button vindButton = FindViewById<Button> (Resource.Id.vind_button);
		
			query = FindViewById<EditText>(Resource.Id.query);
			LiveSearch ();

			allEigenschappenListView.ItemClick += listView_ItemClick;
			vindButton.Click += (sender, eventArgs) => VindTotem();
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
			eigenschappenList = db.FindEigenschapOpNaam (query.Text);
			eigenschappenList.Reverse ();
			eigenschapAdapter = new EigenschapAdapter (this, eigenschappenList, checkList);
			allEigenschappenListView.Adapter = eigenschapAdapter;
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

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			int pos = e.Position;
			var item = eigenschapAdapter.GetItemAtPosition(pos);
		}

		private void VindTotem() {
			foreach (Eigenschap e in eigenschappenList) {
				if(checkList[e.tid]) {
					List<Totem_eigenschap> toevoegen = db.GetTotemsVanEigenschapsID (e.tid);
					foreach(Totem_eigenschap totem in toevoegen) {
						int idx = Convert.ToInt32 (totem.nid);
						DictMethods.AddOrUpdateDictionaryEntry (freqs, idx) ;
					}
				}
			}

			var totemsActivity = new Intent(this, typeof(TotemsActivity));

			int[] sortedTotems = DictMethods.GetSortedList (freqs, true);
			int[] sortedFreqs = DictMethods.GetSortedList (freqs, false);
			totemsActivity.PutExtra ("totemIDs", sortedTotems);
			totemsActivity.PutExtra ("freqs", sortedFreqs);
			Finish ();
			StartActivity(totemsActivity);
		}

		//helper
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
				eigenschapAdapter = new EigenschapAdapter (this, db.GetEigenschappen(), checkList);
				allEigenschappenListView.Adapter = eigenschapAdapter;
			}
		}
	}
}

