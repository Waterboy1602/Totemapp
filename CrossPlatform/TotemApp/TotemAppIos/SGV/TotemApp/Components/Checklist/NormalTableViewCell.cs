using System;

using Foundation;
using UIKit;

namespace TotemAppIos
{
	public partial class NormalTableViewCell : BaseChecklistTableViewCell
	{
		public static NSString _key = new NSString ("NormalTableViewCell");
		public static UINib Nib;

		public override NSString Key { 
			get {
				return _key;
			}
		}

		static NormalTableViewCell ()
		{
			Nib = UINib.FromName ("NormalTableViewCell", NSBundle.MainBundle);
		}

		public static NormalTableViewCell Create () {
			return (NormalTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public NormalTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public override void setData(string s) {
			lblBulletPoint.Text = "\u25EF";
			lblNormal.Text = s;
		}
	}
}
