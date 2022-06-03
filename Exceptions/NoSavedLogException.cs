using System;
using System.Collections.Generic;
using System.Text;

namespace Owlet.Exceptions
{
    public class NoSavedLogException : Exception
    {
        private string logName;
        override public string Message
        {
            get
            {
                return $"Could not find saved Log: {logName}, was the log saved?";
            }
        }
        public NoSavedLogException(Exception inner, string logPath)
        {
            this.logName = logPath;
        }
    }
}
