using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;

using TotemAppCore;

namespace TotemAndroid {
    [Activity (Label = "Totems")]
	public class ProfielTotemsActivity : BaseActivity {
		TotemAdapter totemAdapter;
		ListView allTotemListView;
		List<Totem> totemList;

		TextView title;
		ImageButton back;
		ImageButton delete;
		ImageButton close;

		Toast mToast;

        TextView noTotems;

        Profiel profile;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

            //if else that prevents crash when app is killed
            if (_appController.CurrentProfiel == null) {
                var i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
                Finish();
            } else {
                //Action bar
                InitializeActionBar(SupportActionBar);
                title = ActionBarTitle;
                close = ActionBarClose;
                back = ActionBarBack;
                delete = ActionBarDelete;

                //single toast for entire activity
                mToast = Toast.MakeText(this, "", ToastLength.Short);

                profile = _appController.CurrentProfiel;
                totemList = _appController.GetTotemsFromProfiel(profile.name);

                totemAdapter = new TotemAdapter(this, totemList);
                allTotemListView = FindViewById<ListView>(Resource.Id.all_totem_list);
                allTotemListView.Adapter = totemAdapter;

                allTotemListView.ItemClick += ShowDetail;
                allTotemListView.ItemLongClick += DeleteTotem;

                title.Text = "Totems voor " + profile.name;

                noTotems = FindViewById<TextView>(Resource.Id.empty_totem);

                close.Click += HideDeleteTotems;

                delete.Click += ShowDeleteTotems;

                if (totemList.Count == 0) {
                    noTotems.Visibility = ViewStates.Visible;
                    delete.Visibility = ViewStates.Gone;
                } else {
                    delete.Visibility = ViewStates.Visible;
                }
            }
		}
			
		protected override void OnResume ()	{
			base.OnResume ();

			_appController.NavigationController.GotoTotemDetailEvent+= StartDetailActivity;
            _appController.NavigationController.GotoEigenschapListEvent += gotoEigenschappenList;
        }

		protected override void OnPause ()	{
			base.OnPause ();

			_appController.NavigationController.GotoTotemDetailEvent-= StartDetailActivity;
            _appController.NavigationController.GotoEigenschapListEvent -= gotoEigenschappenList;
        }

		void StartDetailActivity() {
			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			StartActivity (detailActivity); 
		}

		//update data from adapter on restart
		protected override void OnRestart() {
			base.OnRestart ();
            UpdateList();
		}

        void UpdateList() {
            totemList = _appController.GetTotemsFromProfiel(profile.name);
            if (totemList.Count == 0) {
                noTotems.Visibility = ViewStates.Visible;
                delete.Visibility = ViewStates.Gone;
            } else {
                noTotems.Visibility = ViewStates.Gone;
                delete.Visibility = ViewStates.Visible;
            }
            totemAdapter.UpdateData(totemList);
            totemAdapter.NotifyDataSetChanged();
        }

        //get DetailActivity of the totem that is clicked
        //ID is passed as parameter
        void ShowDetail(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			_appController.ProfileTotemSelected (profile.name, item.nid);
		}

        //create options menu
        public override bool OnCreateOptionsMenu(IMenu m) {
            MenuInflater.Inflate(Resource.Menu.ProfielMenu, m);
            return base.OnCreateOptionsMenu(m);
        }

        //options menu: add profile, view selection of view full list
        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {

                //show selected only
                case Resource.Id.showSelectionProfile:
                    _appController.ProfileEigenschappenSelected(profile.name);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        void gotoEigenschappenList() {
            var i = new Intent(this, typeof(EigenschappenActivity));
            StartActivity(i);
        }

        void DeleteTotem(object sender, AdapterView.ItemLongClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var alert = new AlertDialog.Builder (this);
			alert.SetMessage (item.title + " verwijderen uit profiel " + profile.name + "?");
			alert.SetPositiveButton ("Ja", (senderAlert, args) => {
				_appController.DeleteTotemFromProfile(item.nid, profile.name);
				mToast.SetText(item.title + " verwijderd");
				mToast.Show();
                UpdateList();
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {});

			Dialog dialog = alert.Create();
			RunOnUiThread (dialog.Show);
		}

		//also sets all the checkboxes unchecked
		private void ShowDeleteTotems(object sender, EventArgs e) {
			foreach (Totem t in totemList)
				t.selected = false;

			totemAdapter.ShowDelete ();
			totemAdapter.UpdateData (totemList);
			totemAdapter.NotifyDataSetChanged ();

			back.Visibility = ViewStates.Gone;
			close.Visibility = ViewStates.Visible;
			title.Visibility = ViewStates.Gone;

			delete.Click -= ShowDeleteTotems;
			delete.Click += RemoveSelectedTotems;
		}

		private void HideDeleteTotems(object sender, EventArgs e) {
			totemAdapter.HideDelete ();
			totemAdapter.NotifyDataSetChanged ();

			back.Visibility = ViewStates.Visible;
			close.Visibility = ViewStates.Gone;
			title.Visibility = ViewStates.Visible;

			delete.Click -= RemoveSelectedTotems;
			delete.Click += ShowDeleteTotems;
		}

		private void RemoveSelectedTotems(object sender, EventArgs e) {
			bool selected = false;
			foreach(Totem t in totemList) {
				if (t.selected) {
					selected = true;
					break;
				}
			}
			if (selected) {
				var alert1 = new AlertDialog.Builder (this);
				alert1.SetMessage ("Geselecteerde totems verwijderen?");
				alert1.SetPositiveButton ("Ja", (senderAlert, args) => {
					foreach (Totem t in totemList)
						if (t.selected)
							_appController.DeleteTotemFromProfile (t.nid, profile.name);

                    UpdateList();
                    HideDeleteTotems(sender, e);
                });

				alert1.SetNegativeButton ("Nee", (senderAlert, args) => {});

				Dialog d2 = alert1.Create ();

				RunOnUiThread (d2.Show);
			} else {
				mToast.SetText("Geen totems geselecteerd om te verwijderen");
				mToast.Show();
			}
		}
	}
}