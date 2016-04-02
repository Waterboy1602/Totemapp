using SQLite;

namespace TotemAppCore {
	public class Userpref {
		[PrimaryKey, AutoIncrement]
		public string preference { get; set; }
		public string value { get; set; }
	}
}