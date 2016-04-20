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
	[Register ("NormalTableViewCell")]
	partial class NormalTableViewCell
	{
		[Outlet]
		UIKit.UIImageView imgBullet { get; set; }

		[Outlet]
		UIKit.UILabel lblBulletPoint { get; set; }

		[Outlet]
		UIKit.UILabel lblNormal { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint paddingHeight { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint paddingHeigthTop { get; set; }
		
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

			if (imgBullet != null) {
				imgBullet.Dispose ();
				imgBullet = null;
			}

			if (paddingHeight != null) {
				paddingHeight.Dispose ();
				paddingHeight = null;
			}

			if (paddingHeigthTop != null) {
				paddingHeigthTop.Dispose ();
				paddingHeigthTop = null;
			}
		}
	}
}
