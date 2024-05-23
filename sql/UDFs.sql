-- Create UDFs to filter songs and albums by genre and artistID
DROP FUNCTION IF EXISTS FilterSongsByGenre;
GO
CREATE FUNCTION FilterSongsByGenre (@Genre VARCHAR(255))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Song
    WHERE Genre = @Genre
);
GO

DROP FUNCTION IF EXISTS FilterSongsByArtistID;
GO
CREATE FUNCTION FilterSongsByArtistID (@ArtistID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Song
    WHERE ArtistID = @ArtistID
);
GO

DROP FUNCTION IF EXISTS FilterAlbumsByArtistID;
GO
CREATE FUNCTION FilterAlbumsByArtistID (@ArtistID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Album
    WHERE ArtistID = @ArtistID
);
GO



DROP FUNCTION IF EXISTS GetTopArtists;
CREATE FUNCTION GetTopArtists (@TopN INT)
RETURNS TABLE
AS
RETURN (
  SELECT TOP(@TopN) ID, ArtistName, Streams
  FROM Artist
  ORDER BY Streams DESC
);

DROP FUNCTION IF EXISTS GetTopSongs;
CREATE FUNCTION GetTopSongs (@TopN INT)
RETURNS TABLE
AS
RETURN (
  SELECT TOP(@TopN) ID, SongName, Streams
  FROM Song
  ORDER BY Streams DESC 
);


