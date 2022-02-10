using System;
using System.Collections.Generic;
using System.Text;

namespace USMLib.User
{
    public class UserCommand
    {
        public IUser user;
        public string commamd;

        public UserCommand(IUser user, string commamd)
        {
            this.commamd = commamd;
            this.user = user;
        }
    }
}
