using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalServerManager
{
    public class Configuration
    {
        public bool secure = false;
        public int port = 2334;
    }
    public class ServerManagerConfiguration
    {
        public string startCommand;
        public string stopCommand;
    }
}
