using Foundation;

using System;
using System.Collections.Generic;
using System.Linq;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	public class TotemsTableViewSource : UITableViewSource {

		Dictionary<string, List<Totem>> dict;
		string[] keys;

		public TotemsTableViewSource (List<Totem> totems) {
			this.totems = totems;
			FillDict (totems);
		}

		AppController _appController = AppController.Instance;

		List<Totem> totems;
		public List<Totem> Totems {
			get {
				return totems;
			}
			set {
				totems = value;
				FillDict (totems);
			}
		}

		//fill dictionary on letter
		void FillDict(List<Totem> list) {
			dict = new Dictionary<string, List<Totem>> ();
			foreach (var t in list) {
				if (dict.ContainsKey (t.Title[0].ToString ())) {
					dict[t.Title[0].ToString ()].Add(t);
				} else {
					dict.Add (t.Title[0].ToString (), new List<Totem> {t});
				}
			}
			keys = dict.Keys.ToArray ();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
			TotemsTableViewCell cell;

			cell = tableView.DequeueReusableCell (TotemsTableViewCell.Key) as TotemsTableViewCell;
			if (cell == null)
				cell = TotemsTableViewCell.Create ();

			cell.Totem = dict[keys[indexPath.Section]][indexPath.Row];

			//make sperator full width
			cell.PreservesSuperviewLayoutMargins = false;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			cell.setData ();

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return dict[keys[section]].Count;
		}

		public override nint NumberOfSections (UITableView tableView) {
			return keys.Length;
		}

		public override string[] SectionIndexTitles (UITableView tableView) {
			return keys;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
			return 50;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			_appController.TotemSelected (dict[keys[indexPath.Section]][indexPath.Row].Nid);
			tableView.DeselectRow (indexPath,true);
		}
	}
}