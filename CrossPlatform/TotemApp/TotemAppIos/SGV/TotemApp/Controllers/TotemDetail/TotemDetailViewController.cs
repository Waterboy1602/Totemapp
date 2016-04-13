using System;

using Foundation;
using TotemAppCore;
using UIKit;

namespace TotemAppIos {
	public partial class TotemDetailViewController : BaseViewController {
		public TotemDetailViewController () : base ("TotemDetailViewController", null) {}

		bool add;

		public override void ViewDidLoad () {
			base.ViewDidLoad ();

			var leftSwipeGesture = new UISwipeGestureRecognizer (LeftSwipeHandler);
			leftSwipeGesture.Direction = UISwipeGestureRecognizerDirection.Left;
			var rightSwipeGesture = new UISwipeGestureRecognizer (RightSwipeHandler);
			rightSwipeGesture.Direction = UISwipeGestureRecognizerDirection.Right;

			View.AddGestureRecognizer (leftSwipeGesture);
			View.AddGestureRecognizer (rightSwipeGesture);
		}

		void LeftSwipeHandler (UISwipeGestureRecognizer gestureRecognizer) {
			var next = _appController.NextTotem;
			if(next != null) {
				_appController.CurrentTotem = next;
				setData ();
			}
		}

		void RightSwipeHandler (UISwipeGestureRecognizer gestureRecognizer) {
			var prev = _appController.PrevTotem;
			if(prev != null) {
				_appController.CurrentTotem = prev;
				setData ();
			}       
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

		public override void setData() {
			lblTitle.Text = "Beschrijving";

			//shows button depending on action
			add = (_appController.CurrentProfiel == null);
			if(add)
				imgAction.Image = UIImage.FromBundle ("SharedAssets/add_white");
			else
				imgAction.Image = UIImage.FromBundle ("SharedAssets/delete_white");

			//styling for title (totem name)
			var paragraphStyleTitle = new NSMutableParagraphStyle ();
			paragraphStyleTitle.LineSpacing = 15;

			//styling for synonyms
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

			//whitespace for UI purposes
			content += "\n";

			//applying attributes
			var title_synonyms = new NSMutableAttributedString(content);
			title_synonyms.SetAttributes (titleAttributes.Dictionary, new NSRange (0, _appController.CurrentTotem.title.Length));
			title_synonyms.SetAttributes (synonymsAttributes.Dictionary, new NSRange (_appController.CurrentTotem.title.Length, (title_synonyms.Length-_appController.CurrentTotem.title.Length)));

			lblNumber.Text = _appController.CurrentTotem.number + ".";
			lblHead.AttributedText = title_synonyms;

			lblBody.Text = _appController.CurrentTotem.body;
			imgLine.Image = UIImage.FromBundle ("SharedAssets/Lijn_bold");

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
		}

		//handles button click depending on action
		void btnActionTouchUpInside(object sender, EventArgs e) {
			if (add) {
				UIAlertController actionSheetAlert = UIAlertController.Create (null, null, UIAlertControllerStyle.ActionSheet);
				foreach (Profiel p in _appController.DistinctProfielen) {
					actionSheetAlert.AddAction (UIAlertAction.Create (p.name, UIAlertActionStyle.Default, action => addToProfile (p.name)));
				}
					
				actionSheetAlert.AddAction (UIAlertAction.Create ("Nieuw profiel", UIAlertActionStyle.Default, action => addProfileDialog ()));

				actionSheetAlert.AddAction (UIAlertAction.Create ("Annuleer", UIAlertActionStyle.Cancel, null));

				// Required for iPad - You must specify a source for the Action Sheet since it is
				// displayed as a popover
				UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
				if (presentationPopover != null) {
					presentationPopover.SourceView = View;
					presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
				}
					
				PresentViewController (actionSheetAlert, true, null);
			} else {
				var okCancelAlertController = UIAlertController.Create(null, _appController.CurrentTotem.title + " verwijderen uit profiel " + _appController.CurrentProfiel.name + "?", UIAlertControllerStyle.Alert);

				okCancelAlertController.AddAction(UIAlertAction.Create("Ja", UIAlertActionStyle.Default, alert => deleteFromProfile()));
				okCancelAlertController.AddAction(UIAlertAction.Create("Nee", UIAlertActionStyle.Cancel, null));

				PresentViewController(okCancelAlertController, true, null);
			}	
		}

		void addToProfile(string name) {
			_appController.AddTotemToProfiel (_appController.CurrentTotem.nid, name);
		}

		void addProfileDialog () {
			var textInputAlertController = UIAlertController.Create("Nieuw profiel", null, UIAlertControllerStyle.Alert);

			textInputAlertController.AddTextField(textField => {
				textField.AutocapitalizationType = UITextAutocapitalizationType.Words;
				textField.Placeholder = "Naam";
			});
				
			var cancelAction = UIAlertAction.Create ("Annuleer", UIAlertActionStyle.Cancel, alertAction => Console.WriteLine ("Cancel was Pressed"));
			var okayAction = UIAlertAction.Create ("OK", UIAlertActionStyle.Default, alertAction => addProfile(textInputAlertController.TextFields[0].Text));

			textInputAlertController.AddAction(cancelAction);
			textInputAlertController.AddAction(okayAction);

			PresentViewController(textInputAlertController, true, null);
		}

		//handles wrong input and adds profile
		void addProfile(string name) {
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

		//deletes totem drom profile and returns to totem page
		void deleteFromProfile() {
			_appController.DeleteTotemFromProfile (_appController.CurrentTotem.nid, _appController.CurrentProfiel.name);
			NavigationController.PopViewController (true);
		}
	}
}