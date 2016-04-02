using System;

using UIKit;
using TotemAppCore;

namespace TotemAppIos
{
	public partial class MainViewController : UIViewController
	{

		#region delegates

		#endregion

		#region variables
		AppController _appController = AppController.Instance;
		#endregion

		#region constructor
		public MainViewController () : base ("MainViewController", null)
		{
		}
		#endregion

		#region properties

		#endregion

		#region public methods

		#region overrided methods

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}
		#region viewlifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			btnTotems.TouchUpInside+= btnTotemsTouchUpInsideHandler;
			btnEigenschappen.TouchUpInside+= btnEigenschappenTouchUpInside;

			_appController.NavigationController.GotoTotemListEvent+= gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent+= gotoEigenschapListHandler;
		}





		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			btnTotems.TouchUpInside -= btnTotemsTouchUpInsideHandler;
			btnEigenschappen.TouchUpInside-= btnEigenschappenTouchUpInside;

			_appController.NavigationController.GotoTotemListEvent-= gotoTotemListHandler;
			_appController.NavigationController.GotoEigenschapListEvent-= gotoEigenschapListHandler;
		}
		#endregion

		#endregion

		#endregion

		#region private methods
		private void setData(){
			lblTitle.Text = "TOTEMAPP";
			lblTotemsButton.Text = "TOTEMS";
			lblEigenschappenButton.Text = "EIGENSCHAPPEN";
			lblProfielenButton.Text = "PROFIELEN";
			lblChecklistButton.Text = "TOTEMISATIE CHECKLIST";

			imgMountain.Image = UIImage.FromBundle ("SharedAssets/Berg");
			imgTotem.Image = UIImage.FromBundle ("SharedAssets/Totem");

			btnTotems.RippleColor = UIColor.FromRGB (0, 68, 116);
			btnEigenschappen.RippleColor = UIColor.FromRGB (0, 68, 116);
			btnProfielen.RippleColor = UIColor.FromRGB (0, 68, 116);
			btnChecklist.RippleColor = UIColor.FromRGB (0, 68, 116);
		}

		void btnTotemsTouchUpInsideHandler (object sender, EventArgs e)
		{
			_appController.TotemMenuItemClicked ();
		}
		void btnEigenschappenTouchUpInside (object sender, EventArgs e)
		{
			_appController.EigenschappenMenuItemClicked ();
		}
		void gotoTotemListHandler ()
		{
			NavigationController.PushViewController (new TotemListViewController(),true);
		}
		void gotoEigenschapListHandler ()
		{
			NavigationController.PushViewController (new EigenschappenViewController(),true);
		}

		#endregion




	}
}


