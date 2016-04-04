using System;

using Foundation;
using UIKit;
using MaterialControls;
using TotemAppCore;

namespace TotemAppIos {
	public partial class TotemsTableViewCell : MDTableViewCell {
		public static readonly NSString Key = new NSString ("TotemsTableViewCell");
		public static readonly UINib Nib;
		public Totem Totem {
			get;
			set;
		}

		static TotemsTableViewCell () {
			Nib = UINib.FromName ("TotemsTableViewCell", NSBundle.MainBundle);
		}

		public static TotemsTableViewCell Create () {
			return (TotemsTableViewCell)Nib.Instantiate (null, null) [0];

		}

		public TotemsTableViewCell (IntPtr handle) : base (handle) {}

		public void setData() {
			lblTotemName.Text = Totem.title;
		}
	}
}