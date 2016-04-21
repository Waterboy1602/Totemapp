using System;

using UIKit;
using System.Threading.Tasks;

namespace TotemAppIos {
	public partial class MainViewController : BaseViewController {


		public MainViewController () : base ("MainViewController", null) {}

		bool info;

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnTotems.TouchUpInside += btnTotemsTouchUpInsideHandler;
			btnEigenschappen.TouchUpInside += btnEigenschappenTouchUpInside;
			btnProfielen.TouchUpInside += btnProfielenTouchUpInside;
			btnChecklist.TouchUpInside += BtnChecklistTouchUpInside;

			//long press on title
			var uitgr = new UILongPressGestureRecognizer(TapTitle);
			uitgr.MinimumPressDuration = 0.5;
			lblTitle.AddGestureRecognizer(uitgr);

			_appController.NavigationController.GotoTotemListEvent += gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent += gotoEigenschapListHandler;
			_appController.NavigationController.GotoProfileListEvent += gotoProfileListHandler;
			_appController.NavigationController.GotoChecklistEvent += gotoChecklistEvent;
		}

		//fades info in and back out (after two seconds)
		public void TapTitle(UILongPressGestureRecognizer uitgr) {
			if (!info) {
				info = true;

				//fade in
				UIView.Animate (0.3, () => {
					lblInfo.Alpha = 1;
				});

				TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext ();
				Task.Delay (2000).ContinueWith (task => {
					//fade out
					UIView.Animate (0.5, () => {
						lblInfo.Alpha = 0;
					});
					info = false;
				}, uiContext);
			}
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
			lblInfo.Text = "door Frederick Eskens\nvoor Scouts en Gidsen Vlaanderen vzw";

			lblInfo.Alpha = 0;

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