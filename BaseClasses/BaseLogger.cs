using Owlet.Exceptions;
using Owlet.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Owlet.BaseClasses
{
    public class BaseLogger : ILogger<List<FileData>, FileData>
    {
        public bool AutoSend { get; set; }

        public int CurrentBatchSize => 256;

        public string SessionID;

        public int MaxBatchSize { get; set; } = 512;
        public List<FileData> Batched { get; set; } = new List<FileData>();
        public FileData NewestFile
        {
            get
            {
                if (Batched.Count == 0)
                {
                    throw new NoBatchedLogException(null, 0.ToString());
                }
                return Batched[Batched.Count - 1];
            }
        }

        private BaseNetworkWriter writer;

        public INetworkWriter Writer { get => writer; }
        private string filePath;
        public string FilePath => filePath;

        private string completePath;

        private static BaseLogger instance;
        private BaseLogger(string ip = "127.0.0.1", string filePath = "")
        {
            writer = new BaseNetworkWriter(ip);
            GetOrCreateFolder(filePath);

            foreach (var item in Directory.GetFiles(completePath))
            {
                Batched.Add(new FileData(Path.Combine(completePath, item)));

            }
            Batched.Add(new FileData(Path.Combine(completePath, Guid.NewGuid().ToString())));
        }

        private void GetOrCreateFolder(string filePath)
        {
            if (filePath == "")
            {
                this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            string solutionName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).Split('.')[0];

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(FilePath, solutionName);

            // Check if folder exists and if not, create it
            completePath = Directory.CreateDirectory(specificFolder).ToString();
        }

        public static BaseLogger GetInstance(string ip = "127.0.0.1", string filePath = "")
        {
            if (instance == null)
                instance = new BaseLogger(ip, filePath);
            return instance;
        }

        public static void ClearLogger()
        {
            instance.DeleteAllLogs();
            instance = null;
        }

        public static void ClearData()
        {
            instance.DeleteLog();
        }

        public void Log<TData>(string dataName, TData data)
        {
            var stringData = System.Text.Json.JsonSerializer.Serialize(data);
            Batched[Batched.Count - 1].WriteToData(dataName, stringData);
        }

        public void Log<TData>(string dataName, IEnumerable<TData> data)
        {
            foreach (var item in data)
            {
                Log(dataName, item);
            }
        }
        public void SaveLog()
        {
            Batched[Batched.Count - 1].SaveFile();
        }

        public void DeleteLog()
        {
            DeleteLog(Batched.Count - 1);
        }

        public void DeleteLog(int index)
        {
            Batched[index].DeleteFile();
            Batched.RemoveAt(index);
        }

        public void DeleteAllLogs()
        {
            while (Batched.Count > 0)
            {
                DeleteLog(Batched.Count -1);
            }
        }

        public void SendImmediate<TData>(string dataName, TData data)
        {
            throw new NotImplementedException();
        }

        public void SendImmediate<TData>(string dataName, List<FileData> data)
        {
            throw new NotImplementedException();
        }

        public void Send()
        {
            NewestFile.SaveFile();
            Writer.Send(NewestFile.ReadText());
        }
    }
}
