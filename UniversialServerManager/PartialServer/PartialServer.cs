using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Fleck;
using HttpServer;
using UniversalServerManager.User;
using UniversalServerManager;
using USMLib.Server;
using USMLib.User;

namespace UniversialServerManager.PartialServer
{
    public class PartialServer : IPartialServer
    {
        Configuration configuration;
        WebSocketServer WebSocketServer;
        public HttpServer4USM HttpServer4USM { get; set; }

        public Dictionary<string, IServerManager> ServerManagers = new();
        public Dictionary<IWebSocketConnection, User> connections = new();

        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string MainServerHost { get; set; }
        Dictionary<string, IServerManager> IPartialServer.ServerManagers => ServerManagers;
        public PartialServer()
        {
            WebSocketServer = new($"ws://127.0.0.1:{configuration.port}/w/s");
        }
        public static void ConnectionHandler(IWebSocketConnection connection)
        {
            connection.OnOpen = new Action(() =>
              {

              });
            connection.OnMessage = new Action<string>(data =>
              {

              });
        }
        protected Permission CheckPermissionRemotely(string server)
        {
            HttpClient httpClient = new();
            httpClient.GetStringAsync(MainServerHost + "api/users/verify");
            return Permission.None;
        }
    }
}
