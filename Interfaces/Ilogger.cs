using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Owlet.Interfaces
{
    public interface ILogger<TStorage, TStorageData> where TStorage : IEnumerable<TStorageData>
    {
        public bool AutoSend { get; set; }
        public int CurrentBatchSize { get; }
        public int MaxBatchSize { get; set; }
        TStorage Batched { get; set; }
        public INetworkWriter Writer { get; set; }

        public void Log<TData>(TData data) where TData : TStorageData;
        public void Log<TData>(IEnumerable<TData> data) where TData : TStorageData;

        public void SendImmediate<TData>(TData data) where TData : TStorageData;
        public void SendImmediate<TData>(TStorage data) where TData : TStorageData;

        public void Send();
    }
}
