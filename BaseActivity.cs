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
using Android.Content.PM;

namespace Totem {
	[Activity (Label = "BaseActivity")]			
	public abstract class BaseActivity : Activity {

		protected TextView ActionBarTitle { get; set; }
		protected ImageButton ActionBarBack { get; set; }
		protected ImageButton ActionBarAdd { get; set; }
		protected ImageButton ActionBarSearch { get; set; }
		protected ImageButton ActionBarClose { get; set; }
		protected ImageButton ActionBarDelete { get; set; }
		protected EditText ActionBarQuery { get; set; }
		protected ActionBar mActionBar { get; set; }

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);
			RequestedOrientation = ScreenOrientation.SensorPortrait;
		}

		protected void InitializeActionBar(ActionBar ab) {
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