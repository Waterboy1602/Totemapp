using System.Collections.Generic;
using System.Linq;

namespace TotemAppCore {

	//static helper class for operations on Dictionaries and other collections
	public static class CollectionHelper {

		//return sorted array of keys or values from dictionary
		public static Dictionary<Totem, int> GetSortedList(Dictionary<Totem, int> dict) {
			var tempList = new List<KeyValuePair<Totem, int>>(dict);

			tempList.Sort ((firstPair, secondPair) => secondPair.Value.CompareTo (firstPair.Value));

			var mySortedDictionary = new Dictionary<Totem, int>();
			foreach(KeyValuePair<Totem, int> pair in tempList)
				mySortedDictionary.Add(pair.Key, pair.Value);

			return mySortedDictionary; 
		}

		//adds entry to dictionary if it doesn't exist
		//updates it if it does
		public static void AddOrUpdateDictionaryEntry(Dictionary<Totem, int> dict, Totem key) {
			var k = dict.Keys.ToList().Find(x=>x.nid == key.nid);
			if (k != null)
				dict[k]++;
			else 
				dict.Add(key, 1);
		}
	}
}