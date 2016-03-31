using System;

using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Content.Res;
using Android.Graphics;

namespace Totem {

	//allows to put custom font in XML
	public class CustomFontEditText : EditText {

		public CustomFontEditText (Context context): base(context) {
			CustomFontHelper.ApplyCustomFont (this, context, null);
		}

		public CustomFontEditText (Context context, IAttributeSet attrs): base(context, attrs) {
			CustomFontHelper.ApplyCustomFont (this, context, attrs);
		}

		public CustomFontEditText (Context context, IAttributeSet attrs, int defStyle): base(context, attrs, defStyle) {
			CustomFontHelper.ApplyCustomFont (this, context, attrs);
		}
	}
}