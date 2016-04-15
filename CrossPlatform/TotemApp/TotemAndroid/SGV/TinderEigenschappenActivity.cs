using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using TotemAppCore;
using Android.Views;

namespace TotemAndroid {
	[Activity (Label = "Totem bepalen", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class TinderEigenschappenActivity : BaseActivity {
		TextView adjectief;
		List<Eigenschap> eigenschappen;
		int eigenschapCount = 0;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Eigenschappen);

			//Action bar
			InitializeActionBar (SupportActionBar);

			ActionBarTitle.Text = "Eigenschappen";
	
			eigenschappen = _appController.Eigenschappen;

			Button jaKnop = FindViewById<Button> (Resource.Id.jaKnop);
			Button neeKnop = FindViewById<Button> (Resource.Id.neeKnop);

			adjectief = FindViewById<TextView> (Resource.Id.eigenschap);

			UpdateScreen ();

			jaKnop.Click += (sender, eventArgs) => Push(true);
			neeKnop.Click += (sender, eventArgs) => Push(false);
		}

		//show next eigenschap
		public void UpdateScreen() {
			if (eigenschapCount < 324) {
				adjectief.Text = eigenschappen [eigenschapCount].name;
			} else {
				var i = new Intent (this, typeof(EigenschappenActivity));
				i.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				_appController.FireSelectedEvent ();
				StartActivity (i);
			}
		}

		/*
		//create options menu
		public override bool OnCreateOptionsMenu(IMenu m) {
			IMenu menu = m;
			MenuInflater.Inflate(Resource.Menu.TinderMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//options menu: add profile, view selection of view full list
		public override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {

			//reset selection
			case Resource.Id.checklistView:
				var i = new Intent (this, typeof(EigenschappenActivity));
				i.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				StartActivity (i);
				return true;			
			}

			return base.OnOptionsItemSelected(item);
		}
		*/
			
		public void Push(bool choice) {
			eigenschappen [eigenschapCount].selected = choice;
			eigenschapCount++;
			UpdateScreen ();
		}
	}
}