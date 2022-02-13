using System;
using System.Collections.Generic;
using System.Text;
using USMLib.Server;

namespace USMLib.Server
{
    public interface IPartialServer
    {
        public Dictionary<string, IServerManager> ServerManagers { get; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string MainServerHost { get; set; }
    }
}
