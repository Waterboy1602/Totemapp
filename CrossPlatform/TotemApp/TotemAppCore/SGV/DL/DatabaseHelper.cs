namespace TotemAppCore {

	//Database singleton
	public static class DatabaseHelper {

		static Database dbInstance;

		public static Database GetInstance() {
			if (dbInstance == null) 
				dbInstance = new Database ();

			return dbInstance;
		}
	}
}