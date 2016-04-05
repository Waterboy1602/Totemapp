using System;

using Foundation;
using UIKit;
using MaterialControls;
using TotemAppCore;
using BemCheckBox;

namespace TotemAppIos {
	public partial class ProfielenTableViewCell : MDTableViewCell {
		public static readonly NSString Key = new NSString ("ProfielenTableViewCell");
		public static readonly UINib Nib;
		public Profiel Profiel { get; set; }

		//BemCheckBox.BemCheckBox _checkBox;

		static ProfielenTableViewCell () {
			Nib = UINib.FromName ("ProfielenTableViewCell", NSBundle.MainBundle);
		}

		public ProfielenTableViewCell (IntPtr handle) : base (handle) {}

		public static ProfielenTableViewCell Create () {
			return (ProfielenTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public void setData() {
			lblProfile.Text = Profiel.name;
			//_checkBox = new BemCheckBox.BemCheckBox (new CoreGraphics.CGRect (0, 0, 25, 25), new MyBemCheckBoxDelegate(this));
			//vwCheckbox.Add (_checkBox);
		}
	}
}