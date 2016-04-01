using SQLite;

namespace Totem {
	public class Eigenschap {
		[PrimaryKey, AutoIncrement]
		public string tid { get; set; }
		public string name { get; set; }
		public bool selected { get; set; }
	}
}