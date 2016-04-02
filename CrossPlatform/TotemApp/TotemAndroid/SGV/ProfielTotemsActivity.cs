using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
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

		Database db;
		Toast mToast;

		string profileName;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AllTotems);

			//Action bar
			InitializeActionBar (ActionBar);
			title = ActionBarTitle;
			close = ActionBarClose;
			back = ActionBarBack;
			delete = ActionBarDelete;

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			profileName = Intent.GetStringExtra ("profileName");
			totemList = _appController.GetTotemsFromProfiel (profileName);

			totemAdapter = new TotemAdapter (this, totemList);
			allTotemListView = FindViewById<ListView> (Resource.Id.all_totem_list);
			allTotemListView.Adapter = totemAdapter;

			allTotemListView.ItemClick += ShowDetail;
			allTotemListView.ItemLongClick += DeleteTotem;

			title.Text = "Totems voor " + profileName;

			close.Click += HideDeleteTotems;

			delete.Visibility = ViewStates.Visible;
			delete.Click += ShowDeleteTotems;
		}

		//update data from adapter on restart
		protected override void OnRestart() {
			base.OnRestart ();
			totemList = _appController.GetTotemsFromProfiel (profileName);
			totemAdapter.UpdateData (totemList);
			totemAdapter.NotifyDataSetChanged ();
		}

		//get DetailActivity of the totem that is clicked
		//ID is passed as parameter
		void ShowDetail(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var detailActivity = new Intent(this, typeof(TotemDetailActivity));
			detailActivity.PutExtra ("totemID", item.nid);
			detailActivity.PutExtra ("profileName", profileName);
			StartActivity (detailActivity);
		}

		void DeleteTotem(object sender, AdapterView.ItemLongClickEventArgs e) {
			int pos = e.Position;
			var item = totemAdapter.GetItemAtPosition(pos);

			var alert = new AlertDialog.Builder (this);
			alert.SetMessage (item.title + " verwijderen uit profiel " + profileName + "?");
			alert.SetPositiveButton ("Ja", (senderAlert, args) => {
				db.DeleteTotemFromProfile(item.nid, profileName);
				mToast.SetText(item.title + " verwijderd");
				mToast.Show();
				totemList = _appController.GetTotemsFromProfiel(profileName);
				if(totemList.Count == 0) {
					base.OnBackPressed();
				} else {
					totemAdapter.UpdateData (totemList);
					totemAdapter.NotifyDataSetChanged ();
				}
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
							db.DeleteTotemFromProfile (t.nid, profileName);
				
					totemList = _appController.GetTotemsFromProfiel (profileName);
					if (totemList.Count == 0) {
						base.OnBackPressed ();
					} else {
						totemAdapter.UpdateData (totemList);
						totemAdapter.NotifyDataSetChanged ();
						HideDeleteTotems (sender, e);
					}
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