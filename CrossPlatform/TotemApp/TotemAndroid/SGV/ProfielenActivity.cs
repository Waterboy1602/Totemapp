using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using TotemAppCore;

namespace TotemAndroid {
	[Activity (Label = "Profielen", WindowSoftInputMode=SoftInput.StateAlwaysHidden)]			
	public class ProfielenActivity : BaseActivity {
		ProfielAdapter profielAdapter;
		ListView profielenListView;
		List<Profiel> profielen;

		TextView title;
		ImageButton back;
		ImageButton close;
		ImageButton add;
		ImageButton delete;
		TextView noProfiles;
		Toast mToast;

		protected override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Profielen);

			//Action bar
			InitializeActionBar (ActionBar);
			title = ActionBarTitle;
			close = ActionBarClose;
			back = ActionBarBack;
			add = ActionBarAdd;
			delete = ActionBarDelete;

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			profielen = _appController.DistinctProfielen;
			
			profielAdapter = new ProfielAdapter (this, profielen);
			profielenListView = FindViewById<ListView> (Resource.Id.profielen_list);
			profielenListView.Adapter = profielAdapter;

			profielenListView.ItemClick += ShowTotems;
			profielenListView.ItemLongClick += DeleteProfile;

			noProfiles = FindViewById<TextView> (Resource.Id.empty_profiel);

			title.Text = "Profielen";

			add.Visibility = ViewStates.Visible;
			add.Click += (sender, e) => AddProfile ();

			delete.Click += ShowDeleteProfiles;
			close.Click += HideDeleteProfiles;

			if (profielen.Count == 0) {
				noProfiles.Visibility = ViewStates.Visible;
				delete.Visibility = ViewStates.Gone;
			} else {
				delete.Visibility = ViewStates.Visible;
			}
		}

		//updates data of the adapter and shows/hides the "empty"-message when needed
		void UpdateList() {
			this.profielen = _appController.DistinctProfielen;
			if (profielen.Count == 0) {
				noProfiles.Visibility = ViewStates.Visible;
				delete.Visibility = ViewStates.Gone;
			} else {
				noProfiles.Visibility = ViewStates.Gone;
				delete.Visibility = ViewStates.Visible;
			}
			profielAdapter.UpdateData(profielen);
			profielAdapter.NotifyDataSetChanged();
		}

		void ShowTotems(object sender, AdapterView.ItemClickEventArgs e) {
			int pos = e.Position;
			var item = profielAdapter.GetItemAtPosition(pos);

			if (_appController.GetTotemsFromProfiel (item.name).Count == 0) {
				mToast.SetText("Profiel " + item.name + " bevat geen totems");
				mToast.Show();
			} else {
				var totemsActivity = new Intent (this, typeof(ProfielTotemsActivity));
				totemsActivity.PutExtra ("profileName", item.name);
				StartActivity (totemsActivity);
			}
		}

		void DeleteProfile(object sender, AdapterView.ItemLongClickEventArgs e) {
			int pos = e.Position;
			var item = profielAdapter.GetItemAtPosition(pos);

			var alert = new AlertDialog.Builder (this);
			alert.SetMessage ("Profiel " + item.name + " verwijderen?");
			alert.SetPositiveButton ("Ja", (senderAlert, args) => {
				_appController.DeleteProfile(item.name);
				mToast.SetText("Profiel " + item.name + " verwijderd");
				mToast.Show();
				UpdateList();
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {});

			Dialog dialog = alert.Create();
			RunOnUiThread (dialog.Show);
		}

		void AddProfile() {
			var alert = new AlertDialog.Builder (this);
			alert.SetTitle ("Nieuw profiel");
			var input = new EditText (this); 
			input.InputType = Android.Text.InputTypes.TextFlagCapWords;
			input.Hint = "Naam";
			KeyboardHelper.ShowKeyboard (this, input);
			alert.SetView (input);
			alert.SetPositiveButton ("Ok", (sender, args) => {
				string value = input.Text;
				if(value.Replace("'", "").Replace(" ", "").Equals("")) {
					mToast.SetText("Ongeldige naam");
					mToast.Show();				
				} else if(_appController.GetProfielNamen().Contains(value)) {
					input.Text = "";
					mToast.SetText("Profiel " + value + " bestaat al");
					mToast.Show();
				} else {
					_appController.AddProfile(value);
					UpdateList();
				}
			});

			AlertDialog d1 = alert.Create();

			//add profile when enter is clicked
			input.EditorAction += (sender, e) => {
				if (e.ActionId == ImeAction.Done)
					d1.GetButton(-1).PerformClick();
				else
					e.Handled = false;
			};

			RunOnUiThread (d1.Show);
		}
			
		void ShowDeleteProfiles(object sender, EventArgs e) {
			profielAdapter.ShowDelete ();
			profielAdapter.NotifyDataSetChanged ();

			back.Visibility = ViewStates.Gone;
			close.Visibility = ViewStates.Visible;
			title.Visibility = ViewStates.Gone;
			add.Visibility = ViewStates.Gone;

			delete.Click -= ShowDeleteProfiles;
			delete.Click += RemoveSelectedProfiles;
		}

		void HideDeleteProfiles(object sender, EventArgs e) {
			profielAdapter.HideDelete ();
			profielAdapter.NotifyDataSetChanged ();

			back.Visibility = ViewStates.Visible;
			close.Visibility = ViewStates.Gone;
			title.Visibility = ViewStates.Visible;
			add.Visibility = ViewStates.Visible;

			delete.Click -= RemoveSelectedProfiles;
			delete.Click += ShowDeleteProfiles;
		}

		void RemoveSelectedProfiles(object sender, EventArgs e) {
			bool selected = false;
			foreach(Profiel p in profielen) {
				if (p.selected) {
					selected = true;
					break;
				}
			}

			if (selected) {		
				var alert1 = new AlertDialog.Builder (this);
				alert1.SetMessage ("Geselecteerde profielen verwijderen?");
				alert1.SetPositiveButton ("Ja", (senderAlert, args) => {
					foreach (Profiel p in profielen)
						if (p.selected)
							_appController.DeleteProfile (p.name);
				
					UpdateList ();
					HideDeleteProfiles (sender, e);
				});

				alert1.SetNegativeButton ("Nee", (senderAlert, args) => {});

				Dialog d2 = alert1.Create ();

				RunOnUiThread (d2.Show);
			} else {
				mToast.SetText("Geen profielen geselecteerd om te verwijderen");
				mToast.Show();
			}
		}
	}
}