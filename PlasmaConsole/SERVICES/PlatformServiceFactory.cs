using PlasmaLite.SERVICES.MUSIC_SERVICES;
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

        private readonly Dictionary<Platform, IPlatformSearchService> _searchServices;
        private readonly Dictionary<Platform, IPlatformAuthService> _authServices;

        public PlatformServiceFactory(IServiceProvider serviceProvider)
        {
            _searchServices = new Dictionary<Platform, IPlatformSearchService>();
            _authServices = new Dictionary<Platform, IPlatformAuthService>();

            _searchServices[Platform.Spotify] = (IPlatformSearchService)serviceProvider.GetService(typeof(SpotifySearchService));
            _authServices[Platform.Spotify] = (IPlatformAuthService)serviceProvider.GetService(typeof(SpotifyAuthService));
            _searchServices[Platform.Youtube] = (IPlatformSearchService)serviceProvider.GetService(typeof(YoutubeSearchService));
            _authServices[Platform.Youtube] = (IPlatformAuthService)serviceProvider.GetService(typeof(YoutubeAuthService));
            _authServices[Platform.Soundcloud] = (IPlatformAuthService)serviceProvider.GetService(typeof(SoundcloudSearchService));
            _authServices[Platform.Soundcloud] = (IPlatformAuthService)serviceProvider.GetService(typeof(SoundcloudAuthService));
            // add more in the future maybe? "Bandcamp"?
        }

        public IPlatformSearchService? GetPlatformSearchService(Platform platform)
        {
            return _searchServices.TryGetValue(platform, out var service) ? service : null;
        }

        public IPlatformAuthService? GetPlatformAuthService(Platform platform)
        {
            return _authServices.TryGetValue(platform, out var service) ? service : null;
        }
    }
}
