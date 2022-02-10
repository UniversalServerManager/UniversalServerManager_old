using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using USMLib.Event;
using USMLib.User;
using EventArgs = USMLib.Event.EventArgs;

namespace USMLib
{
    public interface IServerManager
    {
        public Process process { get; }
        public bool isRunning { get; }
        public string StartCommand { get; set; }
        public string StopCommand { get; set; }
        public void ForceStop(IUser user);
        public void Stop(IUser user);
        public void Start(IUser user);
        public void SendCommand(IUser user, string command);
        event EventHandler<CancellableEventArgs<IUser>> ProcessEnding;
        event EventHandler<EventArgs<IUser>> ProcessEnded;
        event EventHandler<CancellableEventArgs<IUser>> ProcessStarted;
        event EventHandler<EventArgs<IUser>> ProcessStarting;
        event EventHandler<CancellableEventArgs<UserCommand>> UserSendCommand;
        event EventHandler<EventArgs> OutputStreamChanged;
    }
}
