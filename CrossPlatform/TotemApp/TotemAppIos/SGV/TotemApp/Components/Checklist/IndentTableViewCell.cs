using System;

using Foundation;
using UIKit;

namespace TotemAppIos {

	//indented cell
	public partial class IndentTableViewCell : BaseChecklistTableViewCell {
		new public static NSString _key = new NSString ("IndentTableViewCell");
		new public static UINib Nib;

		public nfloat cellHeight { get; set; }

		public override NSString Key { 
			get {
				return _key;
			}
		}

		static IndentTableViewCell () {
			Nib = UINib.FromName ("IndentTableViewCell", NSBundle.MainBundle);
		}

		public static IndentTableViewCell Create () {
			return (IndentTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public IndentTableViewCell (IntPtr handle) : base (handle) {}

		public override void setData(string s) {
			//bullet point char in UNICODE
			lblBulletPoint.Text = "\u25EF";
			lblIndent.Text = s;
			cellHeight = lblIndent.Frame.Height;
		}
	}
}