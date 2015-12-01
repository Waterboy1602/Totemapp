using System;

using SQLite;

namespace Totem
{
	public class Totem
	{
		[PrimaryKey, AutoIncrement]
		public string nid { get; set; }
		public string title { get; set; }
		public string body { get; set; }
		public string synonyms { get; set; }
	}
}

