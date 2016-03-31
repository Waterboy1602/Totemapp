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
	public class AllTotemsActivity : BaseActivity {
		TotemAdapter totemAdapter;
		ListView allTotemListView;
		List<Totem> totemList;

		Database db;

		EditText query;
		TextView title;
		ImageButton back;
		ImageButton search;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			//Action bar
			base.InitializeActionBar (ActionBar);
			title = base.ActionBarTitle;
			query = base.ActionBarQuery;
			search = base.ActionBarSearch;
			back = base.ActionBarBack;

			db = DatabaseHelper.GetInstance (this);

			totemList = db.GetTotems ();

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			title.Text = "Totems";
			query.Hint = "Zoek totem";

			//hide keyboard when scrolling through list
			allTotemListView.SetOnTouchListener(new MyOnTouchListener(this, query));

			LiveSearch ();

			allTotemListView.ItemClick += ShowDetail;

			search.Visibility = ViewStates.Visible;
			search.Click += (object sender, EventArgs e) => ToggleSearch();

			//hide keybaord when enter is pressed
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
		private void HideSearch() {
			back.Visibility = ViewStates.Visible;
			title.Visibility = ViewStates.Visible;
			query.Visibility = ViewStates.Gone;
			KeyboardHelper.HideKeyboard (this);
			totemAdapter.UpdateData (db.GetTotems ()); 
			totemAdapter.NotifyDataSetChanged ();
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
			totemAdapter.UpdateData (totemList); 
			totemAdapter.NotifyDataSetChanged ();
			if(query.Length() > 0)
				allTotemListView.SetSelection (0);
		}

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		private void ShowDetail(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);
			KeyboardHelper.HideKeyboard (this);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			StartActivity (detailActivity);
		}
			
		//return to full list and empty search field when 'back' is pressed
		//this happens only when a search query is currently entered
		public override void OnBackPressed() {
			if (query.Visibility == ViewStates.Visible)
				HideSearch ();
			else
				base.OnBackPressed ();
		}
	}
}