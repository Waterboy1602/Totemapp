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
using Android.Content.PM;
using Android.Graphics;

namespace Totem {
	[Activity (Label = "Eigenschappen", WindowSoftInputMode = Android.Views.SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]			
	public class AllEigenschappenActivity : Activity {
		EigenschapAdapter eigenschapAdapter;
		ListView allEigenschappenListView;

		List<Eigenschap> eigenschappenList;
		Dictionary<string, bool> checkList;

		Database db;
		EditText query;
		Button vindButton;

		Toast mToast;

		OnCheckBoxClickListener mListener;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllEigenschappen);

			db = DatabaseHelper.GetInstance (this);

			mToast = Toast.MakeText (this, "", ToastLength.Short);

			eigenschappenList = db.GetEigenschappen ();

			//initialize checkList with default values (false) for each eigenschap
			checkList = new Dictionary<string, bool> ();
			foreach (Eigenschap e in eigenschappenList) {
				checkList.Add (e.tid, false);
			}

			//listener to pass to EigenschapAdapter containing context
			mListener = new MyOnCheckBoxClickListener (this);

			eigenschapAdapter = new EigenschapAdapter (this, eigenschappenList, checkList, mListener);
			allEigenschappenListView = FindViewById<ListView> (Resource.Id.all_eigenschappen_list);
			allEigenschappenListView.Adapter = eigenschapAdapter;

			vindButton = FindViewById<Button> (Resource.Id.vind_button);

			query = FindViewById<EditText>(Resource.Id.eigenschapQuery);

			LiveSearch ();

			vindButton.Click += (sender, eventArgs) => VindTotem();
		}

		//removes focus from search bar on resume
		protected override void OnResume ()	{
			base.OnResume ();
			query.ClearFocus ();
			query.SetCursorVisible(false);
			vindButton.Visibility = ViewStates.Visible;
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
			vindButton.Visibility = ViewStates.Gone;
			eigenschappenList = db.FindEigenschapOpNaam (query.Text);
			eigenschapAdapter = new EigenschapAdapter (this, eigenschappenList, checkList, mListener);
			allEigenschappenListView.Adapter = eigenschapAdapter;
		}

		//renders list of totems with frequencies based on selected eigenschappen
		//and redirects to TotemsActivity to view them
		private void VindTotem() {
			Dictionary<int, int> freqs = new Dictionary<int, int> ();
			foreach (Eigenschap e in eigenschappenList) {
				if(checkList[e.tid]) {
					List<Totem_eigenschap> toevoegen = db.GetTotemsVanEigenschapsID (e.tid);
					foreach(Totem_eigenschap totem in toevoegen) {
						int idx = Convert.ToInt32 (totem.nid);
						CollectionHelper.AddOrUpdateDictionaryEntry (freqs, idx) ;
					}
				}
			}

			if (freqs.Count == 0) {
				mToast.SetText ("Geen eigenschappen geselecteerd");
				mToast.Show ();
			} else {
				var totemsActivity = new Intent (this, typeof(TotemsActivity));

				int[] sortedTotems = CollectionHelper.GetSortedList (freqs, true);
				int[] sortedFreqs = CollectionHelper.GetSortedList (freqs, false);
				totemsActivity.PutExtra ("totemIDs", sortedTotems);
				totemsActivity.PutExtra ("freqs", sortedFreqs);

				StartActivity (totemsActivity);
			}
		}

		//create options menu
		public override bool OnCreateOptionsMenu(IMenu menu) {
			MenuInflater.Inflate(Resource.Menu.EigenschapSelectieMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//options menu: add profile or delete all
		public override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {
			case Resource.Id.reset:
				query.Text = "";
				fullList = true;
				checkList = new Dictionary<string, bool> ();
				foreach (Eigenschap e in eigenschappenList) {
					checkList.Add (e.tid, false);
				}
				eigenschapAdapter = new EigenschapAdapter (this, db.GetEigenschappen (), checkList, mListener);
				allEigenschappenListView.Adapter = eigenschapAdapter;
				vindButton.Visibility = ViewStates.Visible;
				return true;

			case Resource.Id.select:
				List<Eigenschap> list = GetSelectedEigenschappen ();
				if (list.Count == 0) {
					mToast.SetText ("Geen eigenschappen geselecteerd");
					mToast.Show ();
				} else {
					fullList = false;
					eigenschapAdapter = new EigenschapAdapter (this, list, checkList, mListener);
					allEigenschappenListView.Adapter = eigenschapAdapter;
					vindButton.Visibility = ViewStates.Visible;
				}
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		private List<Eigenschap> GetSelectedEigenschappen() {
			List<Eigenschap> result = new List<Eigenschap> ();
			foreach(Eigenschap e in eigenschappenList) {
				if (checkList [e.tid]) {
					result.Add (e);
				}
			}
			return result;
		}

		//return to full list and empty search field when 'back' is pressed
		//this happens only when a search query is currently entered
		public override void OnBackPressed() { 
			if (fullList) {
				base.OnBackPressed ();
			} else {
				query.Text = "";
				fullList = true;
				eigenschapAdapter = new EigenschapAdapter (this, db.GetEigenschappen(), checkList, mListener);
				allEigenschappenListView.Adapter = eigenschapAdapter;
			}
			vindButton.Visibility = ViewStates.Visible;
		}
	}
}