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
	[Register ("TotemDetailViewController")]
	partial class TotemDetailViewController
	{
		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgLine { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblBody { get; set; }

		[Outlet]
		UIKit.UILabel lblHead { get; set; }

		[Outlet]
		UIKit.UILabel lblNumber { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}
			if (imgLine != null) {
				imgLine.Dispose ();
				imgLine = null;
			}
			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}
			if (lblBody != null) {
				lblBody.Dispose ();
				lblBody = null;
			}
			if (lblHead != null) {
				lblHead.Dispose ();
				lblHead = null;
			}
			if (lblNumber != null) {
				lblNumber.Dispose ();
				lblNumber = null;
			}
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
		}
	}
}
