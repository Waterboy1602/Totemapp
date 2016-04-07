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
	[Register ("NormalTableViewCell")]
	partial class NormalTableViewCell
	{
		[Outlet]
		UIKit.UILabel lblBulletPoint { get; set; }

		[Outlet]
		UIKit.UILabel lblNormal { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblBulletPoint != null) {
				lblBulletPoint.Dispose ();
				lblBulletPoint = null;
			}
			if (lblNormal != null) {
				lblNormal.Dispose ();
				lblNormal = null;
			}
		}
	}
}
