using System;

namespace TotemAppCore
{
	public class TotemController
	{
		#region delegates

		#endregion

		#region variables
		Totem _currentTotem;
		#endregion

		#region constructor
		public TotemController ()
		{
		}
		#endregion

		#region properties
		public Totem CurrentTotem {
			get {
				return this._currentTotem;
			}
			set {
				_currentTotem = value;
			}
		}
		#endregion

		#region public methods

		#region overrided methods

		#region viewlifecycle

		#endregion

		#endregion

		#endregion

		#region private methods

		#endregion

	}
}

