using SQLite;

namespace TotemAppCore {
	public class Totem {
		[PrimaryKey, AutoIncrement]
		public string Number { get; set; }
		public string Nid { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public string Synonyms { get; set; }
		public bool Selected { get; set; }
	}
}