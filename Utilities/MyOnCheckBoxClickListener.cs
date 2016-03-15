using System;
using Android.Views.InputMethods;
using Android.Content;
using Android.App;
using Android.Widget;

namespace Totem
{
	public class MyOnCheckBoxClickListener : OnCheckBoxClickListener {

		private WeakReference<Context> mContext;

		public MyOnCheckBoxClickListener(Context context) {
			mContext = new WeakReference<Context>(context);
		}

		public void OnCheckboxClicked(){
			Context context = null;
			mContext.TryGetTarget(out context);
			InputMethodManager inputManager = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			inputManager.HideSoftInputFromWindow (((Activity)context).CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
		}
	}
}