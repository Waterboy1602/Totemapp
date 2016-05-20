using Foundation;

using ServiceStack.Text;

using System.Collections.Generic;
using System.Xml;

using UIKit;
using System;

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
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnReset.TouchUpInside += BtnResetTouchUpInside;

			var ser = userDefs.StringForKey ("states");
			if (ser != null) {
				states = JsonSerializer.DeserializeFromString <List<List<bool>>> (ser);
				tvSource.UpdateStates (states);
				tblChecklist.ReloadData ();
			}
		}

		void BtnResetTouchUpInside (object sender, System.EventArgs e) {
			var okCancelAlertController = UIAlertController.Create(null, "Checklist resetten?", UIAlertControllerStyle.Alert);

			okCancelAlertController.AddAction(UIAlertAction.Create("Ja", UIAlertActionStyle.Default, alert => ResetChecklist(sender, e)));
			okCancelAlertController.AddAction(UIAlertAction.Create("Nee", UIAlertActionStyle.Cancel, null));

			PresentViewController(okCancelAlertController, true, null);
		}

		void ResetChecklist(object sender, EventArgs e) {
			tvSource.ResetStates ();
			tblChecklist.ReloadData ();
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnReset.TouchUpInside -= BtnResetTouchUpInside;

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
			imgReset.Image = UIImage.FromBundle ("SharedAssets/reset_white");

			ExtractDataFromXML ();

			lblHead.Font = UIFont.FromName("VerveineW01-Regular", 20f);
			lblFoot.Font = UIFont.FromName("VerveineW01-Regular", 20f);
			lblFoot.Text = "VEEL SUCCES!";
			lblHead.Text = "Een goede, kwalitatieve totemisatie vergt tijd en inspanning. Deze handige checklist helpt je om niets te vergeten. Wil je weten wat er achter deze checklist schuilt? Kijk dan in de Totemmap, hét handboek voor elke totemisatie en een bron van ideeën en inspiratie om je totemactiviteiten top te maken.";

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