using System;

using UIKit;
using TotemAppCore;
using System.Linq;

namespace TotemAppIos {
	public partial class TotemsResultViewController : UIViewController {
		AppController _appController = AppController.Instance;

		public TotemsResultViewController () : base ("TotemsResultViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			_appController.NavigationController.GotoTotemDetailEvent += gotoTotemDetailHandler;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			_appController.NavigationController.GotoTotemDetailEvent -= gotoTotemDetailHandler;
		}

		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		private void setData() {
			lblTitle.Text = "Totems";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			tblTotems.Source = new TotemsResultTableViewSource (_appController.TotemEigenschapDict);
		}

		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}
	}
}


