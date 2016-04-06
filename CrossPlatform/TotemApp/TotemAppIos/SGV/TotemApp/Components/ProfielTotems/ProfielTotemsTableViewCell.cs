using System;

using Foundation;
using UIKit;
using MaterialControls;
using TotemAppCore;
using BemCheckBox;

namespace TotemAppIos {
	public partial class ProfielTotemsTableViewCell : UITableViewCell {
		public static readonly NSString Key = new NSString ("ProfielTotemsTableViewCell");
		public static readonly UINib Nib;
		public Totem Totem { get; set; }

		BemCheckBox.BemCheckBox _checkBox;

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
				_checkBox = new BemCheckBox.BemCheckBox (new CoreGraphics.CGRect (0, 0, 25, 25), new MyBemCheckBoxDelegate(this));
				vwCheckbox.Add (_checkBox);
			}
		}

		public void toggleCheckbox() {
			_checkBox.SetOn (!_checkBox.On,true);
			Totem.selected = _checkBox.On;
		}

		class MyBemCheckBoxDelegate : BemCheckBoxDelegate {
			ProfielTotemsTableViewCell cell;

			public MyBemCheckBoxDelegate(ProfielTotemsTableViewCell cell):base() {
				this.cell = cell;
			}

			public override void DidTapCheckBox(bool checkBoxIsOn) {
				cell.Totem.selected = checkBoxIsOn;
			}
		}
	}
}