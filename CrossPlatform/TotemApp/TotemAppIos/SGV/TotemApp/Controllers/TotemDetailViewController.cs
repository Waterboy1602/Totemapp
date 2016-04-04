using System;

using UIKit;

using TotemAppCore;
using Foundation;

namespace TotemAppIos {
	public partial class TotemDetailViewController : UIViewController {
		public TotemDetailViewController () : base ("TotemDetailViewController", null) {}

		AppController _appController = AppController.Instance;

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		private void setData() {
			lblTitle.Text = "Beschrijving";

			var paragraphStyle = new NSMutableParagraphStyle ();
			paragraphStyle.LineSpacing = 15;

			var titleAttributes = new UIStringAttributes {
				Font = UIFont.FromName("VerveineW01-Regular", 35f),
				ParagraphStyle = paragraphStyle	
			};

			var synonymsAttributes = new UIStringAttributes {
				Font = UIFont.FromName("DIN-LightItalic", 17f),
				ParagraphStyle = paragraphStyle					
			};

			var content = _appController.CurrentTotem.title;
			if(_appController.CurrentTotem.synonyms != null)
				content += " - " + _appController.CurrentTotem.synonyms;
			
			var title_synonyms = new NSMutableAttributedString(content);
			title_synonyms.SetAttributes (titleAttributes.Dictionary, new NSRange (0, _appController.CurrentTotem.title.Length));
			title_synonyms.SetAttributes (synonymsAttributes.Dictionary, new NSRange (_appController.CurrentTotem.title.Length, (title_synonyms.Length-_appController.CurrentTotem.title.Length)));

			lblNumber.Text = _appController.CurrentTotem.number + ". ";
			lblHead.AttributedText = title_synonyms;
			lblBody.Text = _appController.CurrentTotem.body;
			imgLine.Image = UIImage.FromBundle ("SharedAssets/Lijn_bold");

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			UIColor color = UIColor.White;
		}
	}
}