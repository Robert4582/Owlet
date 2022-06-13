using System;
using System.Collections.Generic;
using System.Text;

namespace Owlet.Exceptions
{
    public class NoBatchedLogException : Exception
    {
        private string logIndex;
        override public string Message
        {
            get
            {
                return $"Could not find batched Log: {logIndex}, was the log saved?";
            }
        }
        public NoBatchedLogException(Exception inner, string index)
        {
            this.logIndex = index;
        }
    }
}
