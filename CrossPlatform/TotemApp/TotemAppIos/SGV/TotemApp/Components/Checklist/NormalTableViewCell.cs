using System;

using Foundation;
using UIKit;

namespace TotemAppIos {

	//normal cell
	public partial class NormalTableViewCell : BaseChecklistTableViewCell {
		new public static NSString _key = new NSString ("NormalTableViewCell");
		new public static UINib Nib;

		bool fill;

		public override NSString Key { 
			get {
				return _key;
			}
		}

		static NormalTableViewCell () {
			Nib = UINib.FromName ("NormalTableViewCell", NSBundle.MainBundle);
		}

		public static NormalTableViewCell Create () {
			return (NormalTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public NormalTableViewCell (IntPtr handle) : base (handle) {}

		public override void setData(string s, bool firstItem, bool lastItem, bool fill) {
			this.fill = fill;
			
			paddingHeigthTop.Constant = firstItem ? 15 : 0;
			paddingHeight.Constant = lastItem ? 15 : 0;

			lblNormal.Text = s;
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