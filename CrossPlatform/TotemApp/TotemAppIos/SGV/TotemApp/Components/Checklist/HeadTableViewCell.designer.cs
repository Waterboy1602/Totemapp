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
	[Register ("HeadTableViewCell")]
	partial class HeadTableViewCell
	{
		[Outlet]
		UIKit.NSLayoutConstraint heightPadding { get; set; }

		[Outlet]
		UIKit.UILabel lblHead { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (heightPadding != null) {
				heightPadding.Dispose ();
				heightPadding = null;
			}

			if (lblHead != null) {
				lblHead.Dispose ();
				lblHead = null;
			}
		}
	}
}
