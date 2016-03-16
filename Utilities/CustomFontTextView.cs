using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Content.Res;
using Android.Graphics;

namespace Totem {
	
	//allows to put custom font in XML
	public class CustomFontTextView : TextView {
		public static string ANDROID_SCHEMA = "http://schemas.android.com/apk/res/android";

		public CustomFontTextView (Context context): base(context) {
			CustomFontHelper.ApplyCustomFont (this, context, null);
		}

		public CustomFontTextView (Context context, IAttributeSet attrs): base(context, attrs) {
			CustomFontHelper.ApplyCustomFont (this, context, attrs);
		}

		public CustomFontTextView (Context context, IAttributeSet attrs, int defStyle): base(context, attrs, defStyle) {
			CustomFontHelper.ApplyCustomFont (this, context, attrs);
		}
	}
}