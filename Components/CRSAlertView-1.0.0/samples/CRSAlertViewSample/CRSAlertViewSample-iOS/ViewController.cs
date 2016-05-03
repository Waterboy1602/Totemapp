using System;
using UIKit;
using CoreGraphics;
using Curse;

namespace CRSAlertViewSampleiOS
{
	public class ViewController : UIViewController
	{
		#region Properties
		UILabel _text;
		UIButton _button;


		string _preAcceptText = "Thanks for checking out CRSAlertView! Click the button below to pop the alert and see how it looks.";
		string _postAcceptText = "\n\nYou're super awesome!";
		#endregion


		#region View Lifecycle
		public ViewController ()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			// Build UI
			View.BackgroundColor = UIColor.FromRGB( 0xff, 0x74, 0x02 );

			_button = new UIButton {
				BackgroundColor = UIColor.White,
				Frame = new CGRect(20, View.Frame.Height - 80, View.Frame.Width - 40, 60)
			};
			_button.Layer.CornerRadius = 6;
			_button.SetAttributedTitle (new Foundation.NSAttributedString ("Launch Alert", new UIStringAttributes {
				ForegroundColor = View.BackgroundColor,
				Font = UIFont.SystemFontOfSize(18f),
			}), UIControlState.Normal);
			_button.TouchUpInside += (object sender, EventArgs e) => {
				LaunchAlert();
			};

			_text = new UILabel {
				Text = _preAcceptText,
				Frame = new CGRect(20, 20, View.Frame.Width - 40, _button.Frame.Top - 40),
				TextColor = UIColor.White,
				Font = UIFont.SystemFontOfSize(18f),
				TextAlignment = UITextAlignment.Center,
				Lines = 0
			};

			View.AddSubviews (new UIView[]{ _button, _text });
		}
		#endregion


		#region Launch Alert
		void LaunchAlert()
		{
			var cancelAction = new CRSAlertAction {
				Text = "Cancel",
			};
			var acceptAction = new CRSAlertAction {
				Text = "Accept",
				Highlighted = true,
				DidSelect = (alert) => {
					_text.Text = _preAcceptText + _postAcceptText;
				}
			};
			var alertView = new CRSAlertView {
				Title = "Sample Alert",
				Message = "This is pretty sweet huh? Thanks for taking the time to sample this project. Click accept if you accept that you're an extremely awesome individual.",
				Actions = new CRSAlertAction[]{ cancelAction, acceptAction },
				Image = UIImage.FromBundle("logo_black.png")
			};
			alertView.Show ();
		}
		#endregion
	}
}

