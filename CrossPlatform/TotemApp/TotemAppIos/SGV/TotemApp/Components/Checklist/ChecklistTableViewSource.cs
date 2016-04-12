using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using CoreAnimation;
using Foundation;
using UIKit;

namespace TotemAppIos {
	public class ChecklistTableViewSource : UITableViewSource {
		Dictionary<string, List<string>> xmlDict;

		public ChecklistTableViewSource (Dictionary<string, List<string>> xmlDict) {
			this.xmlDict = xmlDict;
		}

		//builds layout for section headers
		public override UIView GetViewForHeader (UITableView tableView, nint section) {
			return BuildSectionHeaderView(sectionTitles (tableView) [section]);
		}

		//defines layout for section headers
		public static UIView BuildSectionHeaderView(string caption) {
			var  view = new UIView(new RectangleF(0,0,1000,20));
			view.BackgroundColor = UIColor.White;

			var topBorder = new CALayer ();
			topBorder.Frame = new RectangleF(0, 0, (float)view.Frame.Size.Width, 3);
			topBorder.BackgroundColor = UIColor.FromRGB (0, 92, 157).CGColor;
			view.Layer.AddSublayer (topBorder);

			var bottomBorder = new CALayer ();
			bottomBorder.Frame = new RectangleF(0, 37, (float)view.Frame.Size.Width, 3);
			bottomBorder.BackgroundColor = UIColor.FromRGB (0, 92, 157).CGColor;
			view.Layer.AddSublayer (bottomBorder);

			var label = new UILabel();
			label.Opaque = true;
			label.TextColor = UIColor.FromRGB (0, 92, 157);
			label.Font = UIFont.FromName("SketchBlock-Light", 22f);
			label.Frame = new RectangleF(20,12,315,20);
			label.Text = caption;
			view.AddSubview(label);
			return view;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section) {
			return 40f;
		}

		//returns matching view for the type of data
		//i -> indented
		//h -> head
		//n -> normal
		//t -> title
		//e.g.: i_het past in de context
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
			BaseChecklistTableViewCell cell;

			//extract type of cell and content
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
	}
}