using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using System.Collections.Generic;
using System.Linq;

using TotemAppCore;

namespace TotemAndroid {
    [Activity (Label = "Totems")]			
	public class ResultTotemsActivity : BaseActivity {
		TotemAdapter totemAdapter;
		ListView totemListView;
		List<Totem> totemList;

		TextView title;
		ImageButton back;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Totems);

			//Action bar
			InitializeActionBar (SupportActionBar);
			title = ActionBarTitle;
			back = ActionBarBack;
	
			int selected = Intent.GetIntExtra ("selected", 0);

			totemList = _appController.TotemEigenschapDict.Keys.ToList();
			var freqs = _appController.TotemEigenschapDict.Values.ToArray ();

			totemAdapter = new TotemAdapter (this, totemList, freqs, selected);
			totemListView = FindViewById<ListView> (Resource.Id.totem_list);
			totemListView.Adapter = totemAdapter;

			totemListView.ItemClick += ShowDetail;

			title.Text = "Totems";
		}

		protected override void OnResume ()	{
			base.OnResume ();

			_appController.NavigationController.GotoTotemDetailEvent += StartDetailActivity;
		}

		protected override void OnPause ()	{
			base.OnPause ();

			_appController.NavigationController.GotoTotemDetailEvent -= StartDetailActivity;
		}

		//fill totemList with Totem-objects whose ID is in totemIDs
		List<Totem> ConvertIDArrayToTotemList(int[] totemIDs) {
			var list = new List<Totem> ();
			foreach(int idx in totemIDs)
				list.Add (_appController.GetTotemOnID (idx));

			return list;
		}

		void ShowDetail(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			_appController.TotemSelected (item.nid);
		}

		void StartDetailActivity() {
			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			StartActivity (detailActivity); 
		}

		//goes back to main screen when GoToMain is set to true
		//otherwise acts normal
		public override void OnBackPressed() {
			if (Intent.GetBooleanExtra ("GoToMain", false)) {
				var i = new Intent (this, typeof(MainActivity));
				i.SetFlags (ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				StartActivity (i);
			} else {
				base.OnBackPressed ();
			}
		}
	}
}