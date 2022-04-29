using Owlet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Owlet.BaseClasses
{
    public class BaseNetworkWriter : INetworkWriter
    {
        //Host some networking stuff here
        public void Send<T>(T data)
        {
            throw new NotImplementedException();
        }

        public void Send<T>(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }
    }
}
