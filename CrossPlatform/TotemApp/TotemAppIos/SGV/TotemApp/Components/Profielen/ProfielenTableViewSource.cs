using System;
using TotemAppCore;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace TotemAppIos {
	public class ProfielenTableViewSource : UITableViewSource {

		AppController _appController = AppController.Instance;

		public ProfielenTableViewSource (List<Profiel> profielen) {
			this.profielen = profielen;
		}

		List<Profiel> profielen;
		public List<Profiel> Profielen {
			get {
				return this.profielen;
			}
			set {
				profielen = value;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			ProfielenTableViewCell cell;

			cell = tableView.DequeueReusableCell (ProfielenTableViewCell.Key) as ProfielenTableViewCell;
			if (cell == null)
				cell = ProfielenTableViewCell.Create ();

			cell.Profiel = profielen [indexPath.Row];
			cell.RippleColor = UIColor.FromRGBA (200, 200, 200, 50);
			cell.setData ();

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return profielen.Count;
		}

		public override nint NumberOfSections (UITableView tableView) {
			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
			return 50;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			_appController.ProfileSelected (profielen[indexPath.Row].name);
		}
	}
}