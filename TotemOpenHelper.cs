using System;
using Android.Database.Sqlite;
using Android.Content;

namespace Totem
{
	public class TotemOpenHelper : SQLiteOpenHelper
	{
		public TotemOpenHelper (Context context)
		{
			//constr args (context, db naam, cursorfactory(?), db versie)
			base(context, "totems", null, 2);
		}

		public override void OnCreate(SQLiteDatabase db) {
			db.ExecSQL()
		}
	}
}

