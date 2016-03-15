using System;
using Android.Views.InputMethods;
using Android.Content;
using Android.App;
using Android.Widget;

namespace Totem {
	public class MyOnCheckBoxClickListener : OnCheckBoxClickListener {
		
		//WeakReference to context to avoid memory leak
		private WeakReference<Context> mContext;

		public MyOnCheckBoxClickListener(Context context) {
			mContext = new WeakReference<Context>(context);
		}

		//hides keyboard when checkbox is clicked
		public void OnCheckboxClicked() {
			Context context = null;
			mContext.TryGetTarget(out context);
			KeyboardHelper.HideKeyboard (context);
		}
	}
}