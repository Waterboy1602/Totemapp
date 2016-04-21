using System;
using System.Collections.Generic;

using Foundation;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	public class EigenschappenTableViewSource : UITableViewSource {
		
		public EigenschappenTableViewSource (List<Eigenschap> eigenschappen) {
			Eigenschappen = eigenschappen;
		}

		public List<Eigenschap> Eigenschappen { get; set; }

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
			EigenschappenTableViewCell cell;

			cell = tableView.DequeueReusableCell (EigenschappenTableViewCell.Key) as EigenschappenTableViewCell;
			if (cell == null)
				cell = EigenschappenTableViewCell.Create ();

			cell.Eigenschap = Eigenschappen [indexPath.Row];

			//make sperator full width
			cell.PreservesSuperviewLayoutMargins = false;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			cell.setData ();


			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return Eigenschappen.Count;
		}

		public override nint NumberOfSections (UITableView tableView) {
			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
			return 50;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			(tableView.CellAt (indexPath) as EigenschappenTableViewCell).toggleCheckbox ();
			tableView.DeselectRow (indexPath,true);
		}
	}
}