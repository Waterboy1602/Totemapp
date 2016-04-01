using System;
using System.IO;
using System.Collections.Generic;

using SQLite;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Content.PM;

namespace Totem {
	[Activity (Label = "Totem bepalen", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class TinderEigenschappenActivity : BaseActivity {
		TextView adjectief;
		List<Eigenschap> eigenschappen;
		int eigenschapCount = 1;

		//dictionary with totem IDs as keys and frequencies as values
		Dictionary<int, int> freqs;

		Database db;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Eigenschappen);

			//Action bar
			base.InitializeActionBar (ActionBar);

			base.ActionBarTitle.Text = "Eigenschappen";

			db = DatabaseHelper.GetInstance (this);
			eigenschappen = db.GetEigenschappen ();

			freqs = new Dictionary<int, int> ();

			Button jaKnop = FindViewById<Button> (Resource.Id.jaKnop);
			Button neeKnop = FindViewById<Button> (Resource.Id.neeKnop);

			adjectief = FindViewById<TextView> (Resource.Id.eigenschap);

			UpdateScreen ();

			jaKnop.Click += (sender, eventArgs) => PushJa();
			neeKnop.Click += (sender, eventArgs) => PushNee();
		}

		//show next eigenschap
		public void UpdateScreen() {
			if(eigenschapCount < 324)
				adjectief.Text = eigenschappen [eigenschapCount].name;
			else
				VindTotem ();
		}

		//redirect to the result activity
		//totems and their frequencies are sorted and passed seperately as parameters
		public void VindTotem() {
			var totemsActivity = new Intent(this, typeof(ResultTotemsActivity));

			int[] sortedTotems = CollectionHelper.GetSortedList (freqs, true);
			int[] sortedFreqs = CollectionHelper.GetSortedList (freqs, false);
			totemsActivity.PutExtra ("totemIDs", sortedTotems);
			totemsActivity.PutExtra ("freqs", sortedFreqs);
			totemsActivity.PutExtra ("GoToMain", true);

			StartActivity(totemsActivity);
		}

		//adds totem IDs related to the eigenschap to freqs
		//increases eigenschap count next
		public void PushJa() {
			List<Totem_eigenschap> toevoegen = db.GetTotemsVanEigenschapsID (eigenschappen[eigenschapCount].tid);
			foreach(Totem_eigenschap totem in toevoegen) {
				int idx = Convert.ToInt32 (totem.nid);
				CollectionHelper.AddOrUpdateDictionaryEntry (freqs, idx) ;
			}
			eigenschapCount++;
			UpdateScreen ();
		}

		//increases eigenschap count
		public void PushNee() {
			eigenschapCount++;
			UpdateScreen ();
		}
	}
}