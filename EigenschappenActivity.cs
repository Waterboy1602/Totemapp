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

namespace Totem
{
	[Activity (Label = "Totem bepalen", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class EigenschappenActivity : Activity
	{
		TextView adjectief;
		List<Eigenschap> eigenschappen;
		int eigenschapCount;
		Dictionary<int, int> freqs;
		Database db;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Eigenschappen);

			//setup
			db = new Database(this);
			eigenschappen = db.getEigenschappen ();

			eigenschapCount = 1;
			freqs = new Dictionary<int, int> ();

			Button jaKnop = FindViewById<Button> (Resource.Id.jaKnop);
			Button neeKnop = FindViewById<Button> (Resource.Id.neeKnop);

			adjectief = FindViewById<TextView> (Resource.Id.eigenschap);
			Typeface face = Typeface.CreateFromAsset(Assets,"fonts/DCC - Ash.otf");
			adjectief.SetTypeface (face, 0);

			UpdateScreen ();

			jaKnop.Click += (sender, eventArgs) => pushJa();
			neeKnop.Click += (sender, eventArgs) => pushNee();
		}



		public void UpdateScreen() {
			if(eigenschapCount < 359) {
				adjectief.Text = eigenschappen [eigenschapCount].name;
			} else {
				ResultList ();
			}
		}

		public void ResultList() {
			var totemsActivity = new Intent(this, typeof(TotemsActivity));

			int[] sortedTotems = DictMethods.getSortedTotemIDList (freqs);
			int[] sortedFreqs = DictMethods.getSortedFreqList (freqs);
			totemsActivity.PutExtra ("totemIDs", sortedTotems);
			totemsActivity.PutExtra ("freqs", sortedFreqs);

			StartActivity(totemsActivity);
		}

		public void pushJa() {
			List<Totem_eigenschap> toevoegen = db.getTotemsVanEigenschapsID (eigenschappen[eigenschapCount].tid);
			foreach(Totem_eigenschap totem in toevoegen) {
				int idx = Convert.ToInt32 (totem.nid);
				DictMethods.AddOrUpdateDictionaryEntry (freqs, idx) ;
			}
			eigenschapCount++;
			UpdateScreen ();
		}

		public void pushNee() {
			eigenschapCount++;
			UpdateScreen ();
		}
	}
}


