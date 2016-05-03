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
	[Register ("EigenschappenTableViewCell")]
	partial class EigenschappenTableViewCell
	{
		[Outlet]
		UIKit.UILabel lblEigenschapName { get; set; }

		[Outlet]
		UIKit.UIView vwCheckBoxHolder { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblEigenschapName != null) {
				lblEigenschapName.Dispose ();
				lblEigenschapName = null;
			}
			if (vwCheckBoxHolder != null) {
				vwCheckBoxHolder.Dispose ();
				vwCheckBoxHolder = null;
			}
		}
	}
}
