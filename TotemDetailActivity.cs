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
using Android.Text;


namespace Totem {
	[Activity (Label = "Totem detail")]			
	public class TotemDetailActivity : Activity	{
		CustomFontTextView number;
		TextView title_synonyms;
		CustomFontTextView body;

		Database db;
		Toast mToast;

		//CustomFontTextView title;
		ImageButton back;
		ImageButton search;

		String nid;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.TotemDetail);

			ActionBar mActionBar = ActionBar;
			mActionBar.SetDisplayShowTitleEnabled(false);
			mActionBar.SetDisplayShowHomeEnabled(false);

			LayoutInflater mInflater = LayoutInflater.From (this);
			View mCustomView = mInflater.Inflate (Resource.Layout.ActionBar, null);

			db = DatabaseHelper.GetInstance (this);

			Typeface Din = Typeface.CreateFromAsset(Assets,"fonts/DINPro-Light.ttf");

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			//Button voegtoe = FindViewById<Button> (Resource.Id.voegtoe);
			//voegtoe.SetTypeface(Din, 0);

			number = FindViewById<CustomFontTextView> (Resource.Id.number);
			title_synonyms = FindViewById<TextView> (Resource.Id.title_synonyms);
			body = FindViewById<CustomFontTextView> (Resource.Id.body);

			//add to profiles
			//voegtoe.Click += (sender, eventArgs) => ProfilePopup();

			back = mCustomView.FindViewById<ImageButton> (Resource.Id.backButton);
			back.Click += (object sender, EventArgs e) => OnBackPressed();

			search = mCustomView.FindViewById<ImageButton> (Resource.Id.searchButton);
			search.SetImageResource (Resource.Drawable.ic_add_white_48dp);
			search.Click += (object sender, EventArgs e) => ProfilePopup();

			nid = Intent.GetStringExtra ("totemID");
			var hideButton = Intent.GetStringExtra ("hideButton");
			if (hideButton != null)
				search.Visibility = ViewStates.Gone;

			GetInfo (nid);

			ActionBar.LayoutParams layout = new ActionBar.LayoutParams (WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);

			mActionBar.SetCustomView (mCustomView, layout);
			mActionBar.SetDisplayShowCustomEnabled (true);
		}

		private void ProfilePopup() {
				PopupMenu menu = new PopupMenu (this, search);
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
							if(value.Replace("'", "").Replace(" ", "").Equals("")) {
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

						AlertDialog d1 = alert.Create();

						//add profile when enter is clicked
						input.EditorAction += (s2, e) => {
							if (e.ActionId == ImeAction.Done) 
							{
								d1.GetButton(-1).PerformClick();
							}
							else
							{
								e.Handled = false;
							}
						};

						RunOnUiThread (() => {
							d1.Show();
						} );

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
			Totem t = db.GetTotemOnID (idx);
			number.Text = t.number + ". ";
			Typeface Verveine = Typeface.CreateFromAsset (Assets, "fonts/Verveine W01 Regular.ttf");

			//code to get formatting right
			//title and synonyms are in the same textview
			//font, size,... are given using spans
			if (t.synonyms != null) {
				string titlestring = t.title;
				string synonymsstring = " - " + t.synonyms;

				Typeface Din = Typeface.CreateFromAsset (Assets, "fonts/DINPro-Light.ttf");

				ISpannable sp = new SpannableString (titlestring + synonymsstring);

				sp.SetSpan (new CustomTypefaceSpan ("sans-serif", Verveine, 0, 0), 0, titlestring.Length, SpanTypes.ExclusiveExclusive);

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