using System;

using Android.Views;
using Android.Views.InputMethods;
using Android.Content;
using Android.Widget;

namespace Totem {
	public class MyOnTouchListener : Java.Lang.Object, View.IOnTouchListener {

		//WeakReference to context to avoid memory leak
		private WeakReference<Context> mContext;
		private EditText edittext;

		public MyOnTouchListener (Context context, EditText edittext) {
			mContext = new WeakReference<Context>(context);
			this.edittext = edittext;
		}

		//hides keyboard when ListView scrolled
		public bool OnTouch(View v, MotionEvent e) {
			Context context = null;
			mContext.TryGetTarget(out context);
			InputMethodManager imm = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			imm.HideSoftInputFromWindow (edittext.WindowToken, 0);
			return false;
		}
	}
}