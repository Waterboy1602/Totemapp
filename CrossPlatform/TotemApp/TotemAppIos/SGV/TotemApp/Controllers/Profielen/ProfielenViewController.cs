using System;

using UIKit;
using TotemAppCore;

using System.Collections.Generic;
using MaterialControls;

namespace TotemAppIos {
	public partial class ProfielenViewController : UIViewController	{

		AppController _appController = AppController.Instance;

		public ProfielenViewController () : base ("ProfielenViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnAdd.TouchUpInside += deleteProfiles;
			//btnDelete.TouchUpInside += deleteProfiles;
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
			tblProfielen.Source = new ProfielenTableViewSource (_appController.DistinctProfielen);
			var empty = _appController.DistinctProfielen.Count == 0;
			tblProfielen.Hidden = empty;
			lblEmpty.Hidden = !empty;
			btnDelete.Hidden = empty;
		}

		private void updateListSource() {
			(tblProfielen.Source as ProfielenTableViewSource).Profielen = _appController.DistinctProfielen;
			tblProfielen.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			var empty = _appController.DistinctProfielen.Count == 0;
			tblProfielen.Hidden = empty;
			lblEmpty.Hidden = !empty;
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
			lblTitle.Hidden = true;
		}

		void exitDelete(object sender, EventArgs e) {
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			btnReturn.TouchUpInside -= exitDelete;
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnAdd.Hidden = false;
			lblTitle.Hidden = false;
		}
	}
}