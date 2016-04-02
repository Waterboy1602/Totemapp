using System;

namespace TotemAppCore
{
	public class NavigationController
	{
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
		#endregion

		#region variables
		enum Pages 
		{
			MAIN,
			TOTEMLIST,
			TOTEMDETAIL,
			TOTEMRESULT,
			EIGENSCHAPLIST,
			PROFILELLIST,
			PROFILETOTEMLIST,
			CHECKLIST
		}
		#endregion

		#region constructor
		public NavigationController ()
		{
		}
		#endregion

		#region properties

		#endregion

		#region public methods

		public void GoToTotemDetail(){
			goToPage (Pages.TOTEMDETAIL);
		}
		public void GoToTotemList(){
			goToPage (Pages.TOTEMLIST);
		}
		#region overrided methods

		#region viewlifecycle

		#endregion

		#endregion

		#endregion

		#region private methods
		private void goToPage(Pages page){
			switch (page) {
			case Pages.TOTEMLIST:
				if (GotoTotemListEvent != null) {
					GotoTotemListEvent ();
				}
				break;
			default:
				break;
			}
		}
		#endregion

	}
}

