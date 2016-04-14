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

		public override void setData(string s, bool firstItem, bool lastItem) {
			//bullet point char in UNICODE

			if (firstItem) {
				lblBulletPoint.Text = "\n" + "\u25EF";
				lblIndent.Text = "\n" + s;
			} else {
				lblBulletPoint.Text = "\u25EF";
				lblIndent.Text = s;
			}
			if(lastItem) lblIndent.Text += "\n";
			cellHeight = lblIndent.Frame.Height;
		}
	}
}