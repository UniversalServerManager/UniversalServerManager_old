using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UniversalServerManager.User;
using USMLib;
using USMLib.Event;
using USMLib.User;
using EventArgs = USMLib.Event.EventArgs;

namespace UniversialServerManager.PartialServer
{
    public class SimpleServerManager : IServerManager
    {
        public Process serverProcess;
        public string[] stopCommand;
        public string lineCache;
        public string cache;

        protected Task task;
        protected Process process_m;
        public Process process => process_m;

        public bool isRunning
        {
            get
            {
                if (process_m == null) return false;
                else if (process_m.HasExited) return false;
                else return true;
            }
        }

        public string StartCommand { get; set; }
        public string StopCommand { get; set; }

        public event EventHandler<EventArgs> OutputStreamChanged;
        public event EventHandler<CancellableEventArgs<IUser>> ProcessEnding;
        public event EventHandler<EventArgs<IUser>> ProcessEnded;
        public event EventHandler<CancellableEventArgs<IUser>> ProcessStarted;
        public event EventHandler<EventArgs<IUser>> ProcessStarting;
        public event EventHandler<CancellableEventArgs<UserCommand>> UserSendCommand;

        public void ForceStop(IUser user)
        {
            CancellableEventArgs<IUser> eventArgs = new CancellableEventArgs<IUser>(user);
            ProcessEnding(this, eventArgs);
            if (eventArgs.Cencelled) return;
            process.Kill();
            process_m = null;
            ProcessEnded(this, eventArgs);
        }

        public void SendCommand(IUser user, string command)
        {
            if (command.Contains('\n'))
            {
                // WARN
            }
            CancellableEventArgs<UserCommand> eventArgs = new CancellableEventArgs<UserCommand>(new UserCommand(user, command));
            UserSendCommand(user, eventArgs);
            if (eventArgs.Cencelled) return;
            process.StandardInput.WriteLine(command);
        }

        public void Start(IUser user)
        {
            CancellableEventArgs<IUser> eventArgs = new CancellableEventArgs<IUser>(user);
            ProcessStarting(this, eventArgs);
            process_m = new Process();
            process_m.StartInfo.RedirectStandardError = true;
            process_m.StartInfo.RedirectStandardInput = true;
            process_m.StartInfo.RedirectStandardOutput = true;
            if (eventArgs.Cencelled) return;
            process.Start();
            ProcessStarted(this, eventArgs);
        }

        public void Stop(IUser user)
        {
            CancellableEventArgs<IUser> eventArgs = new CancellableEventArgs<IUser>(user);
            ProcessEnding(this, eventArgs);
            if (eventArgs.Cencelled) return;
            if (stopCommand.Length == 0)
            {
                process.Close();
            }
            else
            {
                foreach (var command in stopCommand)
                {
                    process.StandardInput.WriteLine(command);
                }
            }
            process_m = null;
            ProcessEnded(this, eventArgs);
        }

        protected virtual void IOStreamProcessor()
        {
            if (isRunning)
            {
                if (!process_m.StandardOutput.EndOfStream)
                {
                    lineCache += process_m.StandardOutput.ReadToEnd();
                    int index = lineCache.IndexOf('\n');
                    while (index != -1)
                    {
                        string current = lineCache[..index];
                        EventArgs<string> args = new EventArgs<string>(current);
                        OutputStreamChanged(this, args);
                        lineCache = lineCache[(index + 1)..];
                        index = lineCache.IndexOf('\n');
                    }
                }
            }
        }
    }
}
