using Owlet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Owlet.BaseClasses
{
    public class BaseLogger : ILogger<List<object>, object>
    {
        public bool AutoSend { get; set; } = true;

        public int CurrentBatchSize => Batched.Count();

        public int MaxBatchSize { get; set; } = 512;
        public List<object> Batched { get; set; } = new List<object>();
        public INetworkWriter Writer { get; set; }

        public void Log<T>(T data)
        {
            Batched.Add(data);
        }

        public void Log<TData>(List<object> data)
        {
            Batched.AddRange(data.Select(t => t as object));
        }

        public void SendImmediate<T>(T data)
        {
            Writer.Send(data);
        }

        public void SendImmediate<TData>(List<object> data)
        {
            Writer.Send(data);
        }

        public void Send()
        {
            Writer.Send(Batched);
        }

        public void Log<TData>(IEnumerable<TData> data)
        {
            throw new NotImplementedException();
        }
    }
}
