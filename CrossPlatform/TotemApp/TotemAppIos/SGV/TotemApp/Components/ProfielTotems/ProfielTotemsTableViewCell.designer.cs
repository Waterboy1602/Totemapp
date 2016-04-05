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
