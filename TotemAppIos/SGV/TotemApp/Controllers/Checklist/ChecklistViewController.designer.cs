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
	[Register ("ChecklistViewController")]
	partial class ChecklistViewController
	{
		[Outlet]
		UIKit.UIButton btnReset { get; set; }

		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgReset { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UILabel lblFoot { get; set; }

		[Outlet]
		UIKit.UILabel lblHead { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UITableView tblChecklist { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}

			if (imgReturn != null) {
				imgReturn.Dispose ();
				imgReturn = null;
			}

			if (lblFoot != null) {
				lblFoot.Dispose ();
				lblFoot = null;
			}

			if (lblHead != null) {
				lblHead.Dispose ();
				lblHead = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (tblChecklist != null) {
				tblChecklist.Dispose ();
				tblChecklist = null;
			}

			if (imgReset != null) {
				imgReset.Dispose ();
				imgReset = null;
			}

			if (btnReset != null) {
				btnReset.Dispose ();
				btnReset = null;
			}
		}
	}
}
