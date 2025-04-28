using System;
using System.Collections.Generic;
using Systems.SaveSystem;
using UnityEngine;

namespace Core
{
    public class GameProgression : MonoBehaviour
    {
        public class OnProgressLoadedEventArgs : EventArgs
        {
            public SavableData Data;
        }

        private SavableData _data;
        
        public static EventHandler<OnProgressLoadedEventArgs> OnProgressLoaded;
        public static List<ISavable> DataToSave = new();

        private const string SAVE_FILE_NAME = "progress";

        private void Start()
        {
            Load();
        }

        public void Load()
        {
            _data = SaveSystem.LoadData<SavableData>(SAVE_FILE_NAME, true) as SavableData;
            
            OnProgressLoaded?.Invoke(this, new OnProgressLoadedEventArgs { Data = _data });
        }

        public void Save()
        {
            SaveSystem.SaveData(_data, SAVE_FILE_NAME, true);
        }

        private void OnApplicationQuit()
        {
            foreach (var requester in DataToSave)
            {
                List<(string, object)> dataToSave = requester.SaveData();
                foreach (var data in dataToSave)
                {
                    _data.SaveData(data.Item1, data.Item2);
                }
            }

            Save();
        }
    }
}
