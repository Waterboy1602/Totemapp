using System;

using SQLite;

namespace Totem
{
	public class Totem_eigenschap
	{
		[PrimaryKey, AutoIncrement]
		public string tid { get; set; }
		public string nid { get; set; }
	}
}

