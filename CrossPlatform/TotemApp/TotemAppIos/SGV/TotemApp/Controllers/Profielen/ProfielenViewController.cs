using System;

using UIKit;
using TotemAppCore;

using System.Collections.Generic;
using MaterialControls;

namespace TotemAppIos {
	public partial class ProfielenViewController : UIViewController	{

		AppController _appController = AppController.Instance;
		List<Profiel> profielen;

		public ProfielenViewController () : base ("ProfielenViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
		}

		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnAdd.TouchUpInside += addProfileDialog;
			btnDelete.TouchUpInside += deleteProfiles;
			_appController.NavigationController.GotoProfileTotemListEvent += gotoProfileTotemHandler;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnAdd.TouchUpInside -= addProfileDialog;
			btnDelete.TouchUpInside -= deleteProfiles;
			_appController.NavigationController.GotoProfileTotemListEvent -= gotoProfileTotemHandler;
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		void addProfileDialog (object sender, EventArgs e) {
			//Create Alert
			var textInputAlertController = UIAlertController.Create("Nieuw profiel", null, UIAlertControllerStyle.Alert);

			//Add Text Input
			textInputAlertController.AddTextField(textField => {
				textField.AutocapitalizationType = UITextAutocapitalizationType.Words;
				textField.Placeholder = "Naam";
			});

			//Add Actions
			var cancelAction = UIAlertAction.Create ("Annuleer", UIAlertActionStyle.Cancel, null);
			var okayAction = UIAlertAction.Create ("OK", UIAlertActionStyle.Default, alertAction => addProfile(textInputAlertController.TextFields[0].Text));

			textInputAlertController.AddAction(cancelAction);
			textInputAlertController.AddAction(okayAction);

			//Present Alert
			PresentViewController(textInputAlertController, true, null);
		}

		private void addProfile(string name) {
			if (_appController.GetProfielNamen ().Contains (name)) {
				var okAlertController = UIAlertController.Create (null, "Profiel " + name + " bestaat al", UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
				PresentViewController (okAlertController, true, null);
			} else if(name.Replace("'", "").Replace(" ", "").Equals("")) {
				var okAlertController = UIAlertController.Create (null, "Ongeldige naam", UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
				PresentViewController (okAlertController, true, null);	
			} else {
				_appController.AddProfile (name);
				updateListSource();
			}
		}

		private void setData() {
			lblTitle.Text = "Profielen";
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			imgAdd.Image = UIImage.FromBundle ("SharedAssets/add_white");
			imgDelete.Image = UIImage.FromBundle ("SharedAssets/delete_white");
			profielen = _appController.DistinctProfielen;
			tblProfielen.Source = new ProfielenTableViewSource (profielen);
			var empty = _appController.DistinctProfielen.Count == 0;
			tblProfielen.Hidden = empty;
			btnDelete.Hidden = empty;
			tblProfielen.TableFooterView = new UIView ();
		}

		private void updateListSource() {
			profielen = _appController.DistinctProfielen;
			(tblProfielen.Source as ProfielenTableViewSource).Profielen = profielen;
			tblProfielen.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			var empty = profielen.Count == 0;
			tblProfielen.Hidden = empty;
			btnDelete.Hidden = empty;
		}

		void gotoProfileTotemHandler() {
			NavigationController.PushViewController (new ProfielTotemsViewController(), true);
		}

		void deleteProfiles(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/close_white");
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnReturn.TouchUpInside += exitDelete;
			btnAdd.Hidden = true;
			addBtnWidth.Constant = 0;
			lblTitle.Hidden = true;
			((ProfielenTableViewSource)tblProfielen.Source).check = true;
			tblProfielen.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			btnDelete.TouchUpInside -= deleteProfiles;
			btnDelete.TouchUpInside += deleteDialog;
		}

		void exitDelete(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			btnReturn.TouchUpInside -= exitDelete;
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnAdd.Hidden = false;
			addBtnWidth.Constant = 50;
			lblTitle.Hidden = false;
			this.profielen = _appController.DistinctProfielen;
			(tblProfielen.Source as ProfielenTableViewSource).Profielen = profielen;
			((ProfielenTableViewSource)tblProfielen.Source).check = false;
			tblProfielen.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			btnDelete.TouchUpInside -= deleteDialog;
			btnDelete.TouchUpInside += deleteProfiles;
		}

		void deleteDialog(object sender, EventArgs e) {
			//Create Alert
			var okCancelAlertController = UIAlertController.Create(null, "Geselecteerde profielen verwijderen?", UIAlertControllerStyle.Alert);

			//Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create("Ja", UIAlertActionStyle.Default, alert => deleteSelected(sender, e)));
			okCancelAlertController.AddAction(UIAlertAction.Create("Nee", UIAlertActionStyle.Cancel, null));

			//Present Alert
			PresentViewController(okCancelAlertController, true, null);
		}

		void deleteSelected(object sender, EventArgs e) {
			var deleteList = profielen.FindAll (x => x.selected);
			foreach(Profiel p in deleteList)
				_appController.DeleteProfile (p.name);

			updateListSource ();
			exitDelete (sender, e);
		}
	}
}