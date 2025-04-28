using System;
using System.IO;
using System.Text;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Systems.SaveSystem
{
    public static class SaveSystem
    {
        private static readonly string keyword = "Incremental-Toolkit";

        public static object LoadData<T>(string fileName, bool encrypt = true)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".json";

            if (File.Exists(path))
            {
                try
                {
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

                    string json = File.ReadAllText(path);
                    if (encrypt)
                        json = EncryptDecrypt(json);
                    T data = JsonConvert.DeserializeObject<T>(json, settings);
                    return data;
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load data from " + path + " with exception: " + e);
                    return null;
                }
            }
            else
            {
                Debug.LogWarning("Save file not found at " + path);
                return null;
            }
        }

        public static void SaveData(object data, string fileName, bool encrypt = true)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".json";

            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
                if (encrypt)
                    json = EncryptDecrypt(json);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to save data to " + path + " with exception: " + e);
            }
        }

        private static string EncryptDecrypt(string data)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                result.Append((char)(data[i] ^ keyword[i % keyword.Length]));
            }

            return result.ToString();
        }

        public static void DeleteData(string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}