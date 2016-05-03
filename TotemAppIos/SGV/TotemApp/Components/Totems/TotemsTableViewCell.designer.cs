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
	[Register ("TotemsTableViewCell")]
	partial class TotemsTableViewCell
	{
		[Outlet]
		UIKit.UILabel lblFreq { get; set; }

		[Outlet]
		UIKit.UILabel lblTotemName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblFreq != null) {
				lblFreq.Dispose ();
				lblFreq = null;
			}
			if (lblTotemName != null) {
				lblTotemName.Dispose ();
				lblTotemName = null;
			}
		}
	}
}
