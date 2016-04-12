using System;

using Foundation;
using UIKit;

namespace TotemAppIos {

	//title cell
	public partial class TitleTableViewCell : BaseChecklistTableViewCell {
		new public static readonly NSString _key = new NSString ("TitleTableViewCell");
		new public static readonly UINib Nib;

		public override NSString Key { 
			get {
				return _key;
			}
		}

		static TitleTableViewCell () {
			Nib = UINib.FromName ("TitleTableViewCell", NSBundle.MainBundle);
		}

		public static TitleTableViewCell Create () {
			return (TitleTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public TitleTableViewCell (IntPtr handle) : base (handle) {}

		public override void setData(string s) {
			lblTitle.Text = s;
		}
	}
}