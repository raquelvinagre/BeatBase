DROP VIEW IF EXISTS SongsWithoutAlbums;
DROP VIEW IF EXISTS TotalSongs;
DROP VIEW IF EXISTS TotalAlbums;
DROP VIEW IF EXISTS TotalArtists;
DROP VIEW IF EXISTS TotalPlaylists;
DROP VIEW IF EXISTS MostPopularSongGenre;
DROP VIEW IF EXISTS MostPopularPlaylistGenre;
DROP VIEW IF EXISTS MostPopularArtist;

go
CREATE VIEW SongsWithoutAlbums AS
SELECT 
  ID,
  ArtistID,
  Streams,
  Genre,
  Duration,
  Lyrics,
  Name,
  ReleaseDate
FROM 
Song
WHERE 
  AlbumID IS NULL;
go



