using System;

using Foundation;
using UIKit;
using MaterialControls;
using TotemAppCore;

namespace TotemAppIos {
	public partial class ProfielTotemsTableViewCell : MDTableViewCell {
		public static readonly NSString Key = new NSString ("ProfielTotemsTableViewCell");
		public static readonly UINib Nib;
		public Totem Totem { get; set; }

		static ProfielTotemsTableViewCell () {
			Nib = UINib.FromName ("ProfielTotemsTableViewCell", NSBundle.MainBundle);
		}

		public ProfielTotemsTableViewCell (IntPtr handle) : base (handle) {
		}

		public static ProfielTotemsTableViewCell Create () {
			return (ProfielTotemsTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public void setData() {
			lblTotemName.Text = Totem.title;
		}
	}
}