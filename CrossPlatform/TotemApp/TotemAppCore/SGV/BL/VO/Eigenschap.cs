using SQLite;

namespace TotemAppCore {
	public class Eigenschap {
		[PrimaryKey, AutoIncrement]
		[Column("tid")]
		public string eigenschapID { get; set; }
		public string name { get; set; }
		/*public string selectedString { get; set; }
		public bool selected { 
			get {
				return selectedString.Equals ("true");
			}
			set {
				if (value)
					selectedString = "true";
				else
					selectedString = "false";
			}
		}*/
		public bool selected { get; set; }
	}
}