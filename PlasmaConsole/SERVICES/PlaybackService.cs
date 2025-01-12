using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

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

        public Song CurrentTrack { get; private set; }
        public Queue<Song> PlaybackQueue { get; private set; } = new Queue<Song>();
        private WaveOutEvent _waveOut;
        private AudioFileReader _audioFileReader;
        private bool isAutoPlay = true;
        public PlaybackService()
        {
            _waveOut = new WaveOutEvent();
            _waveOut.PlaybackStopped += OnPlaybackStopped;
        }
        public async Task Play(Song song)
        {
            if (song == null)
            {
                Console.WriteLine("No song selected.");
                return;
            }
            try
            {
                Stop();
                if (!File.Exists(song.StreamUrl))
                {
                    Console.WriteLine($"File not found: {song.StreamUrl}");
                    return;
                }

                _audioFileReader = new AudioFileReader(song.StreamUrl);
                _waveOut.Init(_audioFileReader);

                CurrentTrack = song;
                _waveOut.Play();

                Console.WriteLine($"Now Playing: {CurrentTrack.Title} by {CurrentTrack.Artist}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing song: {ex.Message}");
            }
        }

        public void Pause()
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Pause();
                Console.WriteLine("Playback paused.");
            }
        }

        public void Stop()
        {
            if (_waveOut.PlaybackState != PlaybackState.Stopped)
            {
                _waveOut.Stop();
            }
            if (_audioFileReader != null)
            {
                _audioFileReader.Dispose();
                _audioFileReader = null;
            }
        }

        public async Task NextTrack()
        {
            if (PlaybackQueue.Count > 0)
            {
                await Play(PlaybackQueue.Dequeue());
            }
            else
            {
                Console.WriteLine("End of queue.");
                Stop();
            }
        }

        public async Task PreviousTrack()
        {
            // NAudio doesn't support going to a previous track directly
            // Either restart the current track or maintain a history of played tracks
            Console.WriteLine("Previous track not supported in this implementation.");
        }

        private async void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (isAutoPlay && PlaybackQueue.Count > 0)
            {
                await NextTrack();
            }
        }
    }
}
