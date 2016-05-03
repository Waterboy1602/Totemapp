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
	[Register ("ProfielTotemsTableViewCell")]
	partial class ProfielTotemsTableViewCell
	{
		[Outlet]
		UIKit.UILabel lblTotemName { get; set; }

		[Outlet]
		UIKit.UIView vwCheckbox { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblTotemName != null) {
				lblTotemName.Dispose ();
				lblTotemName = null;
			}
			if (vwCheckbox != null) {
				vwCheckbox.Dispose ();
				vwCheckbox = null;
			}
		}
	}
}
