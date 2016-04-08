using System;

using UIKit;
using System.Collections.Generic;
using System.Xml;
using Foundation;

namespace TotemAppIos {
	public partial class ChecklistViewController : UIViewController {
		Dictionary<string, List<string>> dictData;

		public ChecklistViewController () : base ("ChecklistViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside+= btnReturnTouchUpInside;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside-= btnReturnTouchUpInside;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			tblChecklist.RowHeight = UITableView.AutomaticDimension;
			tblChecklist.EstimatedRowHeight = 30f;
			tblChecklist.ReloadData ();

		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()	{
			return UIStatusBarStyle.LightContent;
		}

		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		private void setData() {
			lblTitle.Text = "Totemisatie checklist";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			ExtrectDataFromXML ();
			lblHead.Font = UIFont.FromName("VerveineW01-Regular", 20f);
			lblFoot.Font = UIFont.FromName("VerveineW01-Regular", 20f);
			lblFoot.Text = "VEEL SUCCES!";
			lblHead.Text = "Een totemisatie vergt tijd en inspanning.\nDeze checklist leidt je doorheen de totemmap en helpt om niets te vergeten. Sta even stil bij jullie totemisatie en check of dit overeenstemt met onze lijst.\n ";
			tblChecklist.Source = new ChecklistTableViewSource (dictData);

		}

		void ExtrectDataFromXML() {
			dictData = new Dictionary<string, List<string>> ();

			XmlDocument doc = new XmlDocument ();
			doc.Load(NSBundle.MainBundle.PathForResource ("SharedAssets/checklist","xml"));
			var childNodes = doc.FirstChild.ChildNodes;
			foreach (var item in childNodes) {
				var itemName = (item as XmlElement).GetAttribute ("name");
				dictData.Add (itemName,new List<string>());
				var children = (item as XmlElement).ChildNodes;
				foreach (var child in children) {
					dictData [itemName].Add ((child as XmlElement).InnerText);
				}
			}
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}
	}
}