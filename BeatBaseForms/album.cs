namespace BeatBaseForms
{
    internal class Album
    {

		public int albumID { get; set; }
		public string albumName { get; set; }
		public string albumArtist { get; set; }
		//public string albumGenre { get; set; }
		public string albumDuration { get; set; }
		//public string albumLyrics { get; set; }
		public System.DateTime albumReleaseDate { get; set; }

		public int streams { get; set; }


		public override string ToString()
		{
			return albumName + " | " + albumArtist + " | " + albumID + " | " + albumDuration + " | " + albumReleaseDate +  " | " +  streams;
		}

    }
}