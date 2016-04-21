using System;

using Foundation;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	public partial class ProfielTotemsTableViewCell : UITableViewCell {
		public static readonly NSString Key = new NSString ("ProfielTotemsTableViewCell");
		public static readonly UINib Nib;
		public Totem Totem { get; set; }

		BemCheckBox _checkBox;

		static ProfielTotemsTableViewCell () {
			Nib = UINib.FromName ("ProfielTotemsTableViewCell", NSBundle.MainBundle);
		}

		public ProfielTotemsTableViewCell (IntPtr handle) : base (handle) {
		}

		public static ProfielTotemsTableViewCell Create () {
			return (ProfielTotemsTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public void setData(bool check) {
			lblTotemName.Text = Totem.title;

			if (check) {
				_checkBox = new BemCheckBox (new CoreGraphics.CGRect (0, 0, 20, 20), new MyBemCheckBoxDelegate(this));
				vwCheckbox.Add (_checkBox);
			}
		}

		public void toggleCheckbox() {
			_checkBox.SetOn (!_checkBox.On,true);
			Totem.selected = _checkBox.On;
		}

		class MyBemCheckBoxDelegate : BemCheckBoxDelegate {
			ProfielTotemsTableViewCell cell;

			public MyBemCheckBoxDelegate(ProfielTotemsTableViewCell cell) {
				this.cell = cell;
			}

			public override void DidTapCheckBox(bool checkBoxIsOn) {
				cell.Totem.selected = checkBoxIsOn;
			}
		}
	}
}