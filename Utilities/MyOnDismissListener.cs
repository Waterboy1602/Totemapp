using System;

using Android.Content;
using Android.Runtime;

namespace Totem {
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
			KeyboardHelper.HideKeyboardDialog (context);
		}
	}
}