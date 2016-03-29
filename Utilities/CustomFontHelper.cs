using System;

using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Android.Util;

namespace Totem {
	
	//static helper class for custom fonts on views
	public static class CustomFontHelper {
		public static string ANDROID_SCHEMA = "http://schemas.android.com/apk/res/android";

		//applies font from XML
		public static void ApplyCustomFont(TextView view, Context context, IAttributeSet attrs) {
			TypedArray attributeArray = context.ObtainStyledAttributes (attrs, Resource.Styleable.CustomFont);

			string fontName = attributeArray.GetString (Resource.Styleable.CustomFont_font);
			int textStyle = attrs.GetAttributeIntValue (ANDROID_SCHEMA, "textStyle", 0);

			Typeface customFont = SelectTypeface (context, fontName);

			switch (textStyle) {
			case 1:
				view.SetTypeface (customFont, TypefaceStyle.Bold);
				break;
			case 2:
				view.SetTypeface (customFont, TypefaceStyle.Italic);
				break;
			case 3:
				view.SetTypeface (customFont, TypefaceStyle.BoldItalic);
				break;
			default:
				view.SetTypeface (customFont, TypefaceStyle.Normal);
				break;
			}

			attributeArray.Recycle ();
		}

		//returns correct Typeface from fontName
		public static Typeface SelectTypeface(Context context, String fontName) {
			if (fontName.Equals (context.GetString (Resource.String.Verveine))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/Verveine W01 Regular.ttf");
			} else if (fontName.Equals (context.GetString (Resource.String.DIN_light))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/DINPro-Light.ttf");
			} else if (fontName.Equals (context.GetString (Resource.String.DIN_regular))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/DINPro-Regular.ttf");
			} else if (fontName.Equals (context.GetString (Resource.String.Sketchblock))) {
				return Typeface.CreateFromAsset (context.Assets, "fonts/sketch_block_light.ttf");
			} else {
				return null;
			}
		}
	}
}