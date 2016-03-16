using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.Res;

namespace Totem {
	public class TotemAdapter: BaseAdapter<Totem> {
		Activity _activity;
		List<Totem> totemList;
		int[] freqs;

		public TotemAdapter (Activity activity, List<Totem> list) {	
			this._activity = activity;
			this.totemList = list;
		}

		public TotemAdapter (Activity activity, List<Totem> list, int[] freqs): this(activity, list) {	
			this.freqs = freqs;
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
			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.TotemListItem, parent, false);
			var totem = view.FindViewById<TextView> (Resource.Id.totem);
			totem.Text = totemList[position].title;
			if (freqs != null) {
				var freq = view.FindViewById<TextView> (Resource.Id.freq);
				freq.Text = freqs [position].ToString ();
			}

			return view;
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