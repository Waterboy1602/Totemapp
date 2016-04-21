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
		public List<List<bool>> checkedStates;

		public ChecklistTableViewSource (Dictionary<string, List<string>> xmlDict, List<List<bool>> states) {
			this.xmlDict = xmlDict;

			if (states != null) {
				checkedStates = states;
			} else {
				checkedStates = new List<List<bool>>();
				var count = 0;
				foreach (string s in xmlDict.Keys) {
					checkedStates.Add(new List<bool>());
					foreach (string st in xmlDict[s]) {
						checkedStates[count].Add(false);
					}
					count++;
				}
			}
		}

		public void UpdateStates(List<List<bool>> states) {
			checkedStates = states;
		}

		//builds layout for section headers
		public override UIView GetViewForHeader (UITableView tableView, nint section) {
			return BuildSectionHeaderView(sectionTitles (tableView) [section]);
		}

		//defines layout for section headers
		public static UIView BuildSectionHeaderView(string caption) {
			var  view = new UIView(new RectangleF(0,0,1000,40));
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
		//e.g.: i_het past in de context
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
			BaseChecklistTableViewCell cell;

			//extract type of cell and content
			bool firstItem = (indexPath.Row == 0);
			bool lastItem = (indexPath.Row == (RowsInSection (tableView, indexPath.Section) - 1));
			var item = xmlDict [sectionTitles (tableView)[indexPath.Section]][indexPath.Row];
			string[] data = item.Split ('_');
			var type = data [0];
			var content = data [1];

			cell = tableView.DequeueReusableCell (new NSString ("IndentTableViewCell")) as BaseChecklistTableViewCell;

			//indented cell
			if (type.Equals ("i") && (cell == null || !cell.Key.Equals ("IndentTableViewCell"))) {
				cell = IndentTableViewCell.Create ();
			//normal cell
			} else if (type.Equals ("n") && (cell == null || !cell.Key.Equals ("NormalTableViewCell"))) {
				cell = NormalTableViewCell.Create ();
			//header cell
			} else if (type.Equals ("h") && (cell == null || !cell.Key.Equals ("HeadTableViewCell"))) {
				cell = HeadTableViewCell.Create ();
			}

			cell.setData (content, firstItem, lastItem, checkedStates[indexPath.Section][indexPath.Row]);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section) {
			return section >= 0 ? xmlDict [sectionTitles (tableview) [section]].Count () : 0;
		}

		public override nint NumberOfSections (UITableView tableView) {
			return xmlDict.Keys.ToArray ().Length;
		}

		string[] sectionTitles (UITableView tableView) {
			return xmlDict.Keys.ToArray ();
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			(tableView.CellAt (indexPath) as BaseChecklistTableViewCell).toggle ();
			checkedStates[indexPath.Section][indexPath.Row] = !(checkedStates[indexPath.Section][indexPath.Row]);
		}
	}
}