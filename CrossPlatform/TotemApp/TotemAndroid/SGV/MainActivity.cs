using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Runtime.InteropServices;

namespace TotemAndroid {
	[Activity (Label = "Totemapp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppThemeNoAction")]
	public class MainActivity : BaseActivity {
		//Database db;
		Button totems;
		Button eigenschappen;
		Button profielen;
		Button checklist;
		Button tinder;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			totems = FindViewById<Button> (Resource.Id.totems);
			eigenschappen = FindViewById<Button> (Resource.Id.eigenschappen);
			profielen = FindViewById<Button> (Resource.Id.profielen);
			checklist = FindViewById<Button> (Resource.Id.checklist);
			tinder = FindViewById<Button> (Resource.Id.tinder);

			//TEMP
			tinder.Visibility = ViewStates.Gone;

			//if(_appController. GetPreference("tips").value.Equals("true"))
				//ShowTipDialog ();

			totems.Click += (sender, eventArgs) => _appController.TotemMenuItemClicked ();
			eigenschappen.Click += (sender, eventArgs) => GoToActivity("eigenschappen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
			checklist.Click += (sender, eventArgs) => GoToActivity("checklist");
			tinder.Click += (sender, eventArgs) => GoToActivity("tinder");
		}
		protected override void OnResume ()
		{
			base.OnResume ();

			_appController.NavigationController.GotoTotemListEvent+= gotoTotemListHandler;
		}


		protected override void OnPause ()
		{
			base.OnPause ();
			_appController.NavigationController.GotoTotemListEvent-= gotoTotemListHandler;
		}

		void GoToActivity(string activity) {
			Intent intent = null;
			switch (activity) {
			case "totems":
				intent = new Intent (this, typeof(TotemsActivity));
				break;
			case "eigenschappen":
				intent = new Intent (this, typeof(EigenschappenActivity));
				break;
			case "profielen":
				intent = new Intent (this, typeof(ProfielenActivity));
				break;
			case "checklist":
				intent = new Intent (this, typeof(TotemisatieChecklistActivity));
				break;
			case "tinder":
				intent = new Intent (this, typeof(TinderEigenschappenActivity));
				break;
			}
			StartActivity (intent);
		}

		public void ShowTipDialog() {
			var dialog = TipDialog.NewInstance(this);
			RunOnUiThread (() => dialog.Show (FragmentManager, "dialog"));
		}

		public override void OnBackPressed() {
			var StartMain = new Intent (Intent.ActionMain);
			StartMain.AddCategory (Intent.CategoryHome);
			StartMain.SetFlags (ActivityFlags.NewTask);
			StartActivity (StartMain);
		}
		void gotoTotemListHandler ()
		{
			GoToActivity ("totems");
		}
	}
}