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

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			ProfielTotemsTableViewCell cell;

			cell = tableView.DequeueReusableCell (ProfielTotemsTableViewCell.Key) as ProfielTotemsTableViewCell;
			if (cell == null)
				cell = ProfielTotemsTableViewCell.Create ();

			cell.Totem = totems [indexPath.Row];
			cell.RippleColor = UIColor.FromRGBA (200, 200, 200, 50);
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
			//Console.WriteLine (totems[indexPath.Row].title);
			_appController.ProfileTotemSelected (_appController.CurrentProfiel.name, totems[indexPath.Row].nid);
		}
	}
}