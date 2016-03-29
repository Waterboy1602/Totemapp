
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
	[Activity (Label = "Checklist")]			
	public class ChecklistActivity : Activity {

		Dictionary<string, List<string>> dictGroup;

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Checklist);

			ActionBar mActionBar = ActionBar;

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			var title = mCustomView.FindViewById<TextView> (Resource.Id.title);
			title.Text = "Totemisatie checklist";

			var back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			var search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.Visibility = ViewStates.Gone;

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);

			FillData ();
		}

		private void FillData() {
			var intro = FindViewById<TextView> (Resource.Id.intro);
			intro.Text = "Een totemisatie vergt tijd en inspanning.\nDeze checklist leidt je doorheen de totemmap en helpt om niets te vergeten. Sta even stil bij jullie totemisatie en check of dit overeenstemt met onze lijst."; 
			dictGroup = new Dictionary<string, List<string>> ();
			FillExpandList ();
			var expand = FindViewById<ExpandableListView> (Resource.Id.expand);
			expand.DividerHeight = 0;
			expand.SetAdapter (new ExpendListAdapter(this, dictGroup));
		}

		private void FillExpandList() {
			var voorbereiding = new List<string> ();
			var totemopdrachten = new List<string> ();
			var geven = new List<string> ();
			var bezinning = new List<string> ();

			voorbereiding.Add ("Welke totemvorm?");
			voorbereiding.Add ("Traditie");
			voorbereiding.Add ("Een totem kiezen");
			voorbereiding.Add ("Wie is er aanwezig?");
			voorbereiding.Add ("Waar en wanneer?");
			voorbereiding.Add ("Inkleding, rituelen");

			totemopdrachten.Add ("A");
			totemopdrachten.Add ("B");
			totemopdrachten.Add ("C");

			geven.Add ("A");
			geven.Add ("B");
			geven.Add ("C");

			bezinning.Add ("A");
			bezinning.Add ("B");
			bezinning.Add ("C");

			dictGroup.Add ("Voorbereiding", voorbereiding);
			dictGroup.Add ("Totemopdrachten", totemopdrachten);
			dictGroup.Add ("Geven van een totem", geven);
			dictGroup.Add ("Bezinning", bezinning);
		}
	}
}