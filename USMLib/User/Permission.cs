using System;
using System.Collections.Generic;
using System.Text;

namespace USMLib.User
{
    public enum Permission
    {
        None = 0,
        Read = 1,
        Execute = 2,
        Write = 4,
        Any = 65535,
    }
}
