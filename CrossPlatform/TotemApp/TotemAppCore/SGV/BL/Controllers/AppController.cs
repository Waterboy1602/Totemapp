using System;
using System.Collections.Generic;

namespace TotemAppCore
{
	public class AppController
	{

		#region delegates
		public delegate void updateLabelDelegate(int timesClicked);
		public event updateLabelDelegate updateLabelEvent;
		#endregion

		#region variables
		static readonly AppController _instance = new AppController();
		Database database = DatabaseHelper.GetInstance ();

		List<Totem> _totems;
		List<Eigenschap> _eigenschappen;
		List<Profiel> _profielen;

		TotemController _totemController;
		NavigationController _navigationController;
		#endregion

		#region constructor
		public AppController ()
		{
			_totems = database.GetTotems ();
			_eigenschappen = database.GetEigenschappen ();
			_totemController = new TotemController ();
			_navigationController = new NavigationController ();
		}

		#endregion

		#region properties
		public static AppController Instance {
			get {
				return _instance;
			}
		}

		public TotemController TotemController {
			get {
				return this._totemController;
			}
		}

		public NavigationController NavigationController {
			get {
				return this._navigationController;
			}
		}

		public List<Totem> Totems {
			get {
				return this._totems;
			}
		}

		public List<Eigenschap> Eigenschappen {
			get {
				return this._eigenschappen;
			}
		}

		public List<Profiel> Profielen {
			get {
				_profielen = database.GetProfielen ();
				return this._profielen;
			}
		}

		#endregion

		#region public methods

		//returns totem-object with given id
		public Totem GetTotemOnID(int idx) {
			foreach(Totem t in _totems)
				if(t.nid.Equals(idx.ToString()))
					return t;

			return null;
		}

		//returns totem-object with given id
		public Totem GetTotemOnID(string idx) {
			return GetTotemOnID (Int32.Parse (idx));
		}

		//returns totem-object with given name
		public List<Totem> FindTotemOpNaam(string name) {
			var result = new List<Totem> ();
			foreach(Totem t in _totems)
				if(t.title.ToLower().Contains(name.ToLower()))
					result.Add (t);

			return result;
		}

		//returns eigenschap-object with given name
		public List<Eigenschap> FindEigenschapOpNaam(string name) {
			var result = new List<Eigenschap> ();
			foreach(Eigenschap e in _eigenschappen)
				if(e.name.ToLower().Contains(name.ToLower()))
					result.Add (e);

			return result;
		}
		//returns a list of totems related to a profile
		public List<Totem> GetTotemsFromProfiel(string name) {
			List<Profiel> list = Profielen.FindAll (x=>x.name == name);
			var profiles = list.FindAll (y =>y.nid!=null);
			List<Totem> result = new List<Totem> ();
			foreach (var profile in profiles) {
				result.Add (GetTotemOnID (profile.nid));
			}
			return result;
		}

		//get list of profile names
		public List<string> GetProfielNamen() {
			var namen = new List<string> ();
			foreach (Profiel p in Profielen)
				namen.Add (p.name);
			return namen;
		}

		public void totemSelected(string totemID){
			setCurrentTotem (totemID);
			_navigationController.GoToTotemDetail ();
		}

		public void TotemMenuItemClicked(){
			_navigationController.GoToTotemList ();
		}

		#region overrided methods

		#region viewlifecycle

		#endregion

		#endregion

		#endregion

		#region private methods
		void setCurrentTotem(string totemID){
			var totem = _totems.Find (x => x.nid.Equals (totemID));
			_totemController.CurrentTotem = totem;
		}
		#endregion



	}
}

