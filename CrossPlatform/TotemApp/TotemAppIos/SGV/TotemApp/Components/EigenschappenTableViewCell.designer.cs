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
