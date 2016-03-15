
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


namespace Totem {
	[Activity (Label = "Totem detail")]			
	public class TotemDetailActivity : Activity	{
		TextView number;
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

			Typeface Vervein = Typeface.CreateFromAsset(Assets,"fonts/Verveine W01 Regular.ttf");
			Typeface Din = Typeface.CreateFromAsset(Assets,"fonts/DINPro-Light.ttf");

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			Button voegtoe = FindViewById<Button> (Resource.Id.voegtoe);
			voegtoe.SetTypeface(Din, 0);

			number = FindViewById<TextView> (Resource.Id.number);
			title = FindViewById<TextView> (Resource.Id.title);
			synonyms = FindViewById<TextView> (Resource.Id.synonyms);
			body = FindViewById<TextView> (Resource.Id.body);

			number.SetTypeface (Vervein, 0);
			title.SetTypeface (Vervein, 0);
			synonyms.SetTypeface (Din, TypefaceStyle.Italic);
			body.SetTypeface (Din, 0);

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
						KeyboardHelper.ShowKeyboardDialog(this, input);
						alert.SetView (input);
						alert.SetPositiveButton ("Ok", (s, args) => {
							string value = input.Text;
							if(value.Replace("'", "").Equals("")) {
								mToast.SetText("Ongeldige naam");
								mToast.Show();
								KeyboardHelper.HideKeyboardDialog(this);
							} else if(db.GetProfielNamen().Contains(value)) {
								input.Text = "";
								mToast.SetText("Profiel " + value + " bestaat al");
								mToast.Show();
								KeyboardHelper.HideKeyboardDialog(this);
							} else {
								db.AddProfile(value);
								KeyboardHelper.HideKeyboardDialog(this);
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
			number.Text = t.number + ". ";
			title.Text = t.title;
			if(t.synonyms != null) {
				synonyms.Text = t.synonyms;
			}
			body.Text = t.body;
		}
	}
}