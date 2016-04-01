using SQLite;

namespace Totem {
	public class Tip {
		[PrimaryKey, AutoIncrement]
		public string tip { get; set; }
	}
}