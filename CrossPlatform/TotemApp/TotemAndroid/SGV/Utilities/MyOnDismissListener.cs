using Android.Content;

using System;

namespace TotemAndroid {
	public class MyOnDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener  {

		//WeakReference to context to avoid memory leak
		private WeakReference<Context> mContext;

		public MyOnDismissListener (Context context) {
			mContext = new WeakReference<Context>(context);
		}

		//hides keyboard when dialog is dismissed
		public void OnDismiss(IDialogInterface dialogInterface) {
			Context context = null;
			mContext.TryGetTarget(out context);
			KeyboardHelper.HideKeyboard (context);
		}
	}
}