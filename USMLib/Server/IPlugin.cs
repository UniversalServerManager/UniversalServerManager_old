using System;
using System.Collections.Generic;
using System.Text;

namespace USMLib.Server
{
    public interface IPlugin
    {
        public void OnLoad();
        public void OnEnable();
        public void OnDisable();
        public void OnUpdate();
        public IMainServer Server { get; }
        public string Name { get; set; }
    }
}
