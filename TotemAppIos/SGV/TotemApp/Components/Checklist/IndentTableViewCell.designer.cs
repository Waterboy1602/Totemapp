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
	[Register ("IndentTableViewCell")]
	partial class IndentTableViewCell
	{
		[Outlet]
		UIKit.UIImageView imgBullet { get; set; }

		[Outlet]
		UIKit.UILabel lblBulletPoint { get; set; }

		[Outlet]
		UIKit.UILabel lblIndent { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint paddingHeightTop { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint paddingHeigthBottom { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgBullet != null) {
				imgBullet.Dispose ();
				imgBullet = null;
			}

			if (lblBulletPoint != null) {
				lblBulletPoint.Dispose ();
				lblBulletPoint = null;
			}

			if (lblIndent != null) {
				lblIndent.Dispose ();
				lblIndent = null;
			}

			if (paddingHeightTop != null) {
				paddingHeightTop.Dispose ();
				paddingHeightTop = null;
			}

			if (paddingHeigthBottom != null) {
				paddingHeigthBottom.Dispose ();
				paddingHeigthBottom = null;
			}
		}
	}
}
