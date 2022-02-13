using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using USMLib.Server;
using USMLib.User;
using YamlDotNet.Serialization;

namespace UniversialServerManager.MainServer
{
    public class MainServer : IMainServer
    {
        protected Dictionary<string, IUser> Users = new();
        Dictionary<string, IUser> IMainServer.Users => Users;

        public string IPAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Port { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WorkDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dictionary<string, IPartialServer> Particle => throw new NotImplementedException();

        public void LoadPlugin()
        {
        }
        
    }
}
