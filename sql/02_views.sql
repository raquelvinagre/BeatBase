DROP VIEW IF EXISTS SongsWithoutAlbums;


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



