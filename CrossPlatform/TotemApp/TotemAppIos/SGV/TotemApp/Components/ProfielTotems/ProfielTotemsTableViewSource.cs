using System;
using System.Collections.Generic;
using TotemAppCore;
using UIKit;
using Foundation;

namespace TotemAppIos {
	public class ProfielTotemsTableViewSource : UITableViewSource {
		public ProfielTotemsTableViewSource () {}

		public ProfielTotemsTableViewSource (List<Totem> totems) {
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

		public bool check { get; set; }

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			ProfielTotemsTableViewCell cell;

			cell = tableView.DequeueReusableCell (ProfielTotemsTableViewCell.Key) as ProfielTotemsTableViewCell;
			if (cell == null)
				cell = ProfielTotemsTableViewCell.Create ();

			cell.Totem = totems [indexPath.Row];

			//make sperator full width
			cell.PreservesSuperviewLayoutMargins = false;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			cell.setData (check);

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
			if(check)
				(tableView.CellAt (indexPath) as ProfielTotemsTableViewCell).toggleCheckbox ();
			else
				_appController.ProfileTotemSelected (_appController.CurrentProfiel.name, totems[indexPath.Row].nid);
			
			tableView.DeselectRow (indexPath,true);
		}
	}
}