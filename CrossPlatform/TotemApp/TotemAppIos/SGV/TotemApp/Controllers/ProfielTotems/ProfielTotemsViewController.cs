using System;

using UIKit;
using TotemAppCore;

namespace TotemAppIos {
	public partial class ProfielTotemsViewController : UIViewController	{

		AppController _appController = AppController.Instance;

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
				(tblTotems.Source as ProfielTotemsTableViewSource).Totems = _appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name);
				tblTotems.ReloadSections (new Foundation.NSIndexSet (0), UITableViewRowAnimation.None);
			}
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			_appController.NavigationController.GotoTotemDetailEvent += gotoTotemDetailHandler;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			_appController.NavigationController.GotoTotemDetailEvent -= gotoTotemDetailHandler;
		}

		private void setData() {
			lblTitle.Text = "Totems voor " + _appController.CurrentProfiel.name;

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			tblTotems.Source = new ProfielTotemsTableViewSource (_appController.GetTotemsFromProfiel (_appController.CurrentProfiel.name));
		}
			
		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}
	}
}