using System;
using System.Collections.Generic;
using System.Text;

namespace USMLib.Event
{
    public class EventArgs
    {
        public EventArgs()
        {

        }
    }
    public class EventArgs<T> : EventArgs
    {
        public T arg { protected set; get; }
        public EventArgs(T arg)
        {
            this.arg = arg;
        }
    }
}
