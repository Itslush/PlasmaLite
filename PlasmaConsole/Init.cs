using Microsoft.Extensions.DependencyInjection;
using PlasmaLite.SERVICES;
using PlasmaLite.SERVICES.MUSIC_SERVICES;

namespace PlasmaLite
{
    internal class Init
    {
        private static async Task Main(string[] args)
        {
            /* 
            
            TODO:

            Create a ServiceCollection to register services.

            Register SearchService, PlaybackService, AuthenticationService, and PlatformServiceFactory.
            Register platform-specific services (e.g., SpotifySearchService, YouTubeAuthService).

            Build the ServiceProvider.

            Get instances of SearchService and AuthenticationService from the ServiceProvider.
            Implement Basic Console UI Loop that prompts the user for actions (e.g., search, login, play, etc.)

            Call the appropriate methods on the services based on user input.

            */

            var services = new ServiceCollection();

            services.AddSingleton<SearchService>();
            services.AddSingleton<PlaybackService>();
            services.AddSingleton<AuthService>();
            services.AddSingleton<PlatformServiceFactory>();

            services.AddTransient<SpotifySearchService>();
            services.AddTransient<SpotifyAuthService>();
            services.AddTransient<YoutubeSearchService>();
            services.AddTransient<YoutubeAuthService>();
            services.AddTransient<SoundcloudSearchService>();
            services.AddTransient<SoundcloudAuthService>();

            var serviceProvider = services.BuildServiceProvider();

            var searchService = serviceProvider.GetRequiredService<SearchService>();
            var authenticationService = serviceProvider.GetRequiredService<AuthService>();
            var playbackService = serviceProvider.GetRequiredService<PlaybackService>();
            // ... (get other services if needed)

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Search");
                Console.WriteLine("3. Play");
                Console.WriteLine("4. Pause");
                Console.WriteLine("5. Stop");
                Console.WriteLine("6. Next");
                Console.WriteLine("7. Previous");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Available platforms: Spotify, YouTube, Soundcloud");
                        Console.Write("Enter platform to login to: ");
                        string platformName = Console.ReadLine();
                        if (Enum.TryParse<Platform>(platformName, true, out var platform))
                        {
                            await authenticationService.LoginAsync(platform);
                        }
                        else
                        {
                            Console.WriteLine("Invalid platform.");
                        }
                        break;

                    case "2":
                        Console.Write("Enter search query: ");
                        string query = Console.ReadLine();
                        await searchService.SearchAsync(query);
                        break;
                    case "3":
                        if (searchService.SearchResults.Count > 0)
                        {
                            Console.WriteLine("Select a song to play (enter the number):");
                            for (int i = 0; i < searchService.SearchResults.Count; i++)
                            {
                                var song = searchService.SearchResults[i];
                                Console.WriteLine($"{i + 1}. {song.Artist} - {song.Title} ({song.Platform})");
                            }

                            if (int.TryParse(Console.ReadLine(), out int songIndex) && songIndex > 0 && songIndex <= searchService.SearchResults.Count)
                            {
                                await playbackService.Play(searchService.SearchResults[songIndex - 1]);
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No search results to play.");
                        }
                        break;
                    case "4":
                        playbackService.Pause();
                        break;
                    case "5":
                        playbackService.Stop();
                        break;
                    case "6":
                        await playbackService.NextTrack();
                        break;
                    case "7":
                        await playbackService.PreviousTrack();
                        break;
                    case "8":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
