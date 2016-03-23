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
using Android.Graphics;

namespace Totem {
	[Activity (Label = "Totems")]			
	public class AllTotemsActivity : Activity {
		TotemAdapter totemAdapter;
		ListView allTotemListView;

		List<Totem> totemList;

		Database db;

		EditText query;
		CustomFontTextView title;
		ImageButton back;
		ImageButton search;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			ActionBar mActionBar = ActionBar;
			mActionBar.SetDisplayShowTitleEnabled(true);
			mActionBar.SetDisplayShowHomeEnabled(false);

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			db = DatabaseHelper.GetInstance (this);

			totemList = db.GetTotems ();

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			query = mCustomView.FindViewById<EditText>(Resource.Id.query);
			query.Hint = "Zoek totem";

			LiveSearch ();

			allTotemListView.ItemClick += TotemClick;

			title = mCustomView.FindViewById<CustomFontTextView> (Resource.Id.title);
			title.Text = "Totems";

			back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.Click += (object sender, EventArgs e) => ToggleSearch();

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);

			//hide keybaord when enter is pressed
			query.EditorAction += (sender, e) => {
				if (e.ActionId == ImeAction.Search) 
					KeyboardHelper.HideKeyboard(this);
				else
					e.Handled = false;
			};
		}

		private void ToggleSearch() {
			if (query.Visibility == ViewStates.Visible) {
				HideSearch();
			} else {
				back.Visibility = ViewStates.Gone;
				title.Visibility = ViewStates.Gone;
				query.Visibility = ViewStates.Visible;
				query.RequestFocus ();
			}
		}

		private void HideSearch() {
			back.Visibility = ViewStates.Visible;
			title.Visibility = ViewStates.Visible;
			query.Visibility = ViewStates.Gone;
		}

		//update list after every keystroke
		private void LiveSearch() {
			query.AfterTextChanged += (sender, args) => {
				Search();
			};
		}

		//shows only totems that are searched
		private void Search() {
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
			if (query.Visibility == ViewStates.Visible) {
				HideSearch ();
				query.Text = "";
				totemAdapter = new TotemAdapter (this, db.GetTotems());
				allTotemListView.Adapter = totemAdapter;
			} else {
				base.OnBackPressed ();
			}
		}
	}
}