using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal interface IPlatformAuthService
    {
        /*

        TODO:

        Define:

        > Task LoginAsync().
        > void Logout().
        > string GetAccessToken().

        */

        Task LoginAsync();
        void Logout();
        string GetAccessToken();
    }
}
