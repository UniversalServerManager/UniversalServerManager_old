using System;
using System.Collections.Generic;
using System.Text;
using USMLib.User;

namespace USMLib.Server
{
    public interface IMainServer
    {
        public Dictionary<string, IUser> Users { get; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string WorkDirectory { get; set; }
        public Dictionary<string, IPartialServer> Particle { get; }
    }
}
