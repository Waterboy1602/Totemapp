using System;
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

		//keeps track of which eigenschappen are checked
		Dictionary<string, bool> checkList;

		OnCheckBoxClickListener mListener;

		Typeface Din;

		public EigenschapAdapter (Activity activity, List<Eigenschap> list, Dictionary<string, bool> checkList, OnCheckBoxClickListener listener, Context context) {	
			this._activity = activity;
			this.eigenschapList = list;
			this.checkList = checkList;
			this.mListener = listener;
			Din = Typeface.CreateFromAsset(context.Assets,"fonts/DINPro-Regular.ttf");
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
			var view = _activity.LayoutInflater.Inflate (Resource.Layout.EigenschapListItem, parent, false);
			var eigenschap = view.FindViewById<TextView> (Resource.Id.eigenschap);
			eigenschap.SetTypeface (Din, 0);
			var checkbox = view.FindViewById<CheckBox> (Resource.Id.checkbox);
			eigenschap.Text = eigenschapList [position].name;
			checkbox.Checked = checkList [eigenschapList [position].tid];

			//notifies CheckBoxListener and updates checklist
			checkbox.Click += (o, e) => {
				mListener.OnCheckboxClicked ();
				if (checkbox.Checked) {
					checkList [eigenschapList [position].tid] = true;
				} else {
					checkList [eigenschapList [position].tid] = false;
				}
			};

			//when the row item is clicked, it also checks or unchecks the box
			//notifies CheckBoxListener and updates checklist
			view.Click += (o, e) => {
				checkbox.Checked = !(checkbox.Checked);
				mListener.OnCheckboxClicked ();
				if (checkbox.Checked) {
					checkList [eigenschapList [position].tid] = true;
				} else {
					checkList [eigenschapList [position].tid] = false;
				}
			};

			return view;
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