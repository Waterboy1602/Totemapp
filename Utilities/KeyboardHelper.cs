using System;

using Android.Content;
using Android.Views.InputMethods;
using Android.App;
using Android.Views;

namespace Totem {

	//static helper class for keyboard
	public static class KeyboardHelper	{

		//hides soft keyboard
		public static void HideKeyboard(Context context) {
			InputMethodManager inputManager = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			if(((Activity)context).CurrentFocus != null)
				inputManager.HideSoftInputFromWindow (((Activity)context).CurrentFocus.WindowToken, HideSoftInputFlags.None);
		}

		//shows soft keyboard of dialog
		public static void ShowKeyboard(Context context, View pView) {
			pView.RequestFocus();
			InputMethodManager inputMethodManager = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			inputMethodManager.ShowSoftInput(pView, ShowFlags.Forced);
			inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
		}
	}
}