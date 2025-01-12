using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal class PlaybackService
    {
        /*

        TODO:

        Add a Song property CurrentTrack.
        Add a Queue<Song> property PlaybackQueue.

        Implement Play(Song song):

        > Set CurrentTrack to the provided song.
        > Load the song into NAudio
        > Start playback using NAudio

        Implement Pause(), Stop(), NextTrack(), PreviousTrack().

        Use NAudio methods to control playback.
        Update PlaybackState.
        If NextTrack or PreviousTrack is called, dequeue the next song from PlaybackQueue and play it (if the queue is not empty).

        Implement Console Output: Display messages in the console indicating playback state changes (e.g., "Now Playing: [Song Title] by [Artist]").

        */
    }
}
