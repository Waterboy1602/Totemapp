using SQLite;

namespace TotemAppCore {
	public class Tip {
		[PrimaryKey, AutoIncrement]
		public string tip { get; set; }
	}
}