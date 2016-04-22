using SQLite;

namespace TotemAppCore {
	public class Eigenschap {
		[PrimaryKey, AutoIncrement]
		[Column("tid")]
		public string eigenschapID { get; set; }
		public string name { get; set; }
		public bool selected { get; set; }
	}
}