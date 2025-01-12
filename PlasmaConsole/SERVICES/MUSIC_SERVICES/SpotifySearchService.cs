using SpotifyAPI.Web;

namespace PlasmaLite.SERVICES.MUSIC_SERVICES
{
    internal class SpotifySearchService : IPlatformSearchService
    {
        private readonly SpotifyClient _spotifyClient;

        public SpotifySearchService(AuthService authService)
        {
            _spotifyClient = new SpotifyClient(authService.GetAccessToken(Platform.Spotify));
        }
        public async Task<List<Song>> SearchAsync(string query)
        {
            var searchRequest = new SearchRequest(SearchRequest.Types.Track, query);
            var searchResponse = await _spotifyClient.Search.Item(searchRequest);

            var results = new List<Song>();
            if (searchResponse.Tracks != null)
            {
                string tempPath = Path.GetTempPath(); // Get the temporary folder path

                foreach (var track in searchResponse.Tracks.Items)
                {
                    // Ensure there is at least one artist before accessing the Artists list
                    string artistName = track.Artists.Any() ? track.Artists[0].Name : "Unknown Artist";
                    string songName = track.Name;
                    string localFileName = $"{artistName} - {songName}.mp3".Replace(":", "").Replace("?", "");
                    string localFilePath = Path.Combine(tempPath, localFileName); // Create a local file path
                    File.Create(localFilePath).Close();
                    results.Add(new Song
                    {
                        Title = track.Name,
                        Artist = artistName,
                        Album = track.Album.Name,
                        Duration = TimeSpan.FromMilliseconds(track.DurationMs),
                        Platform = Platform.Spotify,
                        PlatformId = track.Id,
                        StreamUrl = localFilePath,
                        ArtworkUrl = track.Album.Images.FirstOrDefault()?.Url
                    });
                }
            }

            return results;
        }
    }
}