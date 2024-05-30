using System;

namespace BeatBaseForms
{
    internal class Playlist
    {
		public int playlistID { get; set; }
		public string playlistName { get; set; }
		public int authorID { get; set; }
		public int totalDuration { get; set; }
		public string genre { get; set; }
		public Boolean visibility { get; set; }

		public override string ToString()
		{
			return playlistName + " by " + authorID;;
		}

    }
}