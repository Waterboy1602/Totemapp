using System.Collections.Generic;

namespace TotemAndroid {

	//static helper class for operations on Dictionaries and other collections
	public static class CollectionHelper {

		//return sorted array of keys or values from dictionary
		public static int[] GetSortedList(Dictionary<int, int> dict, bool keys) {
			var tempList = new List<KeyValuePair<int, int>>(dict);

			tempList.Sort ((firstPair, secondPair) => firstPair.Value.CompareTo (secondPair.Value));

			var mySortedDictionary = new Dictionary<int, int>();
			foreach(KeyValuePair<int, int> pair in tempList)
				mySortedDictionary.Add(pair.Key, pair.Value);

			//reverse order
			var foos = new int[dict.Count];
			if (keys)
				mySortedDictionary.Keys.CopyTo (foos, 0);
			else
				mySortedDictionary.Values.CopyTo(foos, 0);

			ReverseArray (foos);

			return foos;
		}

		//adds entry to dictionary if it doesn't exist
		//updates it if it does
		public static void AddOrUpdateDictionaryEntry(Dictionary<int, int> dict, int key) {
			if (dict.ContainsKey(key))
				dict[key]++;
			else 
				dict.Add(key, 1);
		}

		//helper method to reverse an array
		private static void ReverseArray(int [] arr) {
			for (int i = 0; i < arr.Length / 2; i++) {
				int tmp = arr[i];
				arr[i] = arr[arr.Length - i - 1];
				arr[arr.Length - i - 1] = tmp;
			}
		}
	}
}