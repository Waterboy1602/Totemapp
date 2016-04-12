using System;

using UIKit;
using TotemAppCore;
using CoreGraphics;

namespace TotemAppIos {
	public partial class TotemListViewController : UIViewController {
		#region delegates

		#endregion

		#region variables
		AppController _appController = AppController.Instance;
		bool isSearching;
		#endregion

		#region constructor
		public TotemListViewController () : base ("TotemListViewController", null) {}
		#endregion

		#region properties

		#endregion

		#region public methods

		#region overrided methods
		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
		}

		public override UIStatusBarStyle PreferredStatusBarStyle () {
			return UIStatusBarStyle.LightContent;
		}

		#region viewlifecycle
		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			setData ();
			NavigationController.NavigationBarHidden = true;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

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

		#endregion

		#endregion

		#endregion

		#region private methods

		private void setData() {
			lblTitle.Text = "Totems";

			imgReturn.Image = UIImage.FromBundle ("SharedAssets/arrow_back_white");
			imgSearch.Image = UIImage.FromBundle ("SharedAssets/search_white");
			txtSearch.Hidden=true;
			txtSearch.TintColor = UIColor.White;
			txtSearch.ReturnKeyType = UIReturnKeyType.Search;
			txtSearch.ShouldReturn = ((UITextField textfield) => {
				textfield.ResignFirstResponder ();
				return true;
			});

			tblTotems.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;

			UIColor color = UIColor.White;
			txtSearch.AttributedPlaceholder = new Foundation.NSAttributedString("Zoek totem",foregroundColor: color);

			tblTotems.SectionIndexColor = UIColor.FromRGB (0, 92, 157);
			tblTotems.Source = new TotemsTableViewSource (_appController.Totems);
			tblTotems.TableFooterView = new UIView ();
		}

		void btnReturnTouchUpInside (object sender, EventArgs e) {
			NavigationController.PopViewController (true);
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

		void TxtSearchValueChangedHandler (object sender, EventArgs e) {
			(tblTotems.Source as TotemsTableViewSource).Totems = _appController.FindTotemOpNaam ((sender as UITextField).Text);
			tblTotems.ReloadData ();
			tblTotems.ScrollRectToVisible (new CGRect(0,0,1,1), false);
		}

		void gotoTotemDetailHandler() {
			NavigationController.PushViewController (new TotemDetailViewController(), true);
		}
		#endregion
	}
}