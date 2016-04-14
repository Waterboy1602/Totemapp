using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using TotemAndroid;
using TotemAppCore;

namespace TotemAndroid {
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

		protected AppController _appController = AppController.Instance;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);
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

			ActionBarBack.Click += (sender, e) => OnBackPressed ();

			var layout = new ActionBar.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);
		}
	}
}