using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal class PlatformServiceFactory
    {
        /*

        TODO:

        Add a method GetPlatformSearchService(Platform platform):
        > Use a switch statement or a dictionary to return the correct IPlatformSearchService implementation based on the Platform enum.

        Add a method GetPlatformAuthService(Platform platform):
        > Similar to GetPlatformSearchService, return the correct IPlatformAuthService implementation.

        In the constructor, inject instances of all platform-specific services (register these in Init.cs). This is so the factory can return them when requested.

        */
    }
}
