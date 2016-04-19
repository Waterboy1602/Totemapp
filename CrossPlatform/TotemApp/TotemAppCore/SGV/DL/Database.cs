using System;
using System.Collections.Generic;
using System.IO;

#if __ANDROID__
using Android.App;
using Android.Content;
using Android.Preferences;
#endif

#if __IOS__
using Foundation;
#endif

using SQLite;

namespace TotemAppCore {
	public class Database {

		SQLiteConnection database;
		const int DATABASE_VERSION = 1;
		#if __ANDROID__
		string originalDBLocation = "totems.sqlite";
		#elif __IOS__
		string originalDBLocation = "SharedAssets/totems.sqlite";
		#endif

		string currentDBName = "totems.sqlite";

        //path for checking if database exists
        string DatabasePath { 
			get { 
				var sqliteFilename = currentDBName;

				#if __IOS__
				int SystemVersion = Convert.ToInt16(UIKit.UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
				string documentsPath;
				if(SystemVersion >= 8) {
					documentsPath = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path;
				} else {
					documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				}
				var path = Path.Combine(documentsPath, sqliteFilename);
                #else
                #if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
                #endif
                #endif
                return path;
			}
		}

		//reads the write stream.
		void ReadWriteStream(Stream readStream, Stream writeStream) {
			int Length = 256;
			var buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0) {
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}

		//initializes a new instance of the database
		//if the database doesn't exist, it will create the database and all the tables
		public Database() {
			var dbPath = DatabasePath;
			int dbVersion;

			#if __ANDROID__
			ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences (Application.Context);
			dbVersion = sharedPrefs.GetInt ("db_ver", 0);
			#elif __IOS__
			NSUserDefaults userDefs = NSUserDefaults.StandardUserDefaults;
			dbVersion = (int)userDefs.IntForKey ("db_ver");
			#endif

			if (!File.Exists (dbPath) || dbVersion != DATABASE_VERSION) {
				CreateDatabase (dbPath);
			}

			database = new SQLiteConnection (dbPath);
		}

		void CreateDatabase(string dbPath) {
			#if __ANDROID__
			var s = Application.Context.Assets.Open (originalDBLocation);
			var writeStream = new FileStream (dbPath, FileMode.OpenOrCreate, FileAccess.Write);
			ReadWriteStream (s, writeStream);
			writeStream.Close ();
			ISharedPreferences sharedPrefs = PreferenceManager.GetDefaultSharedPreferences (Application.Context);
			ISharedPreferencesEditor editor = sharedPrefs.Edit ();
			editor.PutInt ("db_ver", DATABASE_VERSION);

			#elif __IOS__
			var appDir = NSBundle.MainBundle.ResourcePath;
			var originalLocation = Path.Combine (appDir, originalDBLocation);
			File.Delete(dbPath);
			File.Copy (originalLocation, dbPath);
			NSUserDefaults userDefs = NSUserDefaults.StandardUserDefaults;
			userDefs.SetInt (DATABASE_VERSION, "db_ver");
			userDefs.Synchronize ();
			#endif
		}

		/* ------------------------------ INITIALIZE DB ------------------------------ */


		//extract eigenschappen from DB and put them in a list
		public List<Eigenschap> GetEigenschappen() {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				cmd.CommandText = "select * from eigenschap order by name";
				var eigenschappen = cmd.ExecuteQuery<Eigenschap> ();
				return eigenschappen;
			}
		}

		//extract totems from DB and put them in a list
		public List<Totem> GetTotems() {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				cmd.CommandText = "select * from totem order by title";
				var totems = cmd.ExecuteQuery<Totem> ();
				return totems;
			}
		}


		/* ------------------------------ PROFIELEN ------------------------------ */


		//get list of profile-objects
		public List<Profiel> GetProfielen(bool distinct = false) {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				if (distinct)
					cmd.CommandText = "select distinct name from profiel";
				else
					cmd.CommandText = "select * from profiel";
				return cmd.ExecuteQuery<Profiel> ();
			}
		}

		//add totem to profile in db
		public void AddTotemToProfiel(string totemID, string profielName) {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanProfielName = profielName.Replace("'", "");
				var cleanTotemID = totemID.Replace("'", "");
				cmd.CommandText = "insert into profiel (name, nid) select '" + cleanProfielName + "'," + cleanTotemID + 
					" WHERE NOT EXISTS ( SELECT * FROM profiel WHERE name='"+ cleanProfielName +"' AND nid=" + cleanTotemID + ");";
				cmd.ExecuteQuery<Profiel> ();
			}
		}

		//add a profile
		public void AddProfile(string name) {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanName = name.Replace("'", "");
				cmd.CommandText = "insert into profiel (name) values ('" + cleanName + "')";
				cmd.ExecuteQuery<Profiel> ();
			}
		}

		//delete a profile
		public void DeleteProfile(string name) {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanName = name.Replace("'", "");
				cmd.CommandText = "DELETE FROM profiel WHERE name='" + cleanName + "'";
				cmd.ExecuteQuery<Profiel> ();
			}
		}

		//delete a totem from a profile
		public void DeleteTotemFromProfile(string totemID, string profielName) {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanProfielName = profielName.Replace("'", "");
				var cleanTotemID = totemID.Replace("'", "");
				cmd.CommandText = "DELETE FROM profiel WHERE name='" + cleanProfielName + "' AND nid=" + cleanTotemID;
				cmd.ExecuteQuery<Profiel> ();
			}
		}


		/* ------------------------------ TOTEMS EN EIGENSCHAPPEN ------------------------------ */


		//returns List of Totem_eigenschapp related to eigenschap id
		public List<Totem_eigenschap> GetTotemsVanEigenschapsID(string id) {
			List<Totem_eigenschap> totemsVanEigenschap;
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanId = id.Replace("'", "");
				cmd.CommandText = "select nid from totem_eigenschap where tid = " + cleanId;
				totemsVanEigenschap = cmd.ExecuteQuery<Totem_eigenschap> ();
			}
			return totemsVanEigenschap;
		}

		//returns List of Totem_eigenschapp related to eigenschap id
		public Totem GetTotemsOnId(string id) {
			List<Totem> list;
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanId = id.Replace("'", "");
				cmd.CommandText = "select * from totem where nid = " + cleanId;
				list = cmd.ExecuteQuery<Totem> ();
			}
			return list [0];
		}


		/* ------------------------------ UTILS ------------------------------ */


		//returns Userpref-object based on parameter
		public Userpref GetPreference(string preference) {
			lock (database) {
				List<Userpref> list;
				var cmd = new SQLiteCommand (database);
				var cleanPreference = preference.Replace("'", "");
				cmd.CommandText = "select value from userprefs where preference='" + cleanPreference + "'";
				list = cmd.ExecuteQuery<Userpref> ();
				return list [0];
			}
		}

		//updates the preference with new value
		public void ChangePreference(string preference, string value) {
			lock (database) {
				var cmd = new SQLiteCommand (database);
				var cleanPreference = preference.Replace("'", "");
				var cleanValue = value.Replace("'", "");
				cmd.CommandText = "update userprefs set value='" + cleanValue + "' where preference='" + cleanPreference + "'";
				cmd.ExecuteQuery<Userpref> ();
			}
		}

		//returns random tip out of the database
		public string GetRandomTip() {
			List<Tip> list;
			lock (database) {
				var cmd = new SQLiteCommand (database);
				cmd.CommandText = "select * from tip";
				list = cmd.ExecuteQuery<Tip> ();
			}
			var rnd = new Random ();
			return list [rnd.Next (list.Count)].tip;
		}
	}
}