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
	[Register ("ProfielTotemsViewController")]
	partial class ProfielTotemsViewController
	{
		[Outlet]
		UIKit.UIButton btnDelete { get; set; }

		[Outlet]
		UIKit.UIButton btnMore { get; set; }

		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgDelete { get; set; }

		[Outlet]
		UIKit.UIImageView imgMore { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UITableView tblTotems { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnDelete != null) {
				btnDelete.Dispose ();
				btnDelete = null;
			}

			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}

			if (imgDelete != null) {
				imgDelete.Dispose ();
				imgDelete = null;
			}

			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (tblTotems != null) {
				tblTotems.Dispose ();
				tblTotems = null;
			}

			if (btnMore != null) {
				btnMore.Dispose ();
				btnMore = null;
			}

			if (imgMore != null) {
				imgMore.Dispose ();
				imgMore = null;
			}
		}
	}
}
