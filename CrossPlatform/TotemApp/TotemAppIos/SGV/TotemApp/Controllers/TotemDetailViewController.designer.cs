// WARNING
//
// This file has been generated automatically by Xamarin Studio Community to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

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

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (lblNumber != null) {
				lblNumber.Dispose ();
				lblNumber = null;
			}
		}
	}
}
