using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using System.Drawing;
using System.Linq;
using CoreAnimation;

namespace TotemAppIos {
	public class ChecklistTableViewSource : UITableViewSource {
		Dictionary<string, List<string>> xmlDict;

		public ChecklistTableViewSource (Dictionary<string, List<string>> xmlDict) {
			this.xmlDict = xmlDict;
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section) {
			return BuildSectionHeaderView(sectionTitles (tableView) [section]);
		}

		public static UIView BuildSectionHeaderView(string caption) {
			UIView view = new UIView(new System.Drawing.RectangleF(0,0,1000,20));
			view.BackgroundColor = UIColor.White;

			CALayer topBorder = new CALayer ();
			topBorder.Frame = new RectangleF(0, 0, (float)view.Frame.Size.Width, 3);
			topBorder.BackgroundColor = UIColor.FromRGB (0, 92, 157).CGColor;
			view.Layer.AddSublayer (topBorder);

			CALayer bottomBorder = new CALayer ();
			bottomBorder.Frame = new RectangleF(0, 37, (float)view.Frame.Size.Width, 3);
			bottomBorder.BackgroundColor = UIColor.FromRGB (0, 92, 157).CGColor;
			view.Layer.AddSublayer (bottomBorder);

			UILabel label = new UILabel();
			label.Opaque = true;
			label.TextColor = UIColor.FromRGB (0, 92, 157);
			label.Font = UIFont.FromName("SketchBlock-Light", 22f);
			label.Frame = new System.Drawing.RectangleF(20,12,315,20);
			label.Text = caption;
			view.AddSubview(label);
			return view;
		}


		public override nfloat GetHeightForHeader (UITableView tableView, nint section) {
			return 40f;
		}

		#region implemented abstract members of UITableViewSource

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath) {
			BaseChecklistTableViewCell cell = null;

			//var item = xmlData [indexPath.Row];
			var item = xmlDict [sectionTitles (tableView)[indexPath.Section]][indexPath.Row];
			string[] data = item.Split ('_');
			var type = data [0];
			var content = data [1];

			cell = tableView.DequeueReusableCell (new NSString ("NormalTableViewCell")) as BaseChecklistTableViewCell;

			//indented cell
			if (type.Equals ("i") && (cell == null || !cell.Key.Equals ("IndentTableViewCell"))) {
				cell = IndentTableViewCell.Create ();
			//normal cell
			} else if (type.Equals ("n") && (cell == null || !cell.Key.Equals ("NormalTableViewCell"))) {
				cell = NormalTableViewCell.Create ();
			//header cell
			} else if (type.Equals ("h") && (cell == null || !cell.Key.Equals ("HeadTableViewCell"))) {
				cell = HeadTableViewCell.Create ();
			//title cell
			} else if (cell == null || !cell.Key.Equals ("TitleTableViewCell")) {
				cell = TitleTableViewCell.Create ();
			}

			cell.setData (content);

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return xmlDict[sectionTitles (tableview)[section]].Count ();
		}

		public override nint NumberOfSections (UITableView tableView) {
			return xmlDict.Keys.ToArray ().Length;
		}

		string[] sectionTitles (UITableView tableView) {
			return xmlDict.Keys.ToArray ();
		}

		#endregion
	}
}