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

		//keeps track of which eigenschappen are checked
		Dictionary<string, bool> checkList;

		public EigenschapAdapter (Activity activity, List<Eigenschap> list, Dictionary<string, bool> checkList)
		{	
			this._activity = activity;
			this.eigenschapList = list;
			this.checkList = checkList;
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
			var view = _activity.LayoutInflater.Inflate (Resource.Layout.EigenschapListItem, parent, false);
			var eigenschap = view.FindViewById<TextView> (Resource.Id.eigenschap);
			var checkbox = view.FindViewById<CheckBox> (Resource.Id.checkbox);
			eigenschap.Text = eigenschapList [position].name;
			checkbox.Checked = checkList [eigenschapList [position].tid];

			checkbox.Click += (o, e) => {
				if (checkbox.Checked)
					checkList [eigenschapList [position].tid] = true;
				else
					checkList [eigenschapList [position].tid] = false;;
			};

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

