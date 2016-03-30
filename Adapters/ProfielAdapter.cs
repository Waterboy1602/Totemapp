using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Totem {
	public class ProfielAdapter: BaseAdapter<Profiel> {
		Activity _activity;
		List<Profiel> profielList;
		bool showDelete;

		public ProfielAdapter (Activity activity, List<Profiel> list) {	
			this._activity = activity;
			this.profielList = list;
			this.showDelete = false;
		}

		public void UpdateData(List<Profiel> list) {
			this.profielList = list;
		}

		public void ShowDelete() {
			this.showDelete = true;
		}

		public void HideDelete() {
			this.showDelete = false;
		}

		public override Profiel this[int index] {
			get {
				return profielList [index];
			}
		}

		public override long GetItemId (int position) {
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent) {
			ViewHolder viewHolder;

			if (convertView == null) {
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.TotemListItem, parent, false);

				viewHolder = new ViewHolder ();
				viewHolder.profiel = convertView.FindViewById<TextView> (Resource.Id.totem);
				viewHolder.checkbox = convertView.FindViewById<CheckBox> (Resource.Id.deleteItem);

				convertView.Tag = viewHolder;
			} else {
				viewHolder = (ViewHolder)convertView.Tag;
			}

			if (showDelete)
				viewHolder.checkbox.Visibility = ViewStates.Visible;
			else
				viewHolder.checkbox.Visibility = ViewStates.Gone;

			viewHolder.checkbox.Tag = position;

			viewHolder.profiel.Text = profielList [position].name;
			viewHolder.checkbox.Checked = profielList [(int)viewHolder.checkbox.Tag].selected;

			viewHolder.checkbox.Click += (o, e) => {
				if (viewHolder.checkbox.Checked)
					profielList [(int)viewHolder.checkbox.Tag].selected = true;
				else
					profielList [(int)viewHolder.checkbox.Tag].selected = false;
			};

			return convertView;
		}

		//ViewHolder for better performance
		class ViewHolder : Java.Lang.Object {
			public TextView profiel;
			public CheckBox checkbox;
		}

		public override int Count {
			get {
				return profielList.Count;
			}
		}

		public Profiel GetItemAtPosition(int position) {
			return profielList[position];
		}
	}
}