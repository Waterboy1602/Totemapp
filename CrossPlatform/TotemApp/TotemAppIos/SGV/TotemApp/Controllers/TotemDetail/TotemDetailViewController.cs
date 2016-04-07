using System;

using UIKit;

using TotemAppCore;
using Foundation;

namespace TotemAppIos {
	public partial class TotemDetailViewController : UIViewController {
		public TotemDetailViewController () : base ("TotemDetailViewController", null) {}

		AppController _appController = AppController.Instance;
		bool add;

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
			btnAction.TouchUpInside += btnActionTouchUpInside;
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnAction.TouchUpInside -= btnActionTouchUpInside;
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}

		private void setData() {
			lblTitle.Text = "Beschrijving";

			add = (_appController.CurrentProfiel == null);
			if(add)
				imgAction.Image = UIImage.FromBundle ("SharedAssets/add_white");
			else
				imgAction.Image = UIImage.FromBundle ("SharedAssets/delete_white");

			var paragraphStyleTitle = new NSMutableParagraphStyle ();
			paragraphStyleTitle.LineSpacing = 15;

			var paragraphStyleSyn = new NSMutableParagraphStyle ();
			paragraphStyleSyn.LineSpacing = 20;



			var titleAttributes = new UIStringAttributes {
				Font = UIFont.FromName("VerveineW01-Regular", 35f),
				ParagraphStyle = paragraphStyleTitle	
			};

			var synonymsAttributes = new UIStringAttributes {
				Font = UIFont.FromName("DIN-LightItalic", 17f),
				ParagraphStyle = paragraphStyleSyn					
			};

			var content = _appController.CurrentTotem.title;
			if(_appController.CurrentTotem.synonyms != null)
				content += " - " + _appController.CurrentTotem.synonyms;

			content += "\n";
			
			var title_synonyms = new NSMutableAttributedString(content);
			title_synonyms.SetAttributes (titleAttributes.Dictionary, new NSRange (0, _appController.CurrentTotem.title.Length));
			title_synonyms.SetAttributes (synonymsAttributes.Dictionary, new NSRange (_appController.CurrentTotem.title.Length, (title_synonyms.Length-_appController.CurrentTotem.title.Length)));

			lblNumber.Text = _appController.CurrentTotem.number + ".";
			lblHead.AttributedText = title_synonyms;

			lblBody.Text = _appController.CurrentTotem.body;
			imgLine.Image = UIImage.FromBundle ("SharedAssets/Lijn_bold");

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");

			UIColor color = UIColor.White;
		}

		void btnActionTouchUpInside(object sender, EventArgs e) {
			if (add) {
				// Create a new Alert Controller
				UIAlertController actionSheetAlert = UIAlertController.Create (null, null, UIAlertControllerStyle.ActionSheet);
				foreach (Profiel p in _appController.DistinctProfielen) {
					actionSheetAlert.AddAction (UIAlertAction.Create (p.name, UIAlertActionStyle.Default, (action) => addToProfile (p.name)));
				}
					
				actionSheetAlert.AddAction (UIAlertAction.Create ("Nieuw profiel", UIAlertActionStyle.Default, (action) => addProfileDialog ()));

				actionSheetAlert.AddAction (UIAlertAction.Create ("Annuleer", UIAlertActionStyle.Cancel, null));

				// Required for iPad - You must specify a source for the Action Sheet since it is
				// displayed as a popover
				UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
				if (presentationPopover != null) {
					presentationPopover.SourceView = this.View;
					presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
				}

				// Display the alert
				this.PresentViewController (actionSheetAlert, true, null);
			} else {
				//Create Alert
				var okCancelAlertController = UIAlertController.Create(null, _appController.CurrentTotem.title + " verwijderen uit profiel " + _appController.CurrentProfiel.name + "?", UIAlertControllerStyle.Alert);

				//Add Actions
				okCancelAlertController.AddAction(UIAlertAction.Create("Ja", UIAlertActionStyle.Default, alert => deleteFromProfile()));
				okCancelAlertController.AddAction(UIAlertAction.Create("Nee", UIAlertActionStyle.Cancel, null));

				//Present Alert
				PresentViewController(okCancelAlertController, true, null);
			}
				
		}

		void addToProfile(string name) {
			_appController.AddTotemToProfiel (_appController.CurrentTotem.nid, name);
		}

		void addProfileDialog () {
			//Create Alert
			var textInputAlertController = UIAlertController.Create("Nieuw profiel", null, UIAlertControllerStyle.Alert);

			//Add Text Input
			textInputAlertController.AddTextField(textField => {
				textField.AutocapitalizationType = UITextAutocapitalizationType.Words;
			});

			//Add Actions
			var cancelAction = UIAlertAction.Create ("Annuleer", UIAlertActionStyle.Cancel, alertAction => Console.WriteLine ("Cancel was Pressed"));
			var okayAction = UIAlertAction.Create ("OK", UIAlertActionStyle.Default, alertAction => addProfile(textInputAlertController.TextFields[0].Text));

			textInputAlertController.AddAction(cancelAction);
			textInputAlertController.AddAction(okayAction);

			//Present Alert
			PresentViewController(textInputAlertController, true, null);
		}

		private void addProfile(string name) {
			if (_appController.GetProfielNamen ().Contains (name)) {
				var okAlertController = UIAlertController.Create (null, "Profiel " + name + " bestaat al", UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
				PresentViewController (okAlertController, true, null);
			} else if(name.Replace("'", "").Replace(" ", "").Equals("")) {
				var okAlertController = UIAlertController.Create (null, "Ongeldige naam", UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
				PresentViewController (okAlertController, true, null);	
			} else {
				_appController.AddProfile (name);
				_appController.AddTotemToProfiel (_appController.CurrentTotem.nid, name);
			}
		}

		void deleteFromProfile() {
			_appController.DeleteTotemFromProfile (_appController.CurrentTotem.nid, _appController.CurrentProfiel.name);
			NavigationController.PopViewController (true);
		}
	}
}