using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;

namespace Totem {

	//static helper class for keyboard
	public static class KeyboardHelper	{
		
		public static void HideKeyboard(Context context) {
			var inputManager = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			if(((Activity)context).CurrentFocus != null)
				inputManager.HideSoftInputFromWindow (((Activity)context).CurrentFocus.WindowToken, HideSoftInputFlags.None);
		}
			
		public static void ShowKeyboard(Context context, View pView) {
			pView.RequestFocus();
			InputMethodManager inputMethodManager = (InputMethodManager)context.GetSystemService (Context.InputMethodService);
			inputMethodManager.ShowSoftInput(pView, ShowFlags.Forced);
			inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
		}
	}
}