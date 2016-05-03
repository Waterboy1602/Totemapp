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
	[Register ("TinderEigenschappenViewController")]
	partial class TinderEigenschappenViewController
	{
		[Outlet]
		UIKit.UIButton btnJa { get; set; }

		[Outlet]
		UIKit.UIButton btnMore { get; set; }

		[Outlet]
		UIKit.UIButton btnNee { get; set; }

		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgMore { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblEigenschap { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnJa != null) {
				btnJa.Dispose ();
				btnJa = null;
			}
			if (btnMore != null) {
				btnMore.Dispose ();
				btnMore = null;
			}
			if (btnNee != null) {
				btnNee.Dispose ();
				btnNee = null;
			}
			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}
			if (imgMore != null) {
				imgMore.Dispose ();
				imgMore = null;
			}
			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}
			if (lblEigenschap != null) {
				lblEigenschap.Dispose ();
				lblEigenschap = null;
			}
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
		}
	}
}
