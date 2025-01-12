using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace PlasmaLite.SERVICES.MUSIC_SERVICES
{
    internal class YoutubeAuthService
    {
        private string _accessToken;
        private string[] _scopes = { YouTubeService.Scope.YoutubeReadonly };
        private string _clientId = "YOUR_YOUTUBE_CLIENT_ID";
        private string _clientSecret = "YOUR_YOUTUBE_CLIENT_SECRET";

        public async Task LoginAsync()
        {
            string credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "youtube_credentials.json");

            ClientSecrets secrets = new ClientSecrets
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret
            };

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = _scopes,
                DataStore = new FileDataStore(credentialPath, true)
            });

            // initiate the OAuth 2.0 flow
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                _scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credentialPath, true));
            if (credential.Token.IsExpired(credential.Flow.Clock))
            {
                Console.WriteLine("The access token has expired.");
                return;
            }

            _accessToken = credential.Token.AccessToken;
            Console.WriteLine("Login successful. Access token retrieved.");
        }

        public void Logout()
        {

            _accessToken = null;
            Console.WriteLine("Logged out from YouTube.");
        }

        public string GetAccessToken()
        {
            return _accessToken;
        }
    }
}
