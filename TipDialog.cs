using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Totem {
	public class TipDialog : DialogFragment	{
		CheckBox weergeven_checkbox;
		TextView tip;

		Database db;

		Context context;

		public TipDialog(Context context) {
			this.context = context;
		}

		public static TipDialog NewInstance(Context context) {
			var dialogFragment = new TipDialog(context);
			return dialogFragment;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState) {
			var builder = new AlertDialog.Builder (Activity);

			db = DatabaseHelper.GetInstance (context); 

			var inflater = Activity.LayoutInflater;
			var dialogView = inflater.Inflate (Resource.Layout.TipPopUp, null);

			if (dialogView != null) {
				weergeven_checkbox = dialogView.FindViewById<CheckBox> (Resource.Id.checkboxWeergeven);
				tip = dialogView.FindViewById<TextView> (Resource.Id.tip);
				tip.Text = db.GetRandomTip ();

				builder.SetView(dialogView);
				builder.SetPositiveButton("Volgende tip", HandlePositiveButtonClick);
				builder.SetNegativeButton("Ok", HandleNegativeButtonClick);
			}
				 
			var dialog = builder.Create();

			return dialog;
		}

		private void HandlePositiveButtonClick(object sender, DialogClickEventArgs e) {
			var dialog = TipDialog.NewInstance(context);
			dialog.Show(FragmentManager, "dialog");
		}

		private void HandleNegativeButtonClick(object sender, DialogClickEventArgs e) {
			var dialog = (AlertDialog) sender;
			if (weergeven_checkbox.Checked) {
				db.ChangePreference ("tips", "false");
			}
			dialog.Dismiss ();
		}
	}
}