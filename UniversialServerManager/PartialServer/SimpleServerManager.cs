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
using USMLib.Server;
using EventArgs = USMLib.Event.EventArgs;
using System.Runtime.InteropServices;

namespace UniversialServerManager.PartialServer
{
    public class SimpleServerManager : IServerManager
    {
        public Process serverProcess;
        public string[] stopCommand = new string[] { "stop", "exit", "end", ".exit", "exit()" };
        public string lineCache;
        public string cache;
        

        protected string name;
        protected PartialServer owner;
        protected Task task;
        protected Process process_m;
        public Process Process => process_m;

        public bool IsRunning
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

        public IPartialServer PartialServer => owner;

        public string Name => name;

        public event EventHandler<EventArgs> OutputStreamChanged;
        public event EventHandler<CancellableEventArgs<IUser>> ProcessEnding;
        public event EventHandler<EventArgs<IUser>> ProcessEnded;
        public event EventHandler<CancellableEventArgs<IUser>> ProcessStarting;
        public event EventHandler<EventArgs<IUser>> ProcessStarted;
        public event EventHandler<CancellableEventArgs<UserCommand>> UserSendCommand;

        public void ForceStop(IUser user)
        {
            if (user.GetPermission(name).HasFlag(Permission.Execute))
            {

            }
            CancellableEventArgs<IUser> eventArgs = new CancellableEventArgs<IUser>(user);
            ProcessEnding(this, eventArgs);
            if (eventArgs.Cencelled) return;
            Process.Kill();
            process_m = null;
            ProcessEnded(this, eventArgs);
        }

        public void SendCommand(IUser user, string command)
        {
            if (user.GetPermission(name).HasFlag(Permission.Execute)) return;
            if (command.Contains('\n'))
            {
                // WARN
            }
            CancellableEventArgs<UserCommand> eventArgs = new CancellableEventArgs<UserCommand>(new UserCommand(user, command));
            UserSendCommand(this, eventArgs);
            if (eventArgs.Cencelled) return;
            Process.StandardInput.WriteLine(command);
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
            Process.Start();
            ProcessStarted(this, eventArgs);
        }

        public void Stop(IUser user)
        {
            CancellableEventArgs<IUser> eventArgs = new CancellableEventArgs<IUser>(user);
            ProcessEnding(this, eventArgs);
            if (eventArgs.Cencelled) return;
            if (stopCommand.Length == 0)
            {
                Process.Close();
            }
            else
            {
                if (StopCommand != null)
                    Process.StandardInput.WriteLine(StopCommand);
                else
                    foreach (var command in stopCommand)
                    {
                        Process.StandardInput.WriteLine(command);
                    }
            }
            process_m = null;
            ProcessEnded(this, eventArgs);
        }

        protected virtual void IOStreamProcessor()
        {
            if (IsRunning)
            {
                if (!process_m.StandardOutput.EndOfStream)
                {
                    lineCache += process_m.StandardOutput.ReadToEnd();
                    int index = lineCache.IndexOf('\n');
                    while (index != -1)
                    {
                        string current = lineCache[..index];
                        EventArgs<string> args = new(current);
                        OutputStreamChanged(this, args);
                        lineCache = lineCache[(index + 1)..];
                        index = lineCache.IndexOf('\n');
                    }
                }
            }
        }
    }
}
