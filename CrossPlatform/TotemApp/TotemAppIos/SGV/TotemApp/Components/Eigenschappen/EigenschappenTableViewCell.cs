using System;

using Foundation;
using UIKit;
using MaterialControls;
using TotemAppCore;
using BemCheckBox;

namespace TotemAppIos {
	public partial class EigenschappenTableViewCell : UITableViewCell {
		public static readonly NSString Key = new NSString ("EigenschappenTableViewCell");
		public static readonly UINib Nib;
		public Eigenschap Eigenschap { get; set; }

		AppController _appController = AppController.Instance;

		static EigenschappenTableViewCell () {
			Nib = UINib.FromName ("EigenschappenTableViewCell", NSBundle.MainBundle);
		}

		BemCheckBox.BemCheckBox _checkBox;

		public static EigenschappenTableViewCell Create () {
			return (EigenschappenTableViewCell)Nib.Instantiate (null, null) [0];

		}

		public EigenschappenTableViewCell (IntPtr handle) : base (handle) {}

		public void setData() {
			lblEigenschapName.Text = Eigenschap.name;
			_checkBox = new BemCheckBox.BemCheckBox (new CoreGraphics.CGRect (0, 0, 25, 25), new MyBemCheckBoxDelegate(this));
			vwCheckBoxHolder.Add (_checkBox);
			_checkBox.SetOn (Eigenschap.selected,false);
		}

		public void toggleCheckbox() {
			_checkBox.SetOn (!_checkBox.On,true);
			Eigenschap.selected = _checkBox.On;
			_appController.FireUpdateEvent ();
		}

		class MyBemCheckBoxDelegate : BemCheckBoxDelegate {
			EigenschappenTableViewCell cell;
			AppController _appController = AppController.Instance;

			public MyBemCheckBoxDelegate(EigenschappenTableViewCell cell):base() {
				this.cell = cell;
			}

			public override void DidTapCheckBox(bool checkBoxIsOn) {
				cell.Eigenschap.selected = checkBoxIsOn;
				_appController.FireUpdateEvent ();
			}
		}
	}
}