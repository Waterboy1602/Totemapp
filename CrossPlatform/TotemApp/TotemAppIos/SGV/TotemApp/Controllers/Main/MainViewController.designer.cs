// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TotemAppIos
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		UIKit.UIButton btnChecklist { get; set; }

		[Outlet]
		UIKit.UIButton btnEigenschappen { get; set; }

		[Outlet]
		UIKit.UIButton btnProfielen { get; set; }

		[Outlet]
		UIKit.UIButton btnTotems { get; set; }

		[Outlet]
		UIKit.UIImageView imgMountain { get; set; }

		[Outlet]
		UIKit.UIImageView imgTotem { get; set; }

		[Outlet]
		UIKit.UILabel lblChecklistButton { get; set; }

		[Outlet]
		UIKit.UILabel lblEigenschappenButton { get; set; }

		[Outlet]
		UIKit.UILabel lblProfielenButton { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UILabel lblTotemsButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnChecklist != null) {
				btnChecklist.Dispose ();
				btnChecklist = null;
			}
			if (btnEigenschappen != null) {
				btnEigenschappen.Dispose ();
				btnEigenschappen = null;
			}
			if (btnProfielen != null) {
				btnProfielen.Dispose ();
				btnProfielen = null;
			}
			if (btnTotems != null) {
				btnTotems.Dispose ();
				btnTotems = null;
			}
			if (imgMountain != null) {
				imgMountain.Dispose ();
				imgMountain = null;
			}
			if (imgTotem != null) {
				imgTotem.Dispose ();
				imgTotem = null;
			}
			if (lblChecklistButton != null) {
				lblChecklistButton.Dispose ();
				lblChecklistButton = null;
			}
			if (lblEigenschappenButton != null) {
				lblEigenschappenButton.Dispose ();
				lblEigenschappenButton = null;
			}
			if (lblProfielenButton != null) {
				lblProfielenButton.Dispose ();
				lblProfielenButton = null;
			}
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
			if (lblTotemsButton != null) {
				lblTotemsButton.Dispose ();
				lblTotemsButton = null;
			}
		}
	}
}
