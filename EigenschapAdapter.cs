using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;

namespace Totem
{
	public class EigenschapAdapter: BaseAdapter<Eigenschap>
	{
		Activity _activity;
		List<Eigenschap> eigenschapList;

		public EigenschapAdapter (Activity activity, List<Eigenschap> list)
		{	
			this._activity = activity;
			this.eigenschapList = list;
		}

		public override Eigenschap this[int index] {
			get {
				return eigenschapList [index];
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
			totem.Text = eigenschapList [position].name;
			return view;
		}

		public override int Count {
			get {
				return eigenschapList.Count;
			}
		}

		public Eigenschap GetItemAtPosition(int position)
		{
			return eigenschapList[position];
		}
	}
}

