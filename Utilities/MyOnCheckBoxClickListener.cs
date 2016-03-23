using System;
using Android.Views.InputMethods;
using Android.Content;
using Android.App;
using Android.Widget;
using System.Collections.Generic;

namespace Totem {
	public class MyOnCheckBoxClickListener {
		
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

		public void UpdateCounter(List<Eigenschap> eigenschapList)  {
			Context context = null;
			mContext.TryGetTarget(out context);
			TextView count = ((Activity)context).FindViewById<TextView> (Resource.Id.test);
			int counter = CountCheckedItems(eigenschapList);
			count.Text = counter + " selected";
		}

		private int CountCheckedItems(List<Eigenschap> eigenschapList) {
			int result = 0;
			foreach (Eigenschap e in eigenschapList) {
				if (e.selected)
					result++;
			}
			return result;
		}
	}
}