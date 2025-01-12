namespace PlasmaLite
{
    internal class Playlist
    {
        /*

        TODO:

        Define properties:

        > Name
        > Songs
        > Platform
        > PlatformId

        */

        public string? Name { get; set; }
        public List<Song>? Songs { get; set; }
        public Platform Platform { get; set; }
        public string? PlatformId { get; set; }
    }
}
