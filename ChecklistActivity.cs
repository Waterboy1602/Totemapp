
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
			//var intro = FindViewById<TextView> (Resource.Id.intro);
			//intro.Text = "Een totemisatie vergt tijd en inspanning.\nDeze checklist leidt je doorheen de totemmap en helpt om niets te vergeten. Sta even stil bij jullie totemisatie en check of dit overeenstemt met onze lijst."; 
			dictGroup = new Dictionary<string, List<string>> ();
			FillExpandList ();
			var expand = FindViewById<ExpandableListView> (Resource.Id.expand);
			View view = View.Inflate (this, Resource.Layout.ExpandHeadFoot, null);
			view.FindViewById<TextView> (Resource.Id.intro).Text = "Een totemisatie vergt tijd en inspanning.\nDeze checklist leidt je doorheen de totemmap en helpt om niets te vergeten. Sta even stil bij jullie totemisatie en check of dit overeenstemt met onze lijst.";
			expand.AddHeaderView (view, null, false);
			view = View.Inflate (this, Resource.Layout.ExpandHeadFoot, null);
			view.FindViewById<TextView> (Resource.Id.intro).Text = "VEEL SUCCES!";
			expand.AddFooterView (view, null, false);
			expand.DividerHeight = 0;
			expand.SetAdapter (new ExpendListAdapter(this, dictGroup));
		}

		private void FillExpandList() {
			var voorbereiding = new List<string> ();
			var totemopdrachten = new List<string> ();
			var geven = new List<string> ();
			var bezinning = new List<string> ();

			voorbereiding.Add ("h_Sta stil bij:");
			voorbereiding.Add ("n_Welke totemvorm?");
			voorbereiding.Add ("n_Traditie");
			voorbereiding.Add ("n_Een totem kiezen");
			voorbereiding.Add ("i_Begin er ruim op voorhand mee");
			voorbereiding.Add ("i_Neem er tijd voor");
			voorbereiding.Add ("i_Verzamel eigenschappen");
			voorbereiding.Add ("i_Zorg voor inspraak");
			voorbereiding.Add ("i_Weeg alle opties af");
			voorbereiding.Add ("i_Maak een zorgvuldige keuze en bekijk de alternatieve namen");
			voorbereiding.Add ("n_Wie is er aanwezig?");
			voorbereiding.Add ("n_Waar en wanneer?");
			voorbereiding.Add ("n_Inkleding, rituelen");

			totemopdrachten.Add ("n_Individuele opdracht");
			totemopdrachten.Add ("n_Groepsopdracht");
			totemopdrachten.Add ("n_Doeopdracht");
			totemopdrachten.Add ("n_Denkopdracht");
			totemopdrachten.Add ("h_De opdracht is...");
			totemopdrachten.Add ("n_zinvol");
			totemopdrachten.Add ("n_haalbaar");
			totemopdrachten.Add ("n_fysiek veilig");
			totemopdrachten.Add ("n_op maat van het individu en de groep");
			totemopdrachten.Add ("n_emotioneel veilig?");
			totemopdrachten.Add ("i_met toestemming van het lid");
			totemopdrachten.Add ("i_het past in de context");
			totemopdrachten.Add ("i_het past bij de doelgroep");
			totemopdrachten.Add ("i_het hoort bij de ontwikkeling van het lid");
			totemopdrachten.Add ("i_het is met respect voor het lid gegeven");
			totemopdrachten.Add ("i_het lid behoudt zijn zelfrespect");

			geven.Add ("h_Denk aan:");
			geven.Add ("n_Sfeer");
			geven.Add ("n_Intimiteit");
			geven.Add ("n_Ceremonie");
			geven.Add ("n_Ritueel");
			geven.Add ("n_Aandenken");
			geven.Add ("n_Nabespreking / nazorg");

			bezinning.Add ("h_Individueel:");
			bezinning.Add ("h_Totemisant denkt na over...");
			bezinning.Add ("n_zichzelf");
			bezinning.Add ("n_zijn inzet");
			bezinning.Add ("n_zijn engagement");
			bezinning.Add ("h_In groep:");
			bezinning.Add ("n_Stilstaan bij elke totemisant");
			bezinning.Add ("n_Stilstaan bij zijn rol in de groep");
			bezinning.Add ("n_Stilstaan bij scouting");

			dictGroup.Add ("Voorbereiding", voorbereiding);
			dictGroup.Add ("Totemopdrachten", totemopdrachten);
			dictGroup.Add ("Geven van een totem", geven);
			dictGroup.Add ("Bezinning", bezinning);
		}
	}
}