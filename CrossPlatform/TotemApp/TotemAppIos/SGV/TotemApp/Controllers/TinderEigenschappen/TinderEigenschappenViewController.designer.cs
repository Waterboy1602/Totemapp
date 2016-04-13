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
	[Register ("TinderEigenschappenViewController")]
	partial class TinderEigenschappenViewController
	{
		[Outlet]
		UIKit.UIButton btnJa { get; set; }

		[Outlet]
		UIKit.UIButton btnMore { get; set; }

		[Outlet]
		UIKit.UIButton btnNee { get; set; }

		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgMore { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblEigenschap { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblEigenschap != null) {
				lblEigenschap.Dispose ();
				lblEigenschap = null;
			}

			if (btnJa != null) {
				btnJa.Dispose ();
				btnJa = null;
			}

			if (btnNee != null) {
				btnNee.Dispose ();
				btnNee = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}

			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}

			if (imgMore != null) {
				imgMore.Dispose ();
				imgMore = null;
			}

			if (btnMore != null) {
				btnMore.Dispose ();
				btnMore = null;
			}
		}
	}
}
