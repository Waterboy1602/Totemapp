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
			foreach (var fontFamily in UIFont.FamilyNames) {
				Console.WriteLine (fontFamily+":");
				foreach (var fontName in UIFont.FontNamesForFamilyName (fontFamily)) {
					Console.WriteLine ("     "+fontName);
				}
			}
			lblTitle.Text = "TOTEMAPP";
			imgMountain.Image = UIImage.FromBundle ("SharedAssets/Berg");
			imgTotem.Image = UIImage.FromBundle ("SharedAssets/Totem");

			lblTotemsButton.Text = "TOTEMS";
			btnTotems.RippleColor = UIColor.FromRGB (0, 68, 116);
			lblEigenschappenButton.Text = "EIGENSCHAPPEN";
			btnEigenschappen.RippleColor = UIColor.FromRGB (0, 68, 116);
			lblProfielenButton.Text = "PROFIELEN";
			btnProfielen.RippleColor = UIColor.FromRGB (0, 68, 116);
			lblChecklistButton.Text = "TOTEMISATIE CHECKLIST";
			btnChecklist.RippleColor = UIColor.FromRGB (0, 68, 116);
			//imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");





			// Perform any additional setup after loading the view, typically from a nib.
		}
			
		#endregion

		#endregion

		#endregion

		#region private methods

		#endregion




	}
}


