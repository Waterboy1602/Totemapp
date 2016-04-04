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
	[Register ("ProfielenViewController")]
	partial class ProfielenViewController
	{
		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}

			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}
		}
	}
}
