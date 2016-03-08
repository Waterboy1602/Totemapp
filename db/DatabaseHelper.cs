using System;

using SQLite;
using Android.Database.Sqlite;
using Android.Content;

namespace Totem
{
	public static class DatabaseHelper
	{
		private static Database dbInstance = null;

		public static Database GetInstance(Context context) {
			if (dbInstance == null) 
				dbInstance = new Database (context);

			return dbInstance;
		}
	}
}

