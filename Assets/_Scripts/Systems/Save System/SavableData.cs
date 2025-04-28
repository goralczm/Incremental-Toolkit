using System.Collections.Generic;

namespace Systems.SaveSystem
{
    [System.Serializable]
    public class SavableData
    {
        public Dictionary<string, object> SavedData { get; private set; } = new();

        public void SaveData(string key, object obj)
        {
            if (!SavedData.ContainsKey(key))
            {
                SavedData.Add(key, obj);
                return;
            }

            SavedData[key] = obj;
        }

        public object GetData(string key)
        {
            return !SavedData.ContainsKey(key) ? null : SavedData[key];
        }

        public bool TryGetData(string key, out object data)
        {
            return SavedData.TryGetValue(key, out data);
        }
    }
}
