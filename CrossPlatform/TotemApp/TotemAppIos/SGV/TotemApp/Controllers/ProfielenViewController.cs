using System;

using UIKit;

namespace TotemAppIos {
	public partial class ProfielenViewController : UIViewController	{
		public ProfielenViewController () : base ("ProfielenViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
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
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		private void setData() {
			lblTitle.Text = "Profielen";
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
		}
	}
}