using System;
using UIKit;
using System.Collections.Generic;
using TotemAppCore;
using Foundation;

namespace TotemAppIos {
	public class TotemsTableViewSource : UITableViewSource {
		public TotemsTableViewSource (List<Totem> totems) {
			this.totems = totems;
		}

		AppController _appController = AppController.Instance;

		List<Totem> totems;
		public List<Totem> Totems {
			get {
				return this.totems;
			}
			set {
				totems = value;
			}
		}

		#region implemented abstract members of UITableViewSource

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			TotemsTableViewCell cell;

				cell = tableView.DequeueReusableCell (TotemsTableViewCell.Key) as TotemsTableViewCell;
				if (cell == null)
					cell = TotemsTableViewCell.Create ();

			cell.Totem = totems [indexPath.Row];
			cell.RippleColor = UIColor.FromRGBA (200, 200, 200, 50);

			//make sperator full width
			cell.PreservesSuperviewLayoutMargins = false;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			cell.setData ();

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return totems.Count;
		}

		public override nint NumberOfSections (UITableView tableView) {
			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
			return 50;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			_appController.TotemSelected (totems[indexPath.Row].nid);
		}
		#endregion
	}
}