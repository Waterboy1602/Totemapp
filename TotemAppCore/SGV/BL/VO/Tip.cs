using SQLite;

namespace TotemAppCore {
	public class Tip {
		[PrimaryKey, AutoIncrement]
		public string TipBody { get; set; }
	}
}