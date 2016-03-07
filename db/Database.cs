using System;
using System.IO;
using System.Collections.Generic;

using SQLite;

using Android.Content;

namespace Totem
{
	public class Database
	{
		//DB params
		static string dbName = "totems.sqlite";
		string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

		Context context;
		List<Eigenschap> eigenschappen;
		List<Totem> totems;

		public Database (Context context)
		{
			this.context = context;
			ExtractDB ();
			SetEigenschappen ();
			SetTotems ();
		}

		//load DB
		public void ExtractDB() {
			if (!File.Exists(dbPath))
			{
				using (BinaryReader br = new BinaryReader(context.Assets.Open(dbName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write (buffer, 0, len);
						}
					}
				}
			}
		}
			
		//extract eigenschappen from DB and put them in a list
		private void SetEigenschappen() {
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select * from eigenschap";
				eigenschappen = cmd.ExecuteQuery<Eigenschap> ();
			}
		}

		public List<Eigenschap> GetEigenschappen() {
			return eigenschappen;
		}

		//returns List of Totem_eigenschapp related to eigenschap id
		public List<Totem_eigenschap> GetTotemsVanEigenschapsID(string id) {
			List<Totem_eigenschap> totemsVanEigenschap;
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select nid from totem_eigenschap where tid = " + id;
				totemsVanEigenschap = cmd.ExecuteQuery<Totem_eigenschap> ();
			}
			return totemsVanEigenschap;
		}

		//extract totems from DB and put them in a list
		private void SetTotems() {
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select * from totem";
				totems = cmd.ExecuteQuery<Totem> ();
			}
		}

		//returns array of all totem IDs
		public int[] AllTotemIDs() {
			List<Totem> list = totems;
			list.Reverse ();
			int[] result = new int[395];
			int index = 0;
			foreach(Totem t in list) {
				result.SetValue(Int32.Parse(t.nid), index);
				index++;
			}
			return result;
		}

		//returns totem-object with given id
		public Totem GetTotemOnID(int idx) {
			foreach(Totem t in totems) {
				if(t.nid.Equals(idx.ToString())) {
					return t;
				} 
			}
			return null;
		}

		//returns totem-object with given id
		public Totem GetTotemOnID(string idx) {
			return GetTotemOnID (Int32.Parse (idx));
		}

		//returns totem-object with given name
		public List<Totem> FindTotemOpNaam(string name) {
			List<Totem> result = new List<Totem> ();
			foreach(Totem t in totems) {
				if(t.title.ToLower().Contains(name.ToLower())) {
					result.Add (t);
				}
			}
			return result;
		}
	}
}

