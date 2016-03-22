﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Graphics;

namespace Totem {
	public class EigenschapAdapter: BaseAdapter<Eigenschap> {
		Activity _activity;
		List<Eigenschap> eigenschapList;

		OnCheckBoxClickListener mListener;

		public EigenschapAdapter (Activity activity, List<Eigenschap> list, OnCheckBoxClickListener listener) {	
			this._activity = activity;
			this.eigenschapList = list;
			this.mListener = listener;
		}

		public override Eigenschap this[int index] {
			get {
				return eigenschapList [index];
			}
		}

		public override long GetItemId (int position) {
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent) {
			ViewHolder viewHolder;

			if (convertView == null) {
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.EigenschapListItem, parent, false);

				viewHolder = new ViewHolder ();
				viewHolder.eigenschap = convertView.FindViewById<TextView> (Resource.Id.eigenschap);
				viewHolder.checkbox = convertView.FindViewById<CheckBox> (Resource.Id.checkbox);

				convertView.Tag = viewHolder;
			} else {
				viewHolder = (ViewHolder)convertView.Tag;
			}

			//IMPORTANT: keeps track of which checkboxes are checked
			//and which aren't during scrolling
			viewHolder.checkbox.Tag = position;

			viewHolder.eigenschap.Text = eigenschapList [position].name;
			bool itemChecked = eigenschapList [(int)viewHolder.checkbox.Tag].selected;
			viewHolder.checkbox.Checked = itemChecked;

			//notifies CheckBoxListener and stores selection
			viewHolder.checkbox.Click += (o, e) => {
				mListener.OnCheckboxClicked ();
				if (viewHolder.checkbox.Checked) {
					eigenschapList [(int)viewHolder.checkbox.Tag].selected = true;
				} else {
					eigenschapList [(int)viewHolder.checkbox.Tag].selected = false;
				}
			};

			//when the row item is clicked, it also checks or unchecks the box
			//notifies CheckBoxListener and stores selection
			/*convertView.Click += (o, e) => {
				var temp = viewHolder.checkbox.Checked;
				viewHolder.checkbox.Checked = !(temp);
				mListener.OnCheckboxClicked ();
				if (viewHolder.checkbox.Checked) {
					eigenschapList [(int)viewHolder.checkbox.Tag].selected = true;
				} else {
					eigenschapList [(int)viewHolder.checkbox.Tag].selected = false;
				}

				//viewHolder.checkbox.PerformClick();
			};*/

			return convertView;
		}

		//ViewHolder for better performance
		class ViewHolder : Java.Lang.Object {
			public TextView eigenschap;
			public CheckBox checkbox;
		}

		public override int Count {
			get {
				return eigenschapList.Count;
			}
		}

		public Eigenschap GetItemAtPosition(int position) {
			return eigenschapList[position];
		}
	}
}