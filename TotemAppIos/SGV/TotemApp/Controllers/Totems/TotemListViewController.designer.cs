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

		[Outlet]
		UIKit.UITextField txtSearch { get; set; }

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
			if (txtSearch != null) {
				txtSearch.Dispose ();
				txtSearch = null;
			}
		}
	}
}
