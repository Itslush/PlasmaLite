using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal interface IPlatformSearchService
    {
        /*

        TODO:
        Define:
        > Task<List<Song>>
        > SearchAsync(string query).

        */

        Task<List<Song>> SearchAsync(string query);
    }
}