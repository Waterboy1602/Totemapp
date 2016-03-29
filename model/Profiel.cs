using System;

using SQLite;

namespace Totem {
	public class Profiel {
		[PrimaryKey, AutoIncrement]
		public string pid { get; set; }
		public string name { get; set; }
		public string nid { get; set; }
		public bool selected { get; set; }
	}
}