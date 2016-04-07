using System;

using Foundation;
using UIKit;

namespace TotemAppIos
{
	public abstract partial class BaseChecklistTableViewCell : UITableViewCell
	{
		public static NSString _key;
		public static UINib Nib;

		public abstract NSString Key{ get; }

		static BaseChecklistTableViewCell ()
		{
			Nib = UINib.FromName ("BaseChecklistTableViewCell", NSBundle.MainBundle);
		}

		public BaseChecklistTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public abstract void setData(string s);
	}
}
