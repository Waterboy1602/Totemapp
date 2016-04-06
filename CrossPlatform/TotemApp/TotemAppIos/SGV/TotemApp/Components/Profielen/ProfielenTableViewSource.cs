using System;
using TotemAppCore;
using UIKit;
using System.Collections.Generic;
using Foundation;
using System.Runtime.InteropServices;

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

		public bool check { get; set; }

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			ProfielenTableViewCell cell;

			cell = tableView.DequeueReusableCell (ProfielenTableViewCell.Key) as ProfielenTableViewCell;
			if (cell == null)
				cell = ProfielenTableViewCell.Create ();

			cell.Profiel = profielen [indexPath.Row];

			//make sperator full width
			cell.PreservesSuperviewLayoutMargins = false;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			cell.setData (check);

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
			if(check)
				(tableView.CellAt (indexPath) as ProfielenTableViewCell).toggleCheckbox ();
			else
				_appController.ProfileSelected (profielen[indexPath.Row].name);

			tableView.DeselectRow (indexPath,true);
		}


	}
}