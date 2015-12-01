using System;
using System.Collections.Generic;

namespace Totem
{
	public static class DictMethods
	{
		public static int[] getSortedTotemIDList(Dictionary<int, int> dict) {
			List<KeyValuePair<int, int>> tempList = new List<KeyValuePair<int, int>>(dict);

			tempList.Sort(delegate(KeyValuePair<int, int> firstPair, KeyValuePair<int, int> secondPair)
				{
					return firstPair.Value.CompareTo(secondPair.Value);
				}
			);

			Dictionary<int, int> mySortedDictionary = new Dictionary<int, int>();
			foreach(KeyValuePair<int, int> pair in tempList)
			{
				mySortedDictionary.Add(pair.Key, pair.Value);
			}

			int[] foos = new int[dict.Count];
			mySortedDictionary.Keys.CopyTo(foos, 0);

			//reverse order
			return foos;

		}

		public static int[] getSortedFreqList(Dictionary<int, int> dict) {
			List<KeyValuePair<int, int>> tempList = new List<KeyValuePair<int, int>>(dict);

			tempList.Sort(delegate(KeyValuePair<int, int> firstPair, KeyValuePair<int, int> secondPair)
				{
					return firstPair.Value.CompareTo(secondPair.Value);
				}
			);

			Dictionary<int, int> mySortedDictionary = new Dictionary<int, int>();
			foreach(KeyValuePair<int, int> pair in tempList)
			{
				mySortedDictionary.Add(pair.Key, pair.Value);
			}

			int[] foos = new int[dict.Count];
			mySortedDictionary.Values.CopyTo(foos, 0);

			return foos;

		}

		public static void AddOrUpdateDictionaryEntry(Dictionary<int, int> dict, int key)
		{
			if (dict.ContainsKey(key))
			{
				dict[key]++;
			}
			else
			{
				dict.Add(key, 1);
			}
		}
	}
}

