using System;

using UIKit;
using TotemAppCore;
using System.Collections.Generic;

namespace TotemAppIos {
	public partial class ProfielTotemsViewController : UIViewController	{

		AppController _appController = AppController.Instance;
		List<Totem> totems;

		public ProfielTotemsViewController () : base ("ProfielTotemsViewController", null) {}

		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			// Perform any additional setup after loading the view, typically from a nib.
		}

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
			btnDelete.TouchUpInside += deleteProfiles;

			_appController.NavigationController.GotoTotemDetailEvent += gotoTotemDetailHandler;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnDelete.TouchUpInside -= deleteProfiles;

			_appController.NavigationController.GotoTotemDetailEvent -= gotoTotemDetailHandler;
		}

		private void setData() {
			lblTitle.Text = "Totems voor " + _appController.CurrentProfiel.name;

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			imgDelete.Image = UIImage.FromBundle ("SharedAssets/delete_white");

			totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);

			var empty = (totems.Count == 0);
			tblTotems.Hidden = empty;
			btnDelete.Hidden = empty;

			tblTotems.Source = new ProfielTotemsTableViewSource (totems);
			tblTotems.TableFooterView = new UIView ();
		}
			
		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		private void updateListSource() {
			totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
			(tblTotems.Source as ProfielTotemsTableViewSource).Totems = totems;
			tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			var empty = totems.Count == 0;
			tblTotems.Hidden = empty;
			btnDelete.Hidden = empty;
		}

		void deleteProfiles(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/close_white");
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnReturn.TouchUpInside += exitDelete;
			lblTitle.Hidden = true;
			((ProfielTotemsTableViewSource)tblTotems.Source).check = true;
			tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			btnDelete.TouchUpInside -= deleteProfiles;
			btnDelete.TouchUpInside += deleteDialog;
		}

		void exitDelete(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			btnReturn.TouchUpInside -= exitDelete;
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			lblTitle.Hidden = false;
			deselectAndUpdate ();
			((ProfielTotemsTableViewSource)tblTotems.Source).check = false;
			tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			btnDelete.TouchUpInside -= deleteDialog;
			btnDelete.TouchUpInside += deleteProfiles;
		}

		void deleteDialog(object sender, EventArgs e) {
			//Create Alert
			var okCancelAlertController = UIAlertController.Create(null, "Geselecteerde totems verwijderen?", UIAlertControllerStyle.Alert);

			//Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create("Ja", UIAlertActionStyle.Default, alert => deleteSelected(sender, e)));
			okCancelAlertController.AddAction(UIAlertAction.Create("Nee", UIAlertActionStyle.Cancel, null));

			//Present Alert
			PresentViewController(okCancelAlertController, true, null);
		}

		void deleteSelected(object sender, EventArgs e) {
			var deleteList = totems.FindAll (x => x.selected);
			foreach (Totem t in deleteList)
				_appController.DeleteTotemFromProfile (t.nid, _appController.CurrentProfiel.name);

			updateListSource ();
			exitDelete (sender, e);
		}

		void deselectAndUpdate() {
			this.totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
			var deselect = totems.FindAll (x => x.selected);
			foreach (Totem t in deselect)
				t.selected = false;

			(tblTotems.Source as ProfielTotemsTableViewSource).Totems = totems;
		}
	}
}