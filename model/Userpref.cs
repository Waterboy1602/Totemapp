using System;

using SQLite;

namespace Totem
{
	public class Userpref
	{
		[PrimaryKey, AutoIncrement]
		public string preference { get; set; }
		public string value { get; set; }
	}
}

