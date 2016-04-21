using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace TotemAndroid {
    [Activity (Label = "Totemapp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppThemeNoAction")]
	public class MainActivity : BaseActivity {
		//Database db;
		Button totems;
		Button eigenschappen;
		Button profielen;
		Button checklist;

        Toast mToast;
        View toastView;

        protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			totems = FindViewById<Button> (Resource.Id.totems);
			eigenschappen = FindViewById<Button> (Resource.Id.eigenschappen);
			profielen = FindViewById<Button> (Resource.Id.profielen);
			checklist = FindViewById<Button> (Resource.Id.checklist);

			totems.Click += (sender, eventArgs) => _appController.TotemMenuItemClicked ();
			eigenschappen.Click += (sender, eventArgs) => _appController.EigenschappenMenuItemClicked ();
			profielen.Click += (sender, eventArgs) => _appController.ProfileMenuItemClicked ();
			checklist.Click += (sender, eventArgs) => _appController.ChecklistMenuItemClicked ();

            TextView title = FindViewById<TextView>(Resource.Id.totemapp_title);
            title.LongClick += Title_LongClick;

            LayoutInflater mInflater = LayoutInflater.From(this);
            toastView = mInflater.Inflate(Resource.Layout.InfoToast, null);
        }

        private void Title_LongClick(object sender, Android.Views.View.LongClickEventArgs e) {
            mToast = new Toast(this);
            mToast.Duration = ToastLength.Long;
            mToast.SetGravity(GravityFlags.Center | GravityFlags.Bottom, 0, 25);

            toastView.Visibility = ViewStates.Visible;
            mToast.View = toastView;
            mToast.Show();
        }

        protected override void OnResume ()	{
			base.OnResume ();

            _appController.NavigationController.GotoTotemListEvent+= gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent+= gotoEigenschappenListHandler;
			_appController.NavigationController.GotoProfileListEvent+= gotoProfileListHandler;
			_appController.NavigationController.GotoChecklistEvent+= gotoChecklistHandler;
		}

		protected override void OnPause () {
			base.OnPause ();
            toastView.Visibility = ViewStates.Gone;

            _appController.NavigationController.GotoTotemListEvent-= gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent-= gotoEigenschappenListHandler;
			_appController.NavigationController.GotoProfileListEvent-= gotoProfileListHandler;
			_appController.NavigationController.GotoChecklistEvent-= gotoChecklistHandler;
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

		void gotoTotemListHandler () {
			GoToActivity ("totems");
		}

		void gotoEigenschappenListHandler () {
			GoToActivity ("eigenschappen");
		}

		void gotoProfileListHandler () {
			GoToActivity ("profielen");
		}

		void gotoChecklistHandler () {
			GoToActivity ("checklist");
		}
	}
}