using System.IO;
using UnityEngine;

namespace TrapMaster
{
    public sealed class JsonFileSaveStorage : ISaveStorage
    {
        private readonly string filePath;

        public JsonFileSaveStorage()
        {
            filePath = Path.Combine(Application.persistentDataPath, "save.json");
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }

        public SaveData Load()
        {
            if (!Exists())
                return new SaveData();

            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data ?? new SaveData();
        }

        public bool Exists()
        {
            return File.Exists(filePath);
        }
    }
}