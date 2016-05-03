using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	public class TotemsResultTableViewSource : UITableViewSource {
		public TotemsResultTableViewSource (Dictionary<Totem, int> dict) {
			Dict = dict;
		}

		AppController _appController = AppController.Instance;

		Dictionary<Totem, int> Dict { get; set;	}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
			TotemsTableViewCell cell;

			cell = tableView.DequeueReusableCell (TotemsTableViewCell.Key) as TotemsTableViewCell;
			if (cell == null)
				cell = TotemsTableViewCell.Create ();

			List<Totem> totems = Dict.Keys.ToList();
			cell.Totem = totems [indexPath.Row];
			cell.Freq = Dict [totems [indexPath.Row]];

			//make sperator full width
			cell.PreservesSuperviewLayoutMargins = false;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			cell.setData ();

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return Dict.Keys.ToList().Count;
		}

		public override nint NumberOfSections (UITableView tableView) {
			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
			return 50;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			_appController.TotemSelected (Dict.Keys.ToList()[indexPath.Row].nid);
			tableView.DeselectRow (indexPath,true);
		}
	}
}