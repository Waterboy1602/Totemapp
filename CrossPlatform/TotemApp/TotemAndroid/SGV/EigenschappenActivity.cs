using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Android.App;

using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using TotemAppCore;

namespace TotemAndroid {
	[Activity (Label = "Eigenschappen", WindowSoftInputMode = Android.Views.SoftInput.AdjustPan)]			
	public class EigenschappenActivity : BaseActivity {
		EigenschapAdapter eigenschapAdapter;
		ListView allEigenschappenListView;
		List<Eigenschap> eigenschappenList;

		Database db;
		Toast mToastShort;
		Toast mToastLong;

		RelativeLayout bottomBar;

		EditText query;
		TextView title;
		ImageButton back;
		ImageButton search;

		IMenu menu;

		MyOnCheckBoxClickListener mListener;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllEigenschappen);

			//Action bar
			InitializeActionBar (ActionBar);
			title = ActionBarTitle;
			query = ActionBarQuery;
			search = ActionBarSearch;
			back = ActionBarBack;

			db = DatabaseHelper.GetInstance ();

			mToastShort = Toast.MakeText (this, "", ToastLength.Short);
			mToastLong = Toast.MakeText (this, "", ToastLength.Long);

			eigenschappenList = db.GetEigenschappen ();

			//initialize with default values (false) for each eigenschap
			foreach (Eigenschap e in eigenschappenList)
				e.selected = false;

			//listener to pass to EigenschapAdapter containing context
			mListener = new MyOnCheckBoxClickListener (this);

			eigenschapAdapter = new EigenschapAdapter (this, eigenschappenList, mListener);
			allEigenschappenListView = FindViewById<ListView> (Resource.Id.all_eigenschappen_list);
			allEigenschappenListView.Adapter = eigenschapAdapter;

			title.Text = "Eigenschappen";
			query.Hint = "Zoek eigenschap";

			//hide keyboard when scrolling through list
			allEigenschappenListView.SetOnTouchListener(new MyOnTouchListener(this, query));

			LiveSearch ();

			var vind = FindViewById<LinearLayout> (Resource.Id.vind);
			vind.Click += VindTotem;

			bottomBar = FindViewById<RelativeLayout> (Resource.Id.bottomBar);

			search.Visibility = ViewStates.Visible;
			search.Click += (sender, e) => ToggleSearch ();

			//hide keyboard when enter is pressed
			query.EditorAction += (sender, e) => {
				if (e.ActionId == ImeAction.Search)
					KeyboardHelper.HideKeyboard(this);
				else
					e.Handled = false;
			};

			allEigenschappenListView.ItemLongClick += ShowExplanation;
		}

		//IDEA
		//shows short explanation of eigenschap
		void ShowExplanation(object sender, AdapterView.ItemLongClickEventArgs e) {
			int pos = e.Position;
			var item = eigenschapAdapter.GetItemAtPosition(pos);

			mToastLong.SetText("Meer uitleg over " + item.name.ToLower());
			mToastLong.Show();
		}

		//toggles the search bar
		void ToggleSearch() {
			if (query.Visibility == ViewStates.Visible) {
				HideSearch();
				search.SetImageResource (Resource.Drawable.ic_search_white_24dp);
			} else {
				back.Visibility = ViewStates.Gone;
				title.Visibility = ViewStates.Gone;
				query.Visibility = ViewStates.Visible;
				KeyboardHelper.ShowKeyboard (this, query);
				query.Text = "";
				query.RequestFocus ();
				search.SetImageResource (Resource.Drawable.ic_close_white_24dp);
			}
		}

		//hides the search bar
		void HideSearch() {
			back.Visibility = ViewStates.Visible;
			title.Visibility = ViewStates.Visible;
			query.Visibility = ViewStates.Gone;
			KeyboardHelper.HideKeyboard (this);
			eigenschapAdapter.UpdateData (db.GetEigenschappen ());
			eigenschapAdapter.NotifyDataSetChanged ();
			query.Text = "";
			fullList = true;
			UpdateOptionsMenu ();
		}

		//update list after every keystroke
		void LiveSearch() {
			query.AfterTextChanged += (sender, args) => {
				Search();
				if(query.Text.Equals(""))
					fullList = true;
			};
		}

		//shows only totems that match the query
		void Search() {
			fullList = false;
			eigenschappenList = _appController.FindEigenschapOpNaam (query.Text);
			eigenschapAdapter.UpdateData (eigenschappenList);
			eigenschapAdapter.NotifyDataSetChanged ();
			if(query.Length() > 0)
				allEigenschappenListView.SetSelection (0);
		}

		//renders list of totems with frequencies based on selected eigenschappen
		//and redirects to TotemsActivity to view them
		void VindTotem(object sender, EventArgs e) {
			var freqs = new Dictionary<int, int> ();
			int selected = 0;
			foreach (Eigenschap eig in eigenschappenList) {
				if(eig.selected) {
					selected++;
					List<Totem_eigenschap> toAdd = _appController.GetTotemsVanEigenschapsID (eig.tid);
					foreach(Totem_eigenschap totem in toAdd) {
						int idx = Convert.ToInt32 (totem.nid);
						CollectionHelper.AddOrUpdateDictionaryEntry (freqs, idx) ;
					}
				}
			}

			if (freqs.Count == 0) {
				mToastShort.SetText ("Geen eigenschappen geselecteerd");
				mToastShort.Show ();
			} else {
				var totemsActivity = new Intent (this, typeof(ResultTotemsActivity));

				int[] sortedTotems = CollectionHelper.GetSortedList (freqs, true);
				int[] sortedFreqs = CollectionHelper.GetSortedList (freqs, false);
				totemsActivity.PutExtra ("totemIDs", sortedTotems);
				totemsActivity.PutExtra ("freqs", sortedFreqs);
				totemsActivity.PutExtra ("selected", selected);

				StartActivity (totemsActivity);
			}
		}

		//create options menu
		public override bool OnCreateOptionsMenu(IMenu m) {
			this.menu = m;
			MenuInflater.Inflate(Resource.Menu.EigenschapSelectieMenu, menu);
			IMenuItem item = menu.FindItem (Resource.Id.full);
			item.SetVisible (false);
			return base.OnCreateOptionsMenu(menu);
		}

		//options menu: add profile, view selection of view full list
		public override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {

			//reset selection
			case Resource.Id.reset:
				query.Text = "";
				fullList = true;
				foreach (Eigenschap e in eigenschappenList)
					e.selected = false;
				eigenschapAdapter.UpdateData (db.GetEigenschappen ());
				eigenschapAdapter.NotifyDataSetChanged ();
				UpdateOptionsMenu ();
				return true;
			
			//show selected only
			case Resource.Id.select:
				List<Eigenschap> list = GetSelectedEigenschappen ();
				if (list.Count == 0) {
					mToastShort.SetText ("Geen eigenschappen geselecteerd");
					mToastShort.Show ();
				} else {
					fullList = false;
					UpdateOptionsMenu ();
					eigenschapAdapter.UpdateData (list);
					eigenschapAdapter.NotifyDataSetChanged ();
					bottomBar.Visibility = ViewStates.Visible;
				}
				return true;

			//show full list
			case Resource.Id.full:
				query.Text = "";
				fullList = true;
				UpdateOptionsMenu ();
				eigenschapAdapter.UpdateData (db.GetEigenschappen ());
				eigenschapAdapter.NotifyDataSetChanged ();
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		//changes the options menu items according to list
		//delay of 0.5 seconds to take animation into account
		void UpdateOptionsMenu() {
			IMenuItem s = menu.FindItem (Resource.Id.select);
			IMenuItem f = menu.FindItem (Resource.Id.full);

			Task.Factory.StartNew(() => Thread.Sleep(500)).ContinueWith((t) => {
				if(fullList) {
					s.SetVisible (true);
					f.SetVisible (false);
				} else {
					s.SetVisible (false);
					f.SetVisible (true);
				}
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		//returns list of eigenschappen that have been checked
		List<Eigenschap> GetSelectedEigenschappen() {
			var result = new List<Eigenschap> ();
			foreach(Eigenschap e in eigenschappenList)
				if (e.selected)
					result.Add (e);

			return result;
		}

		//return to full list and empty search field when 'back' is pressed
		//this happens only when a search query is currently entered
		public override void OnBackPressed() {
			if (query.Visibility == ViewStates.Visible) {
				HideSearch ();
			} else if (!fullList) {
				query.Text = "";
				fullList = true;
				UpdateOptionsMenu ();
				eigenschapAdapter.UpdateData (db.GetEigenschappen ());
				eigenschapAdapter.NotifyDataSetChanged ();
			} else {
				base.OnBackPressed ();
			}
		}
	}
}