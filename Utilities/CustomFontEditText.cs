using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Content.Res;
using Android.Graphics;

namespace Totem {
	public class CustomFontEditText : EditText {
		public static string ANDROID_SCHEMA = "http://schemas.android.com/apk/res/android";

		public CustomFontEditText (Context context): base(context) {
			ApplyCustomFont (context, null);
		}

		public CustomFontEditText (Context context, IAttributeSet attrs): base(context, attrs) {
			ApplyCustomFont (context, attrs);
		}

		public CustomFontEditText (Context context, IAttributeSet attrs, int defStyle): base(context, attrs, defStyle) {
			ApplyCustomFont (context, attrs);
		}

		private void ApplyCustomFont(Context context, IAttributeSet attrs) {
			TypedArray attributeArray = context.ObtainStyledAttributes (attrs, Resource.Styleable.CustomFontTextView);

			string fontName = attributeArray.GetString (Resource.Styleable.CustomFontTextView_font);
			int textStyle = attrs.GetAttributeIntValue (ANDROID_SCHEMA, "textStyle", 0);

			Typeface customFont = SelectTypeface (context, fontName);

			switch (textStyle) {
			case 1:
				SetTypeface (customFont, TypefaceStyle.Bold);
				break;
			case 2:
				SetTypeface (customFont, TypefaceStyle.Italic);
				break;
			case 3:
				SetTypeface (customFont, TypefaceStyle.BoldItalic);
				break;
			default:
				SetTypeface (customFont, TypefaceStyle.Normal);
				break;
			}

			attributeArray.Recycle ();
		}

		private Typeface SelectTypeface(Context context, String fontName) {
			if (fontName.Equals (context.GetString (Resource.String.Verveine))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/Verveine W01 Regular.ttf");
			} else if (fontName.Equals (context.GetString (Resource.String.DIN_light))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/DINPro-Light.ttf");
			} else if (fontName.Equals (context.GetString (Resource.String.DIN_regular))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/DINPro-Regular.ttf");
			} else {
				return null;
			}
		}
	}
}

