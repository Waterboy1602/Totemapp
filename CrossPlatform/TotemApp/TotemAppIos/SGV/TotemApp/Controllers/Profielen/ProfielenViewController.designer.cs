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
