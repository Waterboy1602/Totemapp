using System;
using UIKit;
using System.Collections.Generic;

namespace TotemAppIos {
	public class ChecklistTableViewSource : UITableViewSource {
		Dictionary<string, List<string>> dict;

		public ChecklistTableViewSource (Dictionary<string, List<string>> dict) {
			this.dict = dict;
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section) {
			throw new System.NotImplementedException ();
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section) {
			throw new System.NotImplementedException ();
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section) {
			throw new System.NotImplementedException ();
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			throw new System.NotImplementedException ();
		}

		#region implemented abstract members of UITableViewSource

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			throw new NotImplementedException ();
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}