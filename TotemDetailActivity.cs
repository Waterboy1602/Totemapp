
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

		static string dbName = "totems.sqlite";
		string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.TotemDetail);

			db = new Database (this);

			title = FindViewById<TextView> (Resource.Id.title);
			Typeface face = Typeface.CreateFromAsset(Assets,"fonts/DCC - Ash.otf");
			title.SetTypeface (face, 0);

			synonyms = FindViewById<TextView> (Resource.Id.synonyms);
			body = FindViewById<TextView> (Resource.Id.body);

			var nid = Intent.GetStringExtra ("totemID");
			getInfo (nid);
		}

		private void getInfo(string idx) {
			Totem t = db.getTotemOnID (idx);
			title.Text = t.title;
			if(t.synonyms != null) {
				synonyms.Text = t.synonyms;
			}
			body.Text = t.body;
		}
	}
}

