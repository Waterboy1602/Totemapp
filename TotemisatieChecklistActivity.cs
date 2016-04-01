
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
	public class TotemisatieChecklistActivity : BaseActivity {
		Dictionary<string, List<string>> dictGroup;

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Checklist);

			base.InitializeActionBar (ActionBar);

			base.ActionBarTitle.Text = "Totemisatie checklist";

			InitializeExpandableListView ();
		}

		//adds header, footer and data to the ExpandableListView
		private void InitializeExpandableListView() {
			dictGroup = new Dictionary<string, List<string>> ();
			FillDictGroup ();
			var expand = FindViewById<ExpandableListView> (Resource.Id.expand);

			View view = View.Inflate (this, Resource.Layout.ExpandHeadFoot, null);
			view.FindViewById<TextView> (Resource.Id.intro).Text = Resources.GetString(Resource.String.checklist_head);
			expand.AddHeaderView (view, null, false);
			view = View.Inflate (this, Resource.Layout.ExpandHeadFoot, null);
			view.FindViewById<TextView> (Resource.Id.intro).Text = Resources.GetString(Resource.String.checklist_foot);;
			expand.AddFooterView (view, null, false);

			expand.DividerHeight = 0;
			expand.SetAdapter (new ExpendListAdapter(this, dictGroup));
		}

		//stores the data from arrays.xml in dictgroup per section
		private void FillDictGroup() {
			var voorbereiding = Resources.GetStringArray (Resource.Array.voorbereiding).ToList();
			var totemopdrachten = Resources.GetStringArray (Resource.Array.totemopdrachten).ToList();
			var geven = Resources.GetStringArray (Resource.Array.geven).ToList();
			var bezinning = Resources.GetStringArray (Resource.Array.bezinning).ToList();

			dictGroup.Add ("Voorbereiding", voorbereiding);
			dictGroup.Add ("Totemopdrachten", totemopdrachten);
			dictGroup.Add ("Geven van een totem", geven);
			dictGroup.Add ("Bezinning", bezinning);
		}
	}
}