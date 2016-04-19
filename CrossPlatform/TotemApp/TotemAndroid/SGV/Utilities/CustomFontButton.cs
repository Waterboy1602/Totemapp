using Android.Content;
using Android.Support.V7.Widget;
using Android.Util;

namespace TotemAndroid {

    //allows to put custom font in XML
    public class CustomFontButton : AppCompatButton {

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