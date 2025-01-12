using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal class AuthenticationService
    {
        /*

        TODO:

        Add a Dictionary<Platform, string> to store access tokens for each platform.

        Add a method IsAuthenticated(Platform platform):

        > Check if an access token exists for the given platform and if it's still valid (might need a separate method to handle token refresh).

        Implement LoginAsync(Platform platform):

        > Get the appropriate IPlatformAuthService from the PlatformServiceFactory.
        > Call LoginAsync() on the platform-specific service.
        > Handle the OAuth 2.0 flow using IdentityModel.OidcClient.
        > After successful authentication, store the access token in the dictionary.
        > Print a message to the console indicating successful login.

        Implement Logout(Platform platform):
        
        > Remove the access token for the platform from the dictionary.
        > If the platform supports it, call an API endpoint to revoke the token.

        Implement GetAccessToken(Platform platform):

        >Return the access token for the platform.

        */
    }
}
