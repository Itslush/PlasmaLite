using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;

namespace PlasmaLite.SERVICES.MUSIC_SERVICES
{
    internal class SpotifyAuthService
    {
        private string _accessToken;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private string _state;
        private PKCEAuthenticator _authenticator;
        private static SpotifyClient _spotify;

        public SpotifyAuthService()
        {
            _clientId = "YOUR_SPOTIFY_CLIENT_ID";
            _clientSecret = "YOUR_SPOTIFY_CLIENT_SECRET";
            _redirectUri = "http://localhost:5000/callback";
        }

        public async Task LoginAsync()
        {
            _state = GenerateRandomString(16);

            _authenticator = new PKCEAuthenticator(_clientId, _redirectUri);

            _authenticator.TokenRefreshed += OnTokenRefreshed;

            var loginRequest = new LoginRequest(
            new Uri(_redirectUri),
            _clientId,
            LoginRequest.ResponseType.Code)
            {
                Scope = new[] { Scopes.UserReadPrivate, Scopes.UserReadEmail, Scopes.UserLibraryRead },
                State = _state
            };
            BrowserUtil.Open(loginRequest.ToUri());

            var (verifier, challenge) = PKCEUtil.GenerateCodes();

            var uri = loginRequest.ToUri();

            Console.WriteLine("Authorization Code URL:");
            Console.WriteLine(uri);

            Console.WriteLine("Please enter the code from the URL:");
            var code = Console.ReadLine();

            var response = await new OAuthClient().RequestToken(
              new PKCETokenRequest(_clientId, code, new Uri(_redirectUri), verifier)
            );
            // Initialize the Spotify Client with the Access Token
            _spotify = new SpotifyClient(response.AccessToken);
            _accessToken = response.AccessToken;
            Console.WriteLine("Login successful. Access token retrieved.");
        }

        public void Logout()
        {
            _accessToken = null;
            Console.WriteLine("Logged out from Spotify.");
        }

        public string GetAccessToken()
        {
            return _accessToken;
        }

        private void OnTokenRefreshed(object sender, PKCETokenResponse response)
        {
            _accessToken = response.AccessToken;
            Console.WriteLine("Spotify access token refreshed.");
        }

        private string GenerateRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var state = new char[length];
            for (int i = 0; i < length; i++)
            {
                state[i] = chars[random.Next(chars.Length)];
            }

            return new string(state);
        }
    }
}