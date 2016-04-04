using System;
using System.Collections.Generic;
using System.Linq;

namespace TotemAppCore {
	public class AppController {

		#region delegates

		#endregion

		#region variables
		static readonly AppController _instance = new AppController();
		readonly Database database = DatabaseHelper.GetInstance ();

		List<Totem> _totems;
		List<Eigenschap> _eigenschappen;
		List<Profiel> _profielen;

		NavigationController _navigationController;

		#endregion

		#region constructor
		public AppController () {
			_totems = database.GetTotems ();
			_eigenschappen = database.GetEigenschappen ();
			_navigationController = new NavigationController ();
			TotemEigenschapDict = new Dictionary<Totem, int> ();
		}

		#endregion

		#region properties
		public static AppController Instance {
			get {
				return _instance;
			}
		}

		public NavigationController NavigationController {
			get {
				return _navigationController;
			}
		}

		public List<Totem> Totems {
			get {
				return _totems;
			}
		}

		public List<Eigenschap> Eigenschappen {
			get {
				return _eigenschappen;
			}
		}

		public List<Profiel> AllProfielen {
			get {
				_profielen = database.GetProfielen ();
				return _profielen;
			}
		}

		public List<Profiel> DistinctProfielen{
			get {
				_profielen = database.GetProfielen (true);
				return _profielen;
			}
		}

		public Totem CurrentTotem { get; set; }

		public Profiel CurrentProfiel { get; set; }

		public Dictionary<Totem, int> TotemEigenschapDict { get; set; }

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
			List<Profiel> list = AllProfielen.FindAll (x=>x.name == name);
			var profiles = list.FindAll (y =>y.nid!=null);
			var result = new List<Totem> ();
			foreach (var profile in profiles) {
				result.Add (GetTotemOnID (profile.nid));
			}
			return result;
		}

		public void AddTotemToProfiel(string totemID, string profielName) {
			database.AddTotemToProfiel (totemID,profielName);
		}

		public void AddProfile(string name){
			database.AddProfile (name);
		}

		public void DeleteProfile(string name) {
			database.DeleteProfile (name);
		}

		public void DeleteTotemFromProfile(string totemID, string profielName) {
			database.DeleteTotemFromProfile (totemID,profielName);
		}

		public List<Totem_eigenschap> GetTotemsVanEigenschapsID(string id) {
			return database.GetTotemsVanEigenschapsID (id);
		}

		public Userpref GetPreference(string preference) {
			return database.GetPreference (preference);
		}

		public void ChangePreference(string preference, string value) {
			database.ChangePreference (preference,value);
		}

		public string GetRandomTip() {
			return database.GetRandomTip ();
		}
		//get list of profile names
		public List<string> GetProfielNamen() {
			var namen = new List<string> ();
			foreach (Profiel p in DistinctProfielen)
				namen.Add (p.name);
			return namen;
		}

		public void TotemMenuItemClicked() {
			_navigationController.GoToTotemList ();
		}

		public void EigenschappenMenuItemClicked() {
			_navigationController.GoToEigenschapList ();
		}

		public void ProfileMenuItemClicked() {
			_navigationController.GoToProfileList ();
		}

		public void ChecklistMenuItemClicked() {
			_navigationController.GoToChecklist ();
		}

		public void TinderMenuItemClicked() {
			_navigationController.GoToTinder ();
		}

		public void TotemSelected(string totemID) {
			setCurrentProfile (null);
			setCurrentTotem (totemID);
			_navigationController.GoToTotemDetail ();
		}

		public void ProfileSelected(string profileName) {
			setCurrentProfile (profileName);
			_navigationController.GoToProfileTotemList ();
		}

		public void ProfileTotemSelected(string profileName, string totemID) {
			setCurrentProfile (profileName);
			setCurrentTotem (totemID);
			_navigationController.GoToTotemDetail ();
		}

		public void CalculateResultlist(List<Eigenschap> checkboxList) {
			FillAndSortDict (checkboxList);
			_navigationController.GoToTotemResult ();
		}

		#region overrided methods

		#region viewlifecycle

		#endregion

		#endregion

		#endregion

		#region private methods
		void setCurrentTotem(string totemID) {
			var totem = _totems.Find (x => x.nid.Equals (totemID));
			CurrentTotem = totem;
		}

		void setCurrentProfile(string profileName) {
			if (profileName == null) {
				CurrentProfiel = null;
			} else {
				var profiel = _profielen.Find (x => x.name.Equals (profileName));
				CurrentProfiel = profiel;
			}
		}

		void FillAndSortDict(List<Eigenschap> checkboxList) {
			foreach (Eigenschap eig in checkboxList) {
				if(eig.selected) {
					List<Totem_eigenschap> toAdd = GetTotemsVanEigenschapsID (eig.eigenschapID);
					foreach(Totem_eigenschap totem in toAdd) {
						CollectionHelper.AddOrUpdateDictionaryEntry (TotemEigenschapDict, database.GetTotemsOnId(totem.nid));
					}
				}
			}
			TotemEigenschapDict = CollectionHelper.GetSortedList (TotemEigenschapDict);
		}
		#endregion
	}
}