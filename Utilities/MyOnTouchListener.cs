using System;

using Android.Views;
using Android.Views.InputMethods;
using Android.Content;
using Android.Widget;

namespace Totem {
	public class MyOnTouchListener : Java.Lang.Object, View.IOnTouchListener {
		private WeakReference<Context> mContext;
		private EditText edittext;

		public MyOnTouchListener (Context context, EditText edittext) {
			mContext = new WeakReference<Context>(context);
			this.edittext = edittext;
		}

		public Boolean OnTouch(View v, MotionEvent e) {
			Context context = null;
			mContext.TryGetTarget(out context);
			InputMethodManager imm = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			imm.HideSoftInputFromWindow (edittext.WindowToken, 0);
			return false;
		}
	}
}