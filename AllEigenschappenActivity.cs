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
using System.Threading.Tasks;
using System.Threading;
using Android.Graphics.Drawables;

namespace Totem {
	[Activity (Label = "Eigenschappen", WindowSoftInputMode = Android.Views.SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]			
	public class AllEigenschappenActivity : Activity {
		EigenschapAdapter eigenschapAdapter;
		ListView allEigenschappenListView;

		List<Eigenschap> eigenschappenList;

		Database db;

		RelativeLayout bottomBar;

		EditText query;
		CustomFontTextView title;
		ImageButton back;

		IMenu menu;

		Toast mToast;

		MyOnCheckBoxClickListener mListener;

		bool fullList = true;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllEigenschappen);

			ActionBar mActionBar = ActionBar;

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			db = DatabaseHelper.GetInstance (this);

			mToast = Toast.MakeText (this, "", ToastLength.Short);

			eigenschappenList = db.GetEigenschappen ();

			//initialize with default values (false) for each eigenschap
			foreach (Eigenschap e in eigenschappenList) {
				e.selected = false;
			}

			//listener to pass to EigenschapAdapter containing context
			mListener = new MyOnCheckBoxClickListener (this);

			eigenschapAdapter = new EigenschapAdapter (this, eigenschappenList, mListener);
			allEigenschappenListView = FindViewById<ListView> (Resource.Id.all_eigenschappen_list);
			allEigenschappenListView.Adapter = eigenschapAdapter;

			query = mCustomView.FindViewById<EditText>(Resource.Id.query);
			query.Hint = "Zoek eigenschap";

			LiveSearch ();

			var vind = FindViewById<LinearLayout> (Resource.Id.vind);
			vind.Click += (sender, eventArgs) => VindTotem();

			bottomBar = FindViewById<RelativeLayout> (Resource.Id.bottomBar);

			title = mCustomView.FindViewById<CustomFontTextView> (Resource.Id.title);
			title.Text = "Eigenschappen";

			back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			var search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.Click += (object sender, EventArgs e) => ToggleSearch();

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);

			//hide keyboard when enter is pressed
			query.EditorAction += (sender, e) => {
				if (e.ActionId == ImeAction.Search)
					KeyboardHelper.HideKeyboard(this);
				else
					e.Handled = false;
			};
		}

		//toggles the search bar
		private void ToggleSearch() {
			if (query.Visibility == ViewStates.Visible) {
				HideSearch();
			} else {
				back.Visibility = ViewStates.Gone;
				title.Visibility = ViewStates.Gone;
				query.Visibility = ViewStates.Visible;
				KeyboardHelper.ShowKeyboard (this, query);
				query.Text = "";
				query.RequestFocus ();
			}
		}

		//hides the search bar
		private void HideSearch() {
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
		private void LiveSearch() {
			query.AfterTextChanged += (sender, args) => {
				Search();
				if(query.Text.Equals("")) {
					fullList = true;
				}
			};
		}

		//shows only totems that are match the query
		private void Search() {
			fullList = false;
			eigenschappenList = db.FindEigenschapOpNaam (query.Text);
			eigenschapAdapter.UpdateData (eigenschappenList);
			eigenschapAdapter.NotifyDataSetChanged ();
		}

		//renders list of totems with frequencies based on selected eigenschappen
		//and redirects to TotemsActivity to view them
		private void VindTotem() {
			Dictionary<int, int> freqs = new Dictionary<int, int> ();
			int selected = 0;
			foreach (Eigenschap e in eigenschappenList) {
				if(e.selected) {
					selected++;
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

		//options menu: add profile or delete all
		public override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {

			//reset selection
			case Resource.Id.reset:
				query.Text = "";
				fullList = true;
				foreach (Eigenschap e in eigenschappenList) {
					e.selected = false;
				}
				eigenschapAdapter.UpdateData (db.GetEigenschappen ());
				eigenschapAdapter.NotifyDataSetChanged ();
				UpdateOptionsMenu ();
				return true;
			
			//show selected only
			case Resource.Id.select:
				List<Eigenschap> list = GetSelectedEigenschappen ();
				if (list.Count == 0) {
					mToast.SetText ("Geen eigenschappen geselecteerd");
					mToast.Show ();
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
		private void UpdateOptionsMenu() {
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
		private List<Eigenschap> GetSelectedEigenschappen() {
			List<Eigenschap> result = new List<Eigenschap> ();
			foreach(Eigenschap e in eigenschappenList) {
				if (e.selected) {
					result.Add (e);
				}
			}
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