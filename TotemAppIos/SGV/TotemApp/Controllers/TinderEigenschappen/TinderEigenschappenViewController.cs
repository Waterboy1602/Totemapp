using Foundation;

using ServiceStack.Text;

using System;

using UIKit;

namespace TotemAppIos {
	public partial class TinderEigenschappenViewController : BaseViewController	{

		int eigenschapCount;
		NSUserDefaults userDefs;

		public TinderEigenschappenViewController () : base ("TinderEigenschappenViewController", null) {}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			userDefs = NSUserDefaults.StandardUserDefaults;

		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside+= btnReturnTouchUpInside;

			btnJa.TouchUpInside += pushJa ;
			btnNee.TouchUpInside += pushNee;

		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside-= btnReturnTouchUpInside;

			btnJa.TouchUpInside -= pushJa;
			btnNee.TouchUpInside -= pushNee;

			var ser = JsonSerializer.SerializeToString (_appController.Eigenschappen);
			userDefs.SetString (ser, "eigenschappen");
			userDefs.Synchronize ();

		}

		public override void setData () {
			lblTitle.Text = "Eigenschappen";
			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			UpdateScreen ();
		}

		public void UpdateScreen() {
			if (eigenschapCount < 324) {
				lblEigenschap.Text = _appController.Eigenschappen[eigenschapCount].name;
			} else {
				_appController.FireSelectedEvent ();
				NavigationController.PopViewController (true);
			}
		}

		public void pushJa(object sender, EventArgs e) {
			_appController.Eigenschappen[eigenschapCount].selected = true;
			eigenschapCount++;
			UpdateScreen ();
		}

		public void pushNee(object sender, EventArgs e) {
			_appController.Eigenschappen[eigenschapCount].selected = false;
			eigenschapCount++;
			UpdateScreen ();
		}
	}
}