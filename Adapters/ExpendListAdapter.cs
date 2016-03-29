using System;
using System.Net;
using Android;
using Android.Widget;
using Android.App;
using Android.Views;
using Android.Content;
using Android.Content.Res;
using System.Collections.Generic;
using System.Collections;
using Android.Runtime;
using Android.OS;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Totem {
	public class ExpendListAdapter: BaseExpandableListAdapter {

		Dictionary<string, List<string>> _dictGroup =null;
		List<string> _lstGroupID = null;
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

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent) {
			var item = _dictGroup [_lstGroupID [groupPosition]] [childPosition];
			if (convertView == null) {
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandChild, parent, false);
			}

			var info = convertView.FindViewById<TextView> (Resource.Id.info);
			info.SetText (item, TextView.BufferType.Normal);

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

			if (convertView == null)
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ExpandParent, null);

			var head = convertView.FindViewById<TextView> (Resource.Id.head);
			head.SetText (item, TextView.BufferType.Normal);

			return convertView;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition) {
			return true;
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