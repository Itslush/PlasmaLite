using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal class SearchService
    {
        /*
        
        TODO:

        Add a List<IPlatformSearchService> to hold platform-specific search services.

        In the constructor, inject the PlatformServiceFactory and use it to get instances of each
        IPlatformSearchService. Add these instances to the _platformSearchServices list.

        Implement SearchAsync(string query):

        > Loop through each IPlatformSearchService in _platformSearchServices.
        > Call SearchAsync on each service.
        > Aggregate the results into a single list of Song objects.
        > Implement basic result ranking or de-duplication. (LOW PRIORITY MAY ADD AS TOGGLE)
        > Store results in SearchResults.

        Implement a mechanism to clear SearchResults before each new search.
        Implement Console Output:

        > Display search results in the console in a readable format.

        */

        private readonly List<IPlatformSearchService> _platformSearchServices;
        public List<Song> SearchResults { get; private set; } = new List<Song>();

        public SearchService(PlatformServiceFactory platformServiceFactory)
        {
            _platformSearchServices = new List<IPlatformSearchService>
            {
                platformServiceFactory.GetPlatformSearchService(Platform.Spotify),
                platformServiceFactory.GetPlatformSearchService(Platform.Youtube),
                platformServiceFactory.GetPlatformSearchService(Platform.Soundcloud),
            };
        }

        public async Task SearchAsync(string query)
        {
            SearchResults.Clear();

            var searchTasks = _platformSearchServices
                .Where(service => service != null)
                .Select(service => service.SearchAsync(query));

            var results = await Task.WhenAll(searchTasks);

            foreach (var resultList in results)
            {
                SearchResults.AddRange(resultList);
            }

            if (SearchResults.Any())
            {
                Console.WriteLine("\nSearch Results:");
                int i = 1;
                foreach (var song in SearchResults)
                {
                    Console.WriteLine($"{i++}. {song.Artist} - {song.Title} ({song.Platform})");
                }
            }
            else
            {
                Console.WriteLine("No results found.");
            }
        }
    }
}
