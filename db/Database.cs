using System;
using System.Collections.Generic;
using System.IO;

using Android.Content;

using SQLite;

namespace Totem {
	public class Database {
		
		//DB parameters
		const string dbName = "totems.sqlite";
		readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);

		Context context;
		List<Eigenschap> eigenschappen;
		List<Totem> totems;

		public Database (Context context) {
			this.context = context;
			ExtractDB ();
			SetEigenschappen ();
			SetTotems ();
		}


		/* ------------------------------ INITIALIZE DB ------------------------------ */


		public void ExtractDB() {
			if (!File.Exists(dbPath)) {
				using (var br = new BinaryReader(context.Assets.Open(dbName))) {
					using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create))) {
						var buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
							bw.Write (buffer, 0, len);
					}
				}
			}
		}

		//extract eigenschappen from DB and put them in a list
		void SetEigenschappen() {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				cmd.CommandText = "select * from eigenschap_nieuw order by name";
				eigenschappen = cmd.ExecuteQuery<Eigenschap> ();
			}
		}

		//extract totems from DB and put them in a list
		void SetTotems() {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				cmd.CommandText = "select * from totem_nieuw order by title";
				totems = cmd.ExecuteQuery<Totem> ();
			}
		}


		/* ------------------------------ PROFIELEN ------------------------------ */


		//get list of profile-objects
		public List<Profiel> GetProfielen() {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				cmd.CommandText = "select distinct name from profiel";
				return cmd.ExecuteQuery<Profiel> ();
			}
		}

		//get list of profile names
		public List<string> GetProfielNamen() {
			var namen = new List<string> ();
			foreach (Profiel p in this.GetProfielen())
				namen.Add (p.name);
			return namen;
		}

		//add totem to profile in db
		public void AddTotemToProfiel(string totemID, string profielName) {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanProfielName = profielName.Replace("'", "");
				var cleanTotemID = totemID.Replace("'", "");
				cmd.CommandText = "insert into profiel (name, nid) select '" + cleanProfielName + "'," + cleanTotemID + 
					" WHERE NOT EXISTS ( SELECT * FROM profiel WHERE name='"+ cleanProfielName +"' AND nid=" + cleanTotemID + ");";
				cmd.ExecuteQuery<Profiel> ();
			}
		}

		//add a profile
		public void AddProfile(string name) {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanName = name.Replace("'", "");
				cmd.CommandText = "insert into profiel (name) values ('" + cleanName + "')";
				cmd.ExecuteQuery<Profiel> ();
			}
		}

		//delete a profile
		public void DeleteProfile(string name) {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanName = name.Replace("'", "");
				cmd.CommandText = "DELETE FROM profiel WHERE name='" + cleanName + "'";
				cmd.ExecuteQuery<Profiel> ();
			}
		}


		//delete a totem from a profile
		public void DeleteTotemFromProfile(string totemID, string profielName) {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanProfielName = profielName.Replace("'", "");
				var cleanTotemID = totemID.Replace("'", "");
				cmd.CommandText = "DELETE FROM profiel WHERE name='" + cleanProfielName + "' AND nid=" + cleanTotemID;
				cmd.ExecuteQuery<Profiel> ();
			}
		}

		//returns a list of totems related to a profile
		public List<Totem> GetTotemsFromProfiel(string name) {
			List<Profiel> list;
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanName = name.Replace("'", "");
				cmd.CommandText = "select nid from profiel where name='" + cleanName + "'";
				list = cmd.ExecuteQuery<Profiel> ();
			}
			var result = new List<Totem> ();
			foreach (Profiel p in list)
				if(p.nid != null) 
					result.Add (GetTotemOnID (p.nid));
			
			return result;
		}


		/* ------------------------------ TOTEMS EN EIGENSCHAPPEN ------------------------------ */


		public List<Eigenschap> GetEigenschappen() {
			return eigenschappen;
		}

		public List<Totem> GetTotems() {
			return totems;
		}

		//returns List of Totem_eigenschapp related to eigenschap id
		public List<Totem_eigenschap> GetTotemsVanEigenschapsID(string id) {
			List<Totem_eigenschap> totemsVanEigenschap;
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanId = id.Replace("'", "");
				cmd.CommandText = "select nid from totem_eigenschap_nieuw where tid = " + cleanId;
				totemsVanEigenschap = cmd.ExecuteQuery<Totem_eigenschap> ();
			}
			return totemsVanEigenschap;
		}

		//returns totem-object with given id
		public Totem GetTotemOnID(int idx) {
			foreach(Totem t in totems)
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
			foreach(Totem t in totems)
				if(t.title.ToLower().Contains(name.ToLower()))
					result.Add (t);
			
			return result;
		}

		//returns eigenschap-object with given name
		public List<Eigenschap> FindEigenschapOpNaam(string name) {
			var result = new List<Eigenschap> ();
			foreach(Eigenschap e in eigenschappen)
				if(e.name.ToLower().Contains(name.ToLower()))
					result.Add (e);
			
			return result;
		}


		/* ------------------------------ UTILS ------------------------------ */


		//returns Userpref-object based on parameter
		public Userpref GetPreference(string preference) {
			using (var conn = new SQLiteConnection (dbPath)) {
				List<Userpref> list;
				var cmd = new SQLiteCommand (conn);
				var cleanPreference = preference.Replace("'", "");
				cmd.CommandText = "select value from userprefs where preference='" + cleanPreference + "'";
				list = cmd.ExecuteQuery<Userpref> ();
				return list [0];
			}
		}

		//updates the preference with new value
		public void ChangePreference(string preference, string value) {
			using (var conn = new SQLiteConnection (dbPath)) {
				var cmd = new SQLiteCommand (conn);
				var cleanPreference = preference.Replace("'", "");
				var cleanValue = value.Replace("'", "");
				cmd.CommandText = "update userprefs set value='" + cleanValue + "' where preference='" + cleanPreference + "'";
				cmd.ExecuteQuery<Userpref> ();
			}
		}

		//returns random tip out of the database
		public string GetRandomTip() {
			List<Tip> list;
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select * from tip";
				list = cmd.ExecuteQuery<Tip> ();
			}
			var rnd = new Random ();
			return list [rnd.Next (list.Count)].tip;
		}
	}
}