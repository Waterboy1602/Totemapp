using Android.App;
using Android.Views;
using Android.Widget;

using System.Collections.Generic;

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
		//e.g.: i_het past in de context
		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent) {

			//extract type of view and content
			var item = _dictGroup [_lstGroupID [groupPosition]] [childPosition];
			string[] data = item.Split ('_');
			var type = data [0];
			var content = data [1];
			TextView tv = null;

			//indented view
			if (type.Equals ("i")) {
				if(convertView == null || convertView.FindViewById<TextView> (Resource.Id.childindent) == null) 
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandChildIndent, parent, false);
				tv = convertView.FindViewById<TextView> (Resource.Id.childindent);
			//header view
			} else if (type.Equals ("h")) {
				if(convertView == null || convertView.FindViewById<TextView> (Resource.Id.childhead) == null)
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandChildHead, parent, false);
				tv = convertView.FindViewById<TextView> (Resource.Id.childhead);
			//normal view
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