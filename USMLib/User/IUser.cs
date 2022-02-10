using System;
using System.Collections.Generic;
using System.Text;

namespace USMLib.User
{
    public interface IUser
    {
        public string Name { get; set; }
        public bool CheckPasswordOrKey(string password);
        public string UpdateApiKey();
        public string GetApiKey();
        public void RemoveApiKey();
        public Permission GetPermission(string serverName);
        public string Password { set; }
        public bool Online { get; set; }
    }
}
