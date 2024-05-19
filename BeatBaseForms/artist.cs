namespace BeatBaseForms
{
    internal class Artist
    {

		public int artistID { get; set; }
		public string artistName { get; set; }
		public int streams { get; set; }


		public override string ToString()
		{
			return artistName + " | Streams:  " + streams ;;
		}

    }
}