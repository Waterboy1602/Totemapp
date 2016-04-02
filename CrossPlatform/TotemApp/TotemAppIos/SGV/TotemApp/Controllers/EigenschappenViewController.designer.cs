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
	[Register ("EigenschappenViewController")]
	partial class EigenschappenViewController
	{
		[Outlet]
		UIKit.UIButton btnMore { get; set; }

		[Outlet]
		UIKit.UIButton btnReturn { get; set; }

		[Outlet]
		UIKit.UIButton btnSearch { get; set; }

		[Outlet]
		UIKit.UIImageView imgMore { get; set; }

		[Outlet]
		UIKit.UIImageView imgReturn { get; set; }

		[Outlet]
		UIKit.UIImageView imgSearch { get; set; }

		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UITableView tblEigenschappen { get; set; }

		[Outlet]
		UIKit.UITextField txtSearch { get; set; }

		[Outlet]
		UIKit.UIView vwSearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnMore != null) {
				btnMore.Dispose ();
				btnMore = null;
			}

			if (btnReturn != null) {
				btnReturn.Dispose ();
				btnReturn = null;
			}

			if (btnSearch != null) {
				btnSearch.Dispose ();
				btnSearch = null;
			}

			if (imgMore != null) {
				imgMore.Dispose ();
				imgMore = null;
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

			if (tblEigenschappen != null) {
				tblEigenschappen.Dispose ();
				tblEigenschappen = null;
			}

			if (txtSearch != null) {
				txtSearch.Dispose ();
				txtSearch = null;
			}

			if (vwSearchBar != null) {
				vwSearchBar.Dispose ();
				vwSearchBar = null;
			}
		}
	}
}
