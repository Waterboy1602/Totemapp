using System;
using UIKit;
using System.Collections.Generic;
using TotemAppCore;
using Foundation;
using System.Linq;

namespace TotemAppIos {
	public class TotemsResultTableViewSource : UITableViewSource {
		public TotemsResultTableViewSource (Dictionary<Totem, int> dict) {
			this.dict = dict;
		}

		AppController _appController = AppController.Instance;

		Dictionary<Totem, int> dict;
		Dictionary<Totem, int> Dict {
			get {
				return this.dict;
			}
			set {
				dict = value;
			}
		}

		#region implemented abstract members of UITableViewSource

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			TotemsTableViewCell cell;

			cell = tableView.DequeueReusableCell (TotemsTableViewCell.Key) as TotemsTableViewCell;
			if (cell == null)
				cell = TotemsTableViewCell.Create ();

			List<Totem> totems = Dict.Keys.ToList();
			cell.Totem = totems [indexPath.Row];
			cell.Freq = Dict [totems [indexPath.Row]];
			cell.RippleColor = UIColor.FromRGBA (200, 200, 200, 50);

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
		}
		#endregion
	}
}