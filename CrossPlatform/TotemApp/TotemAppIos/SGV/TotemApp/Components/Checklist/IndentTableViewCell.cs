using System;

using Foundation;
using UIKit;

namespace TotemAppIos {

	//indented cell
	public partial class IndentTableViewCell : BaseChecklistTableViewCell {
		new public static NSString _key = new NSString ("IndentTableViewCell");
		new public static UINib Nib;

		bool fill;

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

		public override void setData(string s, bool firstItem, bool lastItem, bool fill) {
			this.fill = fill;

			paddingHeightTop.Constant = firstItem ? 15 : 0;
			paddingHeigthBottom.Constant = lastItem ? 15 : 0;

			lblIndent.Text = s;
			if (fill)
				imgBullet.Image = UIImage.FromBundle ("SharedAssets/rsz_black_circle");
			else
				imgBullet.Image = UIImage.FromBundle ("SharedAssets/rsz_white_circle");
			
		}

		public override void toggle() {
			if (fill) {
				fill = false;
				imgBullet.Image = UIImage.FromBundle ("SharedAssets/rsz_white_circle");
			} else {
				fill = true;
				imgBullet.Image = UIImage.FromBundle ("SharedAssets/rsz_black_circle");
			}
		}
	}
}