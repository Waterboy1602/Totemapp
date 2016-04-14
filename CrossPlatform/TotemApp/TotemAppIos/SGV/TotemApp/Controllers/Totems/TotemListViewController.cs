using System;

using CoreGraphics;
using UIKit;
using TotemAppCore;

namespace TotemAppIos {
	public partial class TotemListViewController : BaseViewController {

		bool isSearching;

		public TotemListViewController () : base ("TotemListViewController", null) {}

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
			btnReturn.TouchUpInside += btnReturnTouchUpInside;
			btnSearch.TouchUpInside += btnSearchTouchUpInside;
			txtSearch.EditingChanged += TxtSearchValueChangedHandler;
			_appController.NavigationController.GotoTotemDetailEvent += gotoTotemDetailHandler;
		}
			
		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			btnReturn.TouchUpInside -= btnReturnTouchUpInside;
			btnSearch.TouchUpInside -= btnSearchTouchUpInside;
			txtSearch.EditingChanged -= TxtSearchValueChangedHandler;
			_appController.NavigationController.GotoTotemDetailEvent -= gotoTotemDetailHandler;
		}

		public override void setData() {
			lblTitle.Text = "Totems";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			imgSearch.Image = UIImage.FromBundle ("SharedAssets/search_white");

			_appController.detailMode = AppController.DetailMode.NORMAL;

			//search field is initially hidden
			txtSearch.Hidden=true;
			txtSearch.TintColor = UIColor.White;
			txtSearch.ReturnKeyType = UIReturnKeyType.Search;
			txtSearch.ShouldReturn = (textfield => {
				textfield.ResignFirstResponder ();
				return true;
			});

			//hide keyboard when tapped outside it
			tblTotems.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;

			UIColor color = UIColor.White;
			txtSearch.AttributedPlaceholder = new Foundation.NSAttributedString("Zoek totem",foregroundColor: color);

			//color of index letters on the right
			tblTotems.SectionIndexColor = UIColor.FromRGB (0, 92, 157);

			tblTotems.Source = new TotemsTableViewSource (_appController.Totems);

			//empty view at footer to prevent empty cells at the bottom
			tblTotems.TableFooterView = new UIView ();
		}

		void btnSearchTouchUpInside (object sender, EventArgs e) {
			if (isSearching) {
				txtSearch.Hidden = true;
				btnReturn.Hidden = false;
				lblTitle.Hidden = false;
				txtSearch.Text = "";
				TxtSearchValueChangedHandler (txtSearch,null);
				txtSearch.ResignFirstResponder ();
				imgSearch.Image = UIImage.FromBundle ("SharedAssets/search_white");
			} else {
				txtSearch.Hidden = false;
				btnReturn.Hidden = true;
				lblTitle.Hidden = true;
				imgSearch.Image = UIImage.FromBundle ("SharedAssets/close_white");
				txtSearch.BecomeFirstResponder ();
			}
			isSearching = !isSearching;
		}

		//updates list to match entered query
		void TxtSearchValueChangedHandler (object sender, EventArgs e) {
			(tblTotems.Source as TotemsTableViewSource).Totems = _appController.FindTotemOpNaam ((sender as UITextField).Text);
			tblTotems.ReloadData ();
			tblTotems.ScrollRectToVisible (new CGRect(0,0,1,1), false);
		}

		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}
	}
}