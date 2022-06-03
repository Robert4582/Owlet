using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Owlet.Interfaces
{
    public interface ILogger<TStorage, TStorageData> where TStorage : IEnumerable<TStorageData>
    {
        public bool AutoSend { get; set; }
        public string FilePath { get; }
        public int CurrentBatchSize { get; }
        public int MaxBatchSize { get; set; }
        TStorage Batched { get; set; }
        public INetworkWriter Writer { get; }

        public void Log<TData>(string dataName, TData data);
        public void Log<TData>(string dataName, IEnumerable<TData> data);
        public void SaveLog();

        public void SendImmediate<TData>(string dataName, TData data);
        public void SendImmediate<TData>(string dataName, TStorage data);

        public void Send();
        public void DeleteLog();
    }
}
