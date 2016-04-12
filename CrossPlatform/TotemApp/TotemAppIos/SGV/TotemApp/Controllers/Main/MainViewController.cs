using System;

using UIKit;

namespace TotemAppIos {
	public partial class MainViewController : BaseViewController {


		public MainViewController () : base ("MainViewController", null) {}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnTotems.TouchUpInside += btnTotemsTouchUpInsideHandler;
			btnEigenschappen.TouchUpInside += btnEigenschappenTouchUpInside;
			btnProfielen.TouchUpInside += btnProfielenTouchUpInside;
			btnChecklist.TouchUpInside += BtnChecklistTouchUpInside;

			_appController.NavigationController.GotoTotemListEvent += gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent += gotoEigenschapListHandler;
			_appController.NavigationController.GotoProfileListEvent += gotoProfileListHandler;
			_appController.NavigationController.GotoChecklistEvent += gotoChecklistEvent;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnTotems.TouchUpInside -= btnTotemsTouchUpInsideHandler;
			btnEigenschappen.TouchUpInside -= btnEigenschappenTouchUpInside;
			btnProfielen.TouchUpInside -= btnProfielenTouchUpInside;
			btnChecklist.TouchUpInside -= BtnChecklistTouchUpInside;

			_appController.NavigationController.GotoTotemListEvent -= gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent -= gotoEigenschapListHandler;
			_appController.NavigationController.GotoProfileListEvent -= gotoProfileListHandler;
			_appController.NavigationController.GotoChecklistEvent -= gotoChecklistEvent;
		}

		public override void setData() {
			lblTitle.Text = "TOTEMAPP";
			lblTotemsButton.Text = "TOTEMS";
			lblEigenschappenButton.Text = "EIGENSCHAPPEN";
			lblProfielenButton.Text = "PROFIELEN";
			lblChecklistButton.Text = "TOTEMISATIE CHECKLIST";

			imgMountain.Image = UIImage.FromBundle ("SharedAssets/Berg");
			imgTotem.Image = UIImage.FromBundle ("SharedAssets/Totem");
		}

										//button click handlers//
		void btnTotemsTouchUpInsideHandler (object sender, EventArgs e)	{
			_appController.TotemMenuItemClicked ();
		}

		void btnEigenschappenTouchUpInside (object sender, EventArgs e)	{
			_appController.EigenschappenMenuItemClicked ();
		}

		void btnProfielenTouchUpInside (object sender, EventArgs e)	{
			_appController.ProfileMenuItemClicked ();
		}

		void BtnChecklistTouchUpInside (object sender, EventArgs e) {
			_appController.ChecklistMenuItemClicked ();
		}

										//navigation event handlers//
		void gotoTotemListHandler () {
			NavigationController.PushViewController (new TotemListViewController(),true);
		}

		void gotoEigenschapListHandler () {
			NavigationController.PushViewController (new EigenschappenViewController(),true);
		}

		void gotoProfileListHandler () {
			NavigationController.PushViewController (new ProfielenViewController(),true);
		}

		void gotoChecklistEvent () {
			NavigationController.PushViewController (new ChecklistViewController(),true);
		}
	}
}