using System;

using Foundation;
using UIKit;

namespace TotemAppIos {

	//base class of all Checklist TableViewCells
	public abstract partial class BaseChecklistTableViewCell : UITableViewCell {
		public static NSString _key;
		public static UINib Nib;

		public abstract NSString Key{ get; }

		static BaseChecklistTableViewCell () {
			Nib = UINib.FromName ("BaseChecklistTableViewCell", NSBundle.MainBundle);
		}

		protected BaseChecklistTableViewCell (IntPtr handle) : base (handle) {}

		//sets TableViewCell data
		//overwritten in every child class
		public abstract void setData(string s, bool firstItem, bool lastItem);
	}
}
