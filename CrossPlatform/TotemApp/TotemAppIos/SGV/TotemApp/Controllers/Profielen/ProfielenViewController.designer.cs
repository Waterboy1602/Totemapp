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
	[Register ("ProfielenViewController")]
	partial class ProfielenViewController
	{
		[Outlet]
		UIKit.UIButton btnAdd { get; set; }

		[Outlet]
		UIKit.UIButton btnDelete { get; set; }

		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgAdd { get; set; }

		[Outlet]
		UIKit.UIImageView imgDelete { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblEmpty { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UITableView tblProfielen { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAdd != null) {
				btnAdd.Dispose ();
				btnAdd = null;
			}
			if (btnDelete != null) {
				btnDelete.Dispose ();
				btnDelete = null;
			}
			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}
			if (imgAdd != null) {
				imgAdd.Dispose ();
				imgAdd = null;
			}
			if (imgDelete != null) {
				imgDelete.Dispose ();
				imgDelete = null;
			}
			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}
			if (lblEmpty != null) {
				lblEmpty.Dispose ();
				lblEmpty = null;
			}
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
			if (tblProfielen != null) {
				tblProfielen.Dispose ();
				tblProfielen = null;
			}
		}
	}
}
