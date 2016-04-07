using System;

using Foundation;
using UIKit;

namespace TotemAppIos
{
	public partial class IndentTableViewCell : BaseChecklistTableViewCell
	{
		public static NSString _key = new NSString ("IndentTableViewCell");
		public static UINib Nib;

		public override NSString Key { 
			get {
				return _key;
			}
		}

		static IndentTableViewCell ()
		{
			Nib = UINib.FromName ("IndentTableViewCell", NSBundle.MainBundle);
		}

		public static IndentTableViewCell Create () {
			return (IndentTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public IndentTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public override void setData(string s) {
			lblBulletPoint.Text = "\u25EF";
			lblIndent.Text = s;
		}
	}
}