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
	[Register ("TotemListViewController")]
	partial class TotemListViewController
	{
		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIButton btnSearch { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgSearch { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UITableView tblTotems { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}

			if (btnSearch != null) {
				btnSearch.Dispose ();
				btnSearch = null;
			}

			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}

			if (imgSearch != null) {
				imgSearch.Dispose ();
				imgSearch = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (tblTotems != null) {
				tblTotems.Dispose ();
				tblTotems = null;
			}
		}
	}
}
