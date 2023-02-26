using hundun.unitygame.gamelib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace hundun.unitygame.adapters
{
    public class UnitySaveTool<T_SAVE> : ISaveTool<T_SAVE>
    {
        const string fileName = "save.json";

        public bool hasRootSave()
        {
            return File.Exists(GetFilePath(fileName));
        }

        public void lazyInitOnGameCreate()
        {
            // do nothing
        }

        public T_SAVE readRootSaveData()
        {
            string json = ReadFromFIle(fileName);
            T_SAVE data = JsonConvert.DeserializeObject<T_SAVE>(json);
            return data;
        }

        public void writeRootSaveData(T_SAVE saveData)
        {
            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            WriteToFile(fileName, json);
        }

        private void WriteToFile(string fileName, string json)
        {
            string path = GetFilePath(fileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        private string ReadFromFIle(string fileName)
        {
            string path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEnd();
                    Debug.Log("ReadFromFIle Success by path: " + path);
                    return json;
                }
            }
            else
            {
                Debug.LogWarning("File not found");
            }

            return "ReadFromFIle fail: " + path;
        }

        private string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }
}
