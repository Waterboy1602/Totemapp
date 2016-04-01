using Android.Graphics;
using Android.Text.Style;
using Android.Text;

namespace Totem {

	//helperclass for spannables with custom fonts, style and size
	public class CustomTypefaceSpan : TypefaceSpan {
		private Typeface newType;
		private TypefaceStyle style;
		private int size;

		public CustomTypefaceSpan (string family, Typeface type, TypefaceStyle style) : base(family) {
			newType = type;
			this.style = style;
			this.size = 0;
		}

		public CustomTypefaceSpan (string family, Typeface type, TypefaceStyle style, int size) : base(family) {
			newType = type;
			this.style = style;
			this.size = size;
		}

		public override void UpdateDrawState (TextPaint ds) {
			ApplyCustomTypeface (ds, newType, style, size);
		}

		public override void UpdateMeasureState (TextPaint paint) {
			ApplyCustomTypeface (paint, newType, style, size);
		}

		private static void ApplyCustomTypeface(Paint paint, Typeface tf, TypefaceStyle style, int size) {
			if (style == TypefaceStyle.Italic)
				paint.TextSkewX = -0.25f;
			
			if(size > 0)
				paint.TextSize = size;

			paint.SetTypeface (tf);
		}
	}
}