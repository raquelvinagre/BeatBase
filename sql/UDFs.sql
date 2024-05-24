-- Drop existing UDFs if they exist
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

DROP FUNCTION IF EXISTS FilterSongsByName;
GO
CREATE FUNCTION FilterSongsByName (@Name VARCHAR(255))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Song
    WHERE Name LIKE '%' + @Name + '%'
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

DROP FUNCTION IF EXISTS FilterPlaylistsByGenre;
GO
CREATE FUNCTION FilterPlaylistsByGenre (@Genre VARCHAR(255))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Playlist
    WHERE Genre = @Genre
);
GO

DROP FUNCTION IF EXISTS FilterPlaylistsByVisibility;
GO
CREATE FUNCTION FilterPlaylistsByVisibility (@Visibility BIT)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Playlist
    WHERE Visibility = @Visibility
);
GO

DROP FUNCTION IF EXISTS GetTopArtists;
GO
CREATE FUNCTION GetTopArtists (@TopN INT)
RETURNS TABLE
AS
RETURN (
  SELECT TOP(@TopN) ID, ArtistName, Streams
  FROM Artist
  ORDER BY Streams DESC
);
GO

DROP FUNCTION IF EXISTS GetTopSongs;
GO
CREATE FUNCTION GetTopSongs (@TopN INT)
RETURNS TABLE
AS
RETURN (
  SELECT TOP(@TopN) ID, Name, Streams
  FROM Song
  ORDER BY Streams DESC 
);
GO
