using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UniversialServerManager;
using USMLib.User;
using Fleck;

namespace UniversalServerManager.User
{
    public class User : IUser
    {
        protected string passwordHash;
        protected string apiKey;
        public string Name { get; set; }
        public string Password { set => passwordHash = Sha256(value); }
        public bool Online { get; set; }
        public Dictionary<string, Permission> permissionMap;
        public bool admin;
        public IWebSocketConnection connection;

        public static string Sha256(string str) => BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(str))).Replace("-", "");

        public bool CheckPasswordOrKey(string password)
        {
            if (Sha256(password) == passwordHash)
                return true;
            else if (apiKey != null && password == apiKey)
                return true;
            return false;
        }

        public string GetApiKey()
        {
            return apiKey;
        }

        public Permission GetPermission(string serverName)
        {
            if (admin) return Permission.Any;
            else if (permissionMap.ContainsKey(serverName)) return permissionMap[serverName];
            else return Permission.None;
        }

        public void RemoveApiKey()
        {
            apiKey = null;
        }

        public string UpdateApiKey()
        {
            // 40 chars
            // 30 bytes -> base64
            byte[] vs = new byte[30];
            Program.rnd.NextBytes(vs);
            apiKey = Convert.ToBase64String(vs);
            return apiKey;
        }
    }
}
