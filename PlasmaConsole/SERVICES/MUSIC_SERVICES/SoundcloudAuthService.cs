using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES.MUSIC_SERVICES
{
    internal class SoundcloudAuthService :IPlatformAuthService
    {
        private string _accessToken;

        // Note:SoundCloud search doesn't require authentication

        public async Task LoginAsync()
        {
            // I'm lazy implement Auth2.0 in the future here

            Console.WriteLine("SoundCloud authentication not implemented.");
            await Task.CompletedTask;
        }

        public void Logout()
        {
            _accessToken = null;
            Console.WriteLine("Logged out from SoundCloud (no authentication yet).");
        }

        public string GetAccessToken()
        {
            return _accessToken;
        }
    }
}
