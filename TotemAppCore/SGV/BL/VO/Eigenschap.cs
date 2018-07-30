using SQLite;

namespace TotemAppCore {
	public class Eigenschap {
		[PrimaryKey, AutoIncrement]
		[Column("tid")]
		public string EigenschapId { get; set; }
		public string Name { get; set; }
		public bool Selected { get; set; }
	}
}