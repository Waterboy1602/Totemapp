using System;
using System.Collections.Generic;
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
using Android.Text;

namespace Totem {
	[Activity (Label = "Beschrijving", WindowSoftInputMode=SoftInput.StateAlwaysHidden)]			
	public class TotemDetailActivity : BaseActivity	{
		CustomFontTextView number;
		TextView title_synonyms;
		CustomFontTextView body;

		Database db;
		Toast mToast;

		TextView title;
		ImageButton back;
		ImageButton action;

		String nid;
		Totem t;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.TotemDetail);

			//Action bar
			base.InitializeActionBar (ActionBar);
			title = base.ActionBarTitle;
			back = base.ActionBarBack;

			db = DatabaseHelper.GetInstance (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			number = FindViewById<CustomFontTextView> (Resource.Id.number);
			title_synonyms = FindViewById<TextView> (Resource.Id.title_synonyms);
			body = FindViewById<CustomFontTextView> (Resource.Id.body);

			title.Text = "Beschrijving";

			nid = Intent.GetStringExtra ("totemID");
			t = db.GetTotemOnID (nid);

			var profileName = Intent.GetStringExtra ("profileName");
			if (profileName != null) {
				action = base.ActionBarDelete;
				action.Click += (object sender, EventArgs e) => RemoveFromProfile (profileName);
			} else {
				action = base.ActionBarAdd;
				action.Click += (object sender, EventArgs e) => ProfilePopup();
			}
			action.Visibility = ViewStates.Visible;

			GetInfo (nid);
		}

		private void RemoveFromProfile(string profileName) {
			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetMessage (t.title + " verwijderen uit profiel " + profileName + "?");
			alert.SetPositiveButton ("Ja", (senderAlert, args) => {
				db.DeleteTotemFromProfile(t.nid, profileName);
				mToast.SetText(t.title + " verwijderd");
				mToast.Show();
				if(db.GetTotemsFromProfiel(profileName).Count == 0) {
					Intent i = new Intent(this, typeof(ProfielenActivity));
					i.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
					StartActivity(i);
				} else {
					base.OnBackPressed();
				}
			});

			alert.SetNegativeButton ("Nee", (senderAlert, args) => {});

			Dialog dialog = alert.Create();
			RunOnUiThread (() => {
				dialog.Show();
			} );
		}

		private void ProfilePopup() {
				PopupMenu menu = new PopupMenu (this, action);
				menu.Inflate (Resource.Menu.Popup);
				int count = 0;
				foreach(Profiel p in db.GetProfielen()) {
					menu.Menu.Add(0,count,count,p.name);
					count++;
				}

				menu.Menu.Add(0,count,count, "Nieuw profiel");

				menu.MenuItemClick += (s1, arg1) => {
					if(arg1.Item.ItemId == count) {
						AlertDialog.Builder alert = new AlertDialog.Builder (this);
						alert.SetTitle ("Nieuw profiel");
						EditText input = new EditText (this);
						input.InputType = Android.Text.InputTypes.TextFlagCapWords;
						input.Hint = "Naam";
						KeyboardHelper.ShowKeyboard(this, input);
						alert.SetView (input);
						alert.SetPositiveButton ("Ok", (s, args) => {
							string value = input.Text;
							if(value.Replace("'", "").Replace(" ", "").Equals("")) {
								mToast.SetText("Ongeldige naam");
								mToast.Show();
							} else if(db.GetProfielNamen().Contains(value)) {
								input.Text = "";
								mToast.SetText("Profiel " + value + " bestaat al");
								mToast.Show();
							} else {
								db.AddProfile(value);
								db.AddTotemToProfiel(nid, value);
								mToast.SetText(db.GetTotemOnID(nid).title + " toegevoegd aan profiel " + value.Replace("'", ""));
								mToast.Show();
							}
						});

						AlertDialog d1 = alert.Create();

						//add profile when enter is clicked
						input.EditorAction += (s2, e) => {
							if (e.ActionId == ImeAction.Done)
								d1.GetButton(-1).PerformClick();
							else
								e.Handled = false;
						};

						RunOnUiThread (() => {
							d1.Show();
						});

					} else {
						db.AddTotemToProfiel(nid, arg1.Item.TitleFormatted.ToString());
						mToast.SetText(db.GetTotemOnID(nid).title + " toegevoegd aan profiel " + arg1.Item.TitleFormatted.ToString());
						mToast.Show();
					}
				};

				menu.Show ();
		}
			
		private int ConvertDPToPixels(float dp) {
			float scale = Resources.DisplayMetrics.Density;
			int result =  (int)(dp * scale + 0.5f);
			return result;
		}

		//displays totem info
		private void GetInfo(string idx) {
			number.Text = t.number + ". ";
			Typeface Verveine = Typeface.CreateFromAsset (Assets, "fonts/Verveine W01 Regular.ttf");

			//code to get formatting right
			//title and synonyms are in the same TextView
			//font, size,... are given using spans
			if (t.synonyms != null) {
				string titlestring = t.title;
				string synonymsstring = " - " + t.synonyms + " ";

				Typeface Din = Typeface.CreateFromAsset (Assets, "fonts/DINPro-Light.ttf");

				ISpannable sp = new SpannableString (titlestring + synonymsstring);
				sp.SetSpan (new CustomTypefaceSpan ("sans-serif", Verveine, 0), 0, titlestring.Length, SpanTypes.ExclusiveExclusive);
				sp.SetSpan (new CustomTypefaceSpan ("sans-serif", Din, TypefaceStyle.Italic, ConvertDPToPixels(17)), titlestring.Length, titlestring.Length + synonymsstring.Length, SpanTypes.ExclusiveExclusive);

				title_synonyms.TextFormatted = sp;
			} else {
				title_synonyms.Text = t.title;
				title_synonyms.SetTypeface (Verveine, 0);
			}
			body.Text = t.body;
		}
	}
}