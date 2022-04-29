using System;
using System.Collections.Generic;
using System.Text;

namespace Owlet.Interfaces
{
    public interface INetworkWriter
    {
        public void Send<T>(T data);
        public void Send<T>(IEnumerable<T> data);
    }
}
