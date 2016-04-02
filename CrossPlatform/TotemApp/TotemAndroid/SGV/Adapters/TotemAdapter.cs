using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using TotemAppCore;

namespace TotemAndroid {
	public class TotemAdapter: BaseAdapter<Totem> {
		Activity _activity;
		List<Totem> totemList;
		int[] freqs;
		int selected;
		bool showDelete;

		public TotemAdapter (Activity activity, List<Totem> list) {	
			_activity = activity;
			totemList = list;
			showDelete = false;
		}

		public TotemAdapter (Activity activity, List<Totem> list, int[] freqs, int selected): this(activity, list) {	
			this.freqs = freqs;
			this.selected = selected;
		}

		public void ShowDelete() {
			this.showDelete = true;
		}

		public void HideDelete() {
			this.showDelete = false;
		}

		public void UpdateData(List<Totem> list) {
			this.totemList = list;
		}

		public override Totem this[int index] {
			get {
				return totemList [index];
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
				viewHolder.totem = convertView.FindViewById<TextView> (Resource.Id.totem);
				viewHolder.checkbox = convertView.FindViewById<CheckBox> (Resource.Id.deleteItem);
				viewHolder.freq = convertView.FindViewById<TextView> (Resource.Id.freq);

				convertView.Tag = viewHolder;
			} else {
				viewHolder = (ViewHolder)convertView.Tag;
			}

			viewHolder.checkbox.Visibility = showDelete ? ViewStates.Visible : ViewStates.Gone;

			viewHolder.checkbox.Tag = position;

			viewHolder.totem.Text = totemList [position].title;
			viewHolder.checkbox.Checked = totemList [(int)viewHolder.checkbox.Tag].selected;
			if (freqs != null)
				viewHolder.freq.Text = freqs [position].ToString () /*+ "/" + totemList [position].numberOfEigenschappen*/;

			viewHolder.checkbox.Click += (o, e) => {
				totemList [(int)viewHolder.checkbox.Tag].selected = viewHolder.checkbox.Checked;
			};

			return convertView;
		}

		//ViewHolder for better performance
		class ViewHolder : Java.Lang.Object {
			public TextView totem;
			public CheckBox checkbox;
			public TextView freq;
		}

		public override int Count {
			get {
				return totemList.Count;
			}
		}

		public Totem GetItemAtPosition(int position) {
			return totemList[position];
		}
	}
}