using System;
using System.Collections.Generic;
using System.IO;
using HTTPServerLib;

namespace HttpServer
{
    public class HttpServer4USM : ExampleServer
    {
        public HttpServer4USM(string ipAddress, int port) : base(ipAddress, port)
        {
        }
        public override void OnGet(HttpRequest request, HttpResponse response)
        {
            string requestURL = request.URL.Replace("/..", "");
            if (requestURL.StartsWith("/api/"))
            {
                // TODO
            }
            if (request.URL.StartsWith("/servers/"))
            {

            }
            if (request.URL.StartsWith("/users/"))
            {

            }
        }
    }
}
