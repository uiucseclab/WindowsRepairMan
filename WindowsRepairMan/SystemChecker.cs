using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace WindowsRepairMan
{
    public class SystemChecker
    {
        public bool IsAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
}
