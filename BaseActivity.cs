using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Totem {
	[Activity (Label = "BaseActivity")]			
	public class BaseActivity : Activity {

		public TextView ActionBarTitle { get; set; }
		public ImageButton ActionBarBack { get; set; }
		public ImageButton ActionBarAdd { get; set; }
		public ImageButton ActionBarSearch { get; set; }
		public ImageButton ActionBarClose { get; set; }
		public ImageButton ActionBarDelete { get; set; }
		public EditText ActionBarQuery { get; set; }
		public ActionBar mActionBar { get; set; }

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);
		}

		public void InitializeActionBar(ActionBar ab) {
			mActionBar = ab;

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			ActionBarTitle = mCustomView.FindViewById<TextView> (Resource.Id.title);
			ActionBarBack = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			ActionBarAdd = mCustomView.FindViewById<ImageButton> (Resource.Id.addButton);
			ActionBarSearch = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			ActionBarClose = mCustomView.FindViewById<ImageButton> (Resource.Id.closeButton);
			ActionBarDelete = mCustomView.FindViewById<ImageButton> (Resource.Id.deleteButton);
			ActionBarQuery = mCustomView.FindViewById<EditText>(Resource.Id.query);

			ActionBarBack.Click += (object sender, EventArgs e) => OnBackPressed();

			var layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);
		}
	}
}