using System;

using UIKit;
using TotemAppCore;

namespace TotemAppIos
{
	public partial class MainViewController : UIViewController
	{
		AppController _appController = AppController.Instance;
		public MainViewController () : base ("MainViewController", null)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			btnClick.TouchUpInside+= BtnClickTouchUpInside;
			_appController.updateLabelEvent+= _appController_updateLabelEvent;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void _appController_updateLabelEvent (int timesClicked)
		{
			lblClicked.Text = string.Format ("clicked {0} times",timesClicked);
		}

		void BtnClickTouchUpInside (object sender, EventArgs e)
		{
			Console.WriteLine (_appController.Totems[0].title);
			lblClicked.Text = _appController.Eigenschappen [0].name;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


