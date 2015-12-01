using System;
using System.IO;
using System.Collections.Generic;

using SQLite;

using Android.Content;

namespace Totem
{
	public class Database
	{
		//DB shit
		static string dbName = "totems.sqlite";
		string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

		Context context;
		List<Eigenschap> eigenschappen;
		List<Totem> totems;

		public Database (Context context)
		{
			this.context = context;
			ExtractDB ();
			setEigenschappen ();
			setTotems ();
		}

		//DB inladen
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

		//Eigenschappen uit DB halen en in List steken
		private void setEigenschappen() {
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select * from eigenschap";
				eigenschappen = cmd.ExecuteQuery<Eigenschap> ();
			}
		}

		public List<Eigenschap> getEigenschappen() {
			return eigenschappen;
		}

		public List<Totem_eigenschap> getTotemsVanEigenschapsID(string id) {
			List<Totem_eigenschap> totemsVanEigenschap;
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select nid from totem_eigenschap where tid = " + id;
				totemsVanEigenschap = cmd.ExecuteQuery<Totem_eigenschap> ();
			}
			return totemsVanEigenschap;
		}

		private void setTotems() {
			using (var conn = new SQLite.SQLiteConnection (dbPath)) {
				var cmd = new SQLite.SQLiteCommand (conn);
				cmd.CommandText = "select * from totem";
				totems = cmd.ExecuteQuery<Totem> ();
			}
		}

		public int[] allTotemIDs() {
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

		public Totem getTotemOnID(int idx) {
			foreach(Totem t in totems) {
				if(t.nid.Equals(idx.ToString())) {
					return t;
				} 
			}
			return null;
		}

		public Totem getTotemOnID(string idx) {
			return getTotemOnID (Int32.Parse (idx));
		}

		public List<Totem> findTotemOpNaam(string name) {
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

