using Foundation;

using System;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	public partial class TotemsTableViewCell : UITableViewCell {
		public static readonly NSString Key = new NSString ("TotemsTableViewCell");
		public static readonly UINib Nib;
		public Totem Totem { get; set; }
		public int Freq { get; set; }

		static TotemsTableViewCell () {
			Nib = UINib.FromName ("TotemsTableViewCell", NSBundle.MainBundle);
		}

		public static TotemsTableViewCell Create () {
			return (TotemsTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public TotemsTableViewCell (IntPtr handle) : base (handle) {}

		public void setData() {
			lblTotemName.Text = Totem.title;
			if (Freq != 0)
				lblFreq.Text = Freq.ToString ();
		}
	}
}