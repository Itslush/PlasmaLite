using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace PlasmaLite.SERVICES
{
    internal class AuthService
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

        private readonly Dictionary<Platform, string> _accessTokens = new Dictionary<Platform, string>();
        private readonly PlatformServiceFactory _platformServiceFactory;

        public AuthService(PlatformServiceFactory platformServiceFactory)
        {
            _platformServiceFactory = platformServiceFactory;
        }

        public bool IsAuthenticated(Platform platform)
        {
            return _accessTokens.ContainsKey(platform) && !string.IsNullOrEmpty(_accessTokens[platform]);
        }

        public async Task LoginAsync(Platform platform)
        {
            var authService = _platformServiceFactory.GetPlatformAuthService(platform);
            if (authService == null)
            {
                Console.WriteLine($"Authentication service for {platform} not found.");
                return;
            }

            await authService.LoginAsync();
            _accessTokens[platform] = authService.GetAccessToken();

            Console.WriteLine($"Successfully logged in to {platform}.");
        }

        public void Logout(Platform platform)
        {
            if (_accessTokens.ContainsKey(platform))
            {
                var authService = _platformServiceFactory.GetPlatformAuthService(platform);
                authService?.Logout(); // Optional: Revoke token if platform supports it

                _accessTokens.Remove(platform);
                Console.WriteLine($"Logged out from {platform}.");
            }
        }

        public string GetAccessToken(Platform platform)
        {
            return _accessTokens.TryGetValue(platform, out var token) ? token : null;
        }
    }
}
