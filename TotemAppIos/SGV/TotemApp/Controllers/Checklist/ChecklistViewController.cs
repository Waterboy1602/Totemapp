using Foundation;

using ServiceStack.Text;

using System.Collections.Generic;
using System.Xml;

using UIKit;

namespace TotemAppIos {
	public partial class ChecklistViewController : BaseViewController {
		Dictionary<string, List<string>> dictData;

		NSUserDefaults userDefs;
		List<List<bool>> states;
		ChecklistTableViewSource tvSource;

		public ChecklistViewController () : base ("ChecklistViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			userDefs = NSUserDefaults.StandardUserDefaults;
		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside+= btnReturnTouchUpInside;

			var ser = userDefs.StringForKey ("states");
			if (ser != null) {
				states = JsonSerializer.DeserializeFromString <List<List<bool>>> (ser);
				tvSource.UpdateStates (states);
				tblChecklist.ReloadData ();
			}
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside-= btnReturnTouchUpInside;

			var ser = JsonSerializer.SerializeToString (tvSource.checkedStates);
			userDefs.SetString (ser, "states");
			userDefs.Synchronize ();
		}

		//enforces fitting row heights
		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			tblChecklist.RowHeight = UITableView.AutomaticDimension;
			tblChecklist.EstimatedRowHeight = 30f;
			tblChecklist.ReloadData ();
		}

		public override void setData() {
			lblTitle.Text = "Totemisatie checklist";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			ExtractDataFromXML ();

			lblHead.Font = UIFont.FromName("VerveineW01-Regular", 20f);
			lblFoot.Font = UIFont.FromName("VerveineW01-Regular", 20f);
			lblFoot.Text = "VEEL SUCCES!";
			lblHead.Text = "Een totemisatie vergt tijd en inspanning.\nDeze checklist leidt je doorheen de totemmap en helpt om niets te vergeten. Sta even stil bij jullie totemisatie en check of dit overeenstemt met onze lijst.\n ";

			tvSource = new ChecklistTableViewSource (dictData, states);
			tblChecklist.Source = tvSource;

		}

		//extracts data from XML and puts it in a Dictionary (section header as key, list of children as value)
		//to be used by TableView
		void ExtractDataFromXML() {
			dictData = new Dictionary<string, List<string>> ();

			var doc = new XmlDocument ();
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
	}
}