
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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			RequestWindowFeature(WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.TotemDetail);

			db = new Database (this);

			//single toast for entire activity
			mToast = Toast.MakeText (this, "", ToastLength.Short);

			Button voegtoe = FindViewById<Button> (Resource.Id.voegtoe);

			title = FindViewById<TextView> (Resource.Id.title);
			Typeface face = Typeface.CreateFromAsset(Assets,"fonts/DCC - Ash.otf");
			title.SetTypeface (face, 0);

			synonyms = FindViewById<TextView> (Resource.Id.synonyms);
			body = FindViewById<TextView> (Resource.Id.body);

			var nid = Intent.GetStringExtra ("totemID");
			GetInfo (nid);

			//add to profiles
			voegtoe.Click += (sender, eventArgs) => {
				if (db.GetProfielNamen().Count == 0) {
					mToast.SetText("Nog geen profielen toegevoegd");
					mToast.Show();
				} else {
					PopupMenu menu = new PopupMenu (this, voegtoe);
					menu.Inflate (Resource.Menu.Popup);
					int count = 0;
					foreach(Profiel p in db.GetProfielen()) {
						menu.Menu.Add(0,count,count,p.name);
						count++;
					}

					menu.MenuItemClick += (s1, arg1) => {
						db.AddTotemToProfiel(nid, arg1.Item.TitleFormatted.ToString());
						mToast.SetText(db.GetTotemOnID(nid).title + " toegevoegd aan profiel " + arg1.Item.TitleFormatted.ToString());
						mToast.Show();
					};

					menu.Show ();
				}
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
	}
}