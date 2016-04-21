using System;
using System.Collections.Generic;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	public partial class ProfielTotemsViewController : BaseViewController {

		List<Totem> totems;

		public ProfielTotemsViewController () : base ("ProfielTotemsViewController", null) {}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			if (tblTotems.Source != null) {
				var list = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
				var empty = (list.Count == 0);
				tblTotems.Hidden = empty;
				btnDelete.Hidden = empty;
				(tblTotems.Source as ProfielTotemsTableViewSource).Totems = list;
				tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			}

			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnDelete.TouchUpInside += deleteProfileTotems;

			_appController.NavigationController.GotoTotemDetailEvent += gotoTotemDetailHandler;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnDelete.TouchUpInside -= deleteProfileTotems;

			_appController.NavigationController.GotoTotemDetailEvent -= gotoTotemDetailHandler;
		}

		public override void setData() {
			lblTitle.Text = "Totems voor " + _appController.CurrentProfiel.name;

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			imgDelete.Image = UIImage.FromBundle ("SharedAssets/delete_white");

			totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
			tblTotems.Source = new ProfielTotemsTableViewSource (totems);

			//hide necessary UI elements
			var empty = (totems.Count == 0);
			tblTotems.Hidden = empty;
			btnDelete.Hidden = empty;

			//empty view at footer to prevent empty cells at the bottom
			tblTotems.TableFooterView = new UIView ();
		}
			
		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}

		//updates data of TableView and handles necessary UI changes
		void updateListSource() {
			totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
			(tblTotems.Source as ProfielTotemsTableViewSource).Totems = totems;
			tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			var empty = totems.Count == 0;
			tblTotems.Hidden = empty;
			btnDelete.Hidden = empty;
		}

		//handles UI changes for deleting totems
		void deleteProfileTotems(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/close_white");
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnReturn.TouchUpInside += exitDelete;
			lblTitle.Hidden = true;
			((ProfielTotemsTableViewSource)tblTotems.Source).check = true;
			tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			btnDelete.TouchUpInside -= deleteProfileTotems;
			btnDelete.TouchUpInside += deleteDialog;
		}

		//reverts UI changes for deleting totems
		void exitDelete(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			btnReturn.TouchUpInside -= exitDelete;
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			lblTitle.Hidden = false;
			deselectAndUpdate ();
			((ProfielTotemsTableViewSource)tblTotems.Source).check = false;
			tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			btnDelete.TouchUpInside -= deleteDialog;
			btnDelete.TouchUpInside += deleteProfileTotems;
		}

		//delete confirmation dialog
		void deleteDialog(object sender, EventArgs e) {
			var okCancelAlertController = UIAlertController.Create(null, "Geselecteerde totems verwijderen?", UIAlertControllerStyle.Alert);

			okCancelAlertController.AddAction(UIAlertAction.Create("Ja", UIAlertActionStyle.Default, alert => deleteSelected(sender, e)));
			okCancelAlertController.AddAction(UIAlertAction.Create("Nee", UIAlertActionStyle.Cancel, null));

			PresentViewController(okCancelAlertController, true, null);
		}

		//the actual deletion of the selected profiles
		void deleteSelected(object sender, EventArgs e) {
			var deleteList = totems.FindAll (x => x.selected);
			foreach (Totem t in deleteList)
				_appController.DeleteTotemFromProfile (t.nid, _appController.CurrentProfiel.name);

			updateListSource ();
			exitDelete (sender, e);
		}

		//resets selection and updates list
		void deselectAndUpdate() {
			totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
			var deselect = totems.FindAll (x => x.selected);
			foreach (Totem t in deselect)
				t.selected = false;

			(tblTotems.Source as ProfielTotemsTableViewSource).Totems = totems;
		}
	}
}