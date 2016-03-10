
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
using Android.Graphics;
using Android.Text.Method;
using Android.Views.InputMethods;


namespace Totem
{
	[Activity (Label = "Totem detail")]			
	public class TotemDetailActivity : Activity
	{
		TextView title;
		TextView synonyms;
		TextView body;

		Database db;
		Toast mToast;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			RequestWindowFeature(WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.TotemDetail);

			db = DatabaseHelper.GetInstance (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			Button voegtoe = FindViewById<Button> (Resource.Id.voegtoe);

			title = FindViewById<TextView> (Resource.Id.title);
			Typeface face = Typeface.CreateFromAsset(Assets,"fonts/DCC - Ash.otf");
			title.SetTypeface (face, 0);

			synonyms = FindViewById<TextView> (Resource.Id.synonyms);
			body = FindViewById<TextView> (Resource.Id.body);

			var nid = Intent.GetStringExtra ("totemID");
			var hideButton = Intent.GetStringExtra ("hideButton");
			if (hideButton != null)
				voegtoe.Visibility = ViewStates.Gone;
			
			GetInfo (nid);

			//add to profiles
			voegtoe.Click += (sender, eventArgs) => {
				PopupMenu menu = new PopupMenu (this, voegtoe);
				menu.Inflate (Resource.Menu.Popup);
				int count = 0;
				foreach(Profiel p in db.GetProfielen()) {
					menu.Menu.Add(0,count,count,p.name);
					count++;
				}

				menu.Menu.Add(0,count,count, "Nieuw profiel");

				menu.MenuItemClick += (s1, arg1) => {
					if(arg1.Item.TitleFormatted.ToString().Equals("Nieuw profiel")) {
						AlertDialog.Builder alert = new AlertDialog.Builder (this);
						alert.SetTitle ("Naam");
						EditText input = new EditText (this); 
						input.InputType = Android.Text.InputTypes.TextFlagCapWords;
						ShowKeyboard (input);
						alert.SetView (input);
						alert.SetPositiveButton ("Ok", (s, args) => {
							string value = input.Text;
							if(value.Replace("'", "").Equals("")) {
								mToast.SetText("Ongeldige naam");
								mToast.Show();
								HideKeyboard(); 
							} else if(db.GetProfielNamen().Contains(value)) {
								input.Text = "";
								mToast.SetText("Profiel " + value + " bestaat al");
								mToast.Show();
								HideKeyboard(); 
							} else {
								db.AddProfile(value);
								HideKeyboard();
								db.AddTotemToProfiel(nid, value);
								mToast.SetText(db.GetTotemOnID(nid).title + " toegevoegd aan profiel " + value.Replace("'", ""));
								mToast.Show();
							}
						});
							
						RunOnUiThread (() => {
							alert.Show();
						} );

					} else {
						db.AddTotemToProfiel(nid, arg1.Item.TitleFormatted.ToString());
						mToast.SetText(db.GetTotemOnID(nid).title + " toegevoegd aan profiel " + arg1.Item.TitleFormatted.ToString());
						mToast.Show();
					}
				};

				menu.Show ();
			};
		}

		//displays totem info
		private void GetInfo(string idx) {
			Totem t = db.GetTotemOnID (idx);
			title.Text = t.title;
			if(t.synonyms != null) {
				synonyms.Text = t.synonyms;
			}
			body.Text = t.body;
		}

		//helper
		public void ShowKeyboard(View pView) {
			pView.RequestFocus();

			InputMethodManager inputMethodManager = Application.GetSystemService(Context.InputMethodService) as InputMethodManager;
			inputMethodManager.ShowSoftInput(pView, ShowFlags.Forced);
			inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
		}

		//helper
		public void HideKeyboard() {
			InputMethodManager inputManager = (InputMethodManager)this.GetSystemService (Context.InputMethodService);
			inputManager.ToggleSoftInput (ShowFlags.Implicit, 0);
		}
	}
}