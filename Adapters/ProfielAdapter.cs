using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;

namespace Totem
{
	public class ProfielAdapter: BaseAdapter<Profiel>
	{
		Activity _activity;
		List<Profiel> profielList;

		public ProfielAdapter (Activity activity, List<Profiel> list)
		{	
			this._activity = activity;
			this.profielList = list;
		}

		public override Profiel this[int index] {
			get {
				return profielList [index];
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.TotemListItem, parent, false);
			var totem = view.FindViewById<TextView> (Resource.Id.totem);
			totem.Text = profielList[position].name;

			return view;
		}

		public override int Count {
			get {
				return profielList.Count;
			}
		}

		public Profiel GetItemAtPosition(int position)
		{
			return profielList[position];
		}
	}
}

