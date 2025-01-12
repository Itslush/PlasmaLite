namespace PlasmaLite
{
    internal class Song
    {
        /*

        TODO:

        Define properties:

        > Title
        > Artist
        > Album
        > Duration
        > Platform
        > PlatformId
        > StreamUrl
        > ArtworkUrl

        Define enum:

        > Platform: SC, SP, YT

        */

        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? PlatformId { get; set; }
        public string? StreamUrl { get; set; }
        public string? ArtworkUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public Platform Platform { get; set; }
    }

    public enum Platform { Soundcloud, Spotify, Youtube}
}
