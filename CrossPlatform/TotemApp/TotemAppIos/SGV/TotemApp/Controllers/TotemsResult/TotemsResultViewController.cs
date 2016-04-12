using UIKit;

namespace TotemAppIos {
	public partial class TotemsResultViewController : BaseViewController {

		public TotemsResultViewController () : base ("TotemsResultViewController", null) {}


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

		public override void setData() {
			lblTitle.Text = "Totems";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			tblTotems.Source = new TotemsResultTableViewSource (_appController.TotemEigenschapDict);
		}

		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}
	}
}