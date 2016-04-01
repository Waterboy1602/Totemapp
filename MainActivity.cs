using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Totem {
	[Activity (Label = "Totemapp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppThemeNoAction")]
	public class MainActivity : BaseActivity {
		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			db = DatabaseHelper.GetInstance (this);

			SetContentView (Resource.Layout.Main);

			Button totems = FindViewById<Button> (Resource.Id.totems);
			Button eigenschappen = FindViewById<Button> (Resource.Id.eigenschappen);
			Button profielen = FindViewById<Button> (Resource.Id.profielen);
			Button checklist = FindViewById<Button> (Resource.Id.checklist);
			Button tinder = FindViewById<Button> (Resource.Id.tinder);

			//TEMP
			tinder.Visibility = ViewStates.Gone;

			if(db.GetPreference("tips").value.Equals("true"))
				ShowTipDialog ();

			totems.Click += (sender, eventArgs) => GoToActivity("totems");
			eigenschappen.Click += (sender, eventArgs) => GoToActivity("eigenschappen");
			profielen.Click += (sender, eventArgs) => GoToActivity("profielen");
			checklist.Click += (sender, eventArgs) => GoToActivity("checklist");
			tinder.Click += (sender, eventArgs) => GoToActivity("tinder");
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
	}
}