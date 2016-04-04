using System;

namespace TotemAppCore {
	public class NavigationController {
		#region delegates
		public delegate void GoToPageDelegate();
		public event GoToPageDelegate GotoMainEvent;
		public event GoToPageDelegate GotoTotemListEvent;
		public event GoToPageDelegate GotoTotemDetailEvent;
		public event GoToPageDelegate GotoTotemResultEvent;
		public event GoToPageDelegate GotoEigenschapListEvent;
		public event GoToPageDelegate GotoProfileListEvent;
		public event GoToPageDelegate GotoProfileTotemListEvent;
		public event GoToPageDelegate GotoChecklistEvent;
		public event GoToPageDelegate GotoTinderEvent;
		#endregion

		#region variables
		enum Pages {
			MAIN,
			TOTEMLIST,
			TOTEMDETAIL,
			TOTEMRESULT,
			EIGENSCHAPLIST,
			PROFILELLIST,
			PROFILETOTEMLIST,
			CHECKLIST,
			TINDER
		}
		#endregion

		#region constructor
		public NavigationController () {}
		#endregion

		#region properties

		#endregion

		#region public methods

		public void GoToTotemList() {
			goToPage (Pages.TOTEMLIST);
		}

		public void GoToTotemDetail() {
			goToPage (Pages.TOTEMDETAIL);
		}

		public void GoToTotemResult() {
			goToPage (Pages.TOTEMRESULT);
		}

		public void GoToEigenschapList() {
			goToPage (Pages.EIGENSCHAPLIST);
		}

		public void GoToProfileList() {
			goToPage (Pages.PROFILELLIST);
		}

		public void GoToProfileTotemList() {
			goToPage (Pages.PROFILETOTEMLIST);
		}

		public void GoToChecklist() {
			goToPage (Pages.CHECKLIST);
		}

		public void GoToTinder() {
			goToPage (Pages.TINDER);
		}

		#region overrided methods

		#region viewlifecycle

		#endregion

		#endregion

		#endregion

		#region private methods
		private void goToPage(Pages page) {
			switch (page) {
			case Pages.TOTEMLIST:
				if (GotoTotemListEvent != null)
					GotoTotemListEvent ();
				break;
			case Pages.TOTEMDETAIL:
				if (GotoTotemDetailEvent != null)
					GotoTotemDetailEvent ();
				break;
			case Pages.TOTEMRESULT:
				if (GotoTotemResultEvent != null)
					GotoTotemResultEvent ();
				break;
			case Pages.EIGENSCHAPLIST:
				if (GotoEigenschapListEvent != null)
					GotoEigenschapListEvent ();
				break;
			case Pages.PROFILELLIST:
				if (GotoProfileListEvent != null)
					GotoProfileListEvent ();
				break;
			case Pages.PROFILETOTEMLIST:
				if (GotoProfileTotemListEvent != null)
					GotoProfileTotemListEvent ();
				break;
			case Pages.CHECKLIST:
				if (GotoChecklistEvent != null)
					GotoChecklistEvent ();
				break;
			case Pages.TINDER:
				if (GotoTinderEvent != null)
					GotoTinderEvent ();
				break;
			default:
				break;
			}
		}
		#endregion
	}
}