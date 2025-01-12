using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PlasmaLite.SERVICES.MUSIC_SERVICES
{
    internal class SoundcloudSearchService : IPlatformSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId; // Your SoundCloud Client ID

        public SoundcloudSearchService(AuthService authService)
        {
            // No authentication needed for basic SoundCloud search, but you need a Client ID
            _clientId = "YOUR_SOUNDCLOUD_CLIENT_ID";
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PlasmaLite");
        }

        public async Task<List<Song>> SearchAsync(string query)
        {
            try
            {
                var results = new List<Song>();
                string tempPath = Path.GetTempPath();

                // Make a request to the SoundCloud API to resolve the track URL and get track details
                var resolveUrl = $"https://api.soundcloud.com/resolve?url={Uri.EscapeDataString(query)}&client_id={_clientId}";
                var resolveResponse = await _httpClient.GetAsync(resolveUrl);

                if (resolveResponse.IsSuccessStatusCode)
                {
                    var responseContent = await resolveResponse.Content.ReadAsStringAsync();
                    var trackObject = JsonConvert.DeserializeObject<JObject>(responseContent);

                    // Extract the track ID
                    var trackId = trackObject["id"]?.ToString();

                    if (!string.IsNullOrEmpty(trackId))
                    {
                        // Use the track ID to get the stream URL
                        var streamUrl = $"https://api.soundcloud.com/tracks/{trackId}/stream?client_id={_clientId}";
                        string songName = trackObject["title"]?.ToString();
                        string localFileName = $"{songName}.mp3".Replace(":", "").Replace("?", "");
                        string localFilePath = Path.Combine(tempPath, localFileName);
                        File.Create(localFilePath).Close();
                        // Add the song to the results list
                        results.Add(new Song
                        {
                            Title = trackObject["title"]?.ToString(),
                            Artist = trackObject["user"]?["username"]?.ToString(),
                            Album = null, // Not applicable for SoundCloud
                            Duration = TimeSpan.FromMilliseconds(trackObject["duration"]?.Value<double>() ?? 0),
                            Platform = Platform.Soundcloud,
                            PlatformId = trackId,
                            StreamUrl = localFilePath, // Use the stream URL for playback
                            ArtworkUrl = trackObject["artwork_url"]?.ToString()
                        });
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {resolveResponse.StatusCode} - {await resolveResponse.Content.ReadAsStringAsync()}");
                }

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Song>(); // Return an empty list in case of an error
            }
        }
    }
}
