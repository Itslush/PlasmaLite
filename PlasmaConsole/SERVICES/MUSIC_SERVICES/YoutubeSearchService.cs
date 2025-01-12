using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES.MUSIC_SERVICES
{
    internal class YoutubeSearchService : IPlatformSearchService
    {
        private readonly YouTubeService _youtubeService;

        public YouTubeSearchService(AuthService authService)
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = authService.GetAccessToken(Platform.Youtube),
                ApplicationName = "PlasmaLite"
            });
        }

        public async Task<List<Song>> SearchAsync(string query)
        {
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.Q = query;
            searchListRequest.MaxResults = 10;
            searchListRequest.Type = "video";

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var results = new List<Song>();
            string tempPath = Path.GetTempPath();

            foreach (var searchResult in searchListResponse.Items)
            {
                string songName = searchResult.Snippet.Title;
                string localFileName = $"{songName}.mp3".Replace(":", "").Replace("?", "");
                string localFilePath = Path.Combine(tempPath, localFileName);
                File.Create(localFilePath).Close();
                results.Add(new Song
                {
                    Title = searchResult.Snippet.Title,
                    Artist = searchResult.Snippet.ChannelTitle,
                    Album = null, // YouTube doesn't have albums in the same way as Spotify
                    Duration = TimeSpan.Zero, // Duration is not directly available in search results
                    Platform = Platform.Youtube,
                    PlatformId = searchResult.Id.VideoId,
                    StreamUrl = localFilePath,
                    ArtworkUrl = searchResult.Snippet.Thumbnails.Default__.Url
                });
            }

            return results;
        }
    }
}
