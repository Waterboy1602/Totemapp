using System;

using Foundation;

using TotemAppCore;

using UIKit;

namespace TotemAppIos {
	
	//base class of all ViewControllers
	public abstract partial class BaseViewController : UIViewController	{

		//instance of AppController
		protected AppController _appController = AppController.Instance;

		protected BaseViewController (string nibName, NSBundle bundle) : base (nibName, bundle) {}

		//sets ViewController data and hides navbar
		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		//back button
		protected void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		//make navbar text white
		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		//sets ViewController data
		//overwritten in every child class
		public abstract void setData();
	}
}