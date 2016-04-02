using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TotemAppCore;

namespace TotemAndroid {
	public class TipDialog : DialogFragment	{
		CheckBox weergeven_checkbox;
		TextView tip;

		Context context;

		AppController _appController = AppController.Instance;

		public TipDialog(Context context) {
			this.context = context;
		}

		public static TipDialog NewInstance(Context context) {
			var dialogFragment = new TipDialog(context);
			return dialogFragment;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState) {
			var builder = new AlertDialog.Builder (Activity); 

			var inflater = Activity.LayoutInflater;
			var dialogView = inflater.Inflate (Resource.Layout.TipPopUp, null);

			if (dialogView != null) {
				weergeven_checkbox = dialogView.FindViewById<CheckBox> (Resource.Id.checkboxWeergeven);
				tip = dialogView.FindViewById<TextView> (Resource.Id.tip);
				tip.Text = _appController.GetRandomTip ();

				builder.SetView(dialogView);
				builder.SetPositiveButton("Volgende tip", HandlePositiveButtonClick);
				builder.SetNegativeButton("Ok", HandleNegativeButtonClick);
			}
				 
			var dialog = builder.Create();

			return dialog;
		}

		void HandlePositiveButtonClick(object sender, DialogClickEventArgs e) {
			//var dialog = TipDialog.NewInstance(context);
			//dialog.Show(FragmentManager, "a");
		}

		void HandleNegativeButtonClick(object sender, DialogClickEventArgs e) {
			var dialog = (AlertDialog) sender;
			if (weergeven_checkbox.Checked)
				_appController.ChangePreference ("tips", "false");

			dialog.Dismiss ();
		}
	}
}