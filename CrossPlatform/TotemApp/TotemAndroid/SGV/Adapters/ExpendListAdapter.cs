using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace TotemAndroid {
	public class ExpendListAdapter: BaseExpandableListAdapter {
		Dictionary<string, List<string>> _dictGroup;
		List<string> _lstGroupID;
		Activity _activity;

		public ExpendListAdapter (Activity activity, Dictionary<string, List<string>> dictGroup)	{
			_dictGroup = dictGroup;
			_activity = activity;
			_lstGroupID = new List<string>(dictGroup.Keys);
		}

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition) {
			return _dictGroup [_lstGroupID [groupPosition]] [childPosition];
		}

		public override long GetChildId (int groupPosition, int childPosition) {
			return childPosition;
		}

		public override int GetChildrenCount (int groupPosition) {
			return _dictGroup [_lstGroupID [groupPosition]].Count;
		}

		//returns matching view for the type of data
		//i -> indented
		//h -> head
		//n -> normal
		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent) {
			var item = _dictGroup [_lstGroupID [groupPosition]] [childPosition];
			string[] data = item.Split ('_');
			var type = data [0];
			var content = data [1];
			TextView tv = null;
			if (type.Equals ("i")) {
				if(convertView == null || convertView.FindViewById<TextView> (Resource.Id.childindent) == null) 
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandChildIndent, parent, false);
				tv = convertView.FindViewById<TextView> (Resource.Id.childindent);
			} else if (type.Equals ("h")) {
				if(convertView == null || convertView.FindViewById<TextView> (Resource.Id.childhead) == null)
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandChildHead, parent, false);
				tv = convertView.FindViewById<TextView> (Resource.Id.childhead);
			} else if (type.Equals ("n")) {
				if(convertView == null || convertView.FindViewById<TextView> (Resource.Id.child) == null)
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandChild, parent, false);
				tv = convertView.FindViewById<TextView> (Resource.Id.child);
			}
				
			tv.SetText (content, TextView.BufferType.Normal);

			return convertView;
		}

		public override Java.Lang.Object GetGroup (int groupPosition){
			return _lstGroupID [groupPosition];
		}

		public override long GetGroupId (int groupPosition) {
			return groupPosition;
		}

		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent) {
			var item = _lstGroupID [groupPosition];

			convertView = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.ExpandParent, null);

			var head = convertView.FindViewById<TextView> (Resource.Id.head);
			head.SetText (item, TextView.BufferType.Normal);

			return convertView;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition) {
			return false;
		}

		public override int GroupCount {
			get {
				return _dictGroup.Count;
			}
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}
	}
}