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
	[Register ("ProfielenTableViewCell")]
	partial class ProfielenTableViewCell
	{
		[Outlet]
		UIKit.UILabel lblProfile { get; set; }

		[Outlet]
		UIKit.UIView vwCheckbox { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblProfile != null) {
				lblProfile.Dispose ();
				lblProfile = null;
			}
			if (vwCheckbox != null) {
				vwCheckbox.Dispose ();
				vwCheckbox = null;
			}
		}
	}
}
