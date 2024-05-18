namespace BeatBaseForms
{
    internal class Song
    {

		public int SongID { get; set; }
		public string songName { get; set; }
		public string songArtist { get; set; }
		public string songGenre { get; set; }
		public string songDuration { get; set; }
		public string songLyrics { get; set; }
		public System.DateTime songReleaseDate { get; set; }
		public int? songAlbumID { get; set; }

		public int streams { get; set; }


		public override string ToString()
		{
			return songName + " by " + songArtist;;
		}

    }
}