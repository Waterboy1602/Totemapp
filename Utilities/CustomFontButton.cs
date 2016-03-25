using System;

using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Content.Res;
using Android.Graphics;

namespace Totem {

	//allows to put custom font in XML
	public class CustomFontButton : Button {
		public static string ANDROID_SCHEMA = "http://schemas.android.com/apk/res/android";

		public CustomFontButton (Context context): base(context) {
			CustomFontHelper.ApplyCustomFont (this, context, null);
		}

		public CustomFontButton (Context context, IAttributeSet attrs): base(context, attrs) {
			CustomFontHelper.ApplyCustomFont (this, context, attrs);
		}

		public CustomFontButton (Context context, IAttributeSet attrs, int defStyle): base(context, attrs, defStyle) {
			CustomFontHelper.ApplyCustomFont (this, context, attrs);
		}
	}
}