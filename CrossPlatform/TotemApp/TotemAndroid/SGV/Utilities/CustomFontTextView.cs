using Android.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;

namespace TotemAndroid {
	
	//allows to put custom font in XML
	public class CustomFontTextView : AppCompatTextView {

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