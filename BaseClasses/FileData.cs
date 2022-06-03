using Owlet.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Owlet.BaseClasses
{
    public class FileData
    {
        public string FilePath{ get; set; }
        Dictionary<string, List<string>> data;

        private string serializedData;
        public string Serialized => serializedData;

        private bool changed;

        public FileData(string filePath)
        {
            FilePath = filePath;
        }

        public void WriteToData(string dataName, string inputData)
        {
            InitData(dataName);
            data[dataName].Add(inputData);
            changed = true;
        }

        private void InitData(string dataName)
        {
            if (data == null)
            {
                data = new Dictionary<string, List<string>>();
            }
            if (!data.ContainsKey(dataName))
            {
                data.Add(dataName, new List<string>());
            }
        }
        public void SaveFile()
        {
            serializedData = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(FilePath, Serialized);
        }
        public void DeleteFile()
        {
            File.Delete(FilePath);
        }

        public string ReadText()
        {
            try
            {
                return File.ReadAllText(FilePath);
            }
            catch (Exception e)
            {
                throw new NoSavedLogException(e, FilePath);
            }
            return null;
        }

        public void SetDictionary()
        {
            data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,List<string>>>(ReadText());
        }
    }
}
