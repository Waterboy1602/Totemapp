using System;

using UIKit;
using TotemAppCore;

namespace TotemAppIos
{
	public partial class TotemListViewController : UIViewController
	{
		#region delegates

		#endregion

		#region variables
		AppController _appController = AppController.Instance;
		#endregion

		#region constructor
		public TotemListViewController () : base ("TotemListViewController", null)
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
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside+= btnReturnTouchUpInside;
		}
			
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside-= btnReturnTouchUpInside;
		}
		#endregion

		#endregion

		#endregion

		#region private methods

		private void setData(){
			lblTitle.Text = "Totems";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			imgSearch.Image = UIImage.FromBundle ("SharedAssets/search_white");

			tblTotems.Source = new TotemsTableViewSource (_appController.Totems);
		}

		void btnReturnTouchUpInside (object sender, EventArgs e)
		{
			NavigationController.PopViewController (true);
		}


		#endregion




	}
}


