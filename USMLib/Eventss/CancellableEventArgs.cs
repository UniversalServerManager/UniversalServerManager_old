using System;
using System.Collections.Generic;
using System.Text;

namespace USMLib.Event
{
    public class CancellableEventArgs : EventArgs
    {
        public bool Cencelled { get; set; } = false;
        public CancellableEventArgs()
        {

        }
    }
    public class CancellableEventArgs<T> : EventArgs<T>
    {
        public bool Cencelled { get; set; } = false;
        public CancellableEventArgs(T arg) : base(arg)
        {

        }
    }
}
