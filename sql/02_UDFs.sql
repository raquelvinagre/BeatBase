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

DROP FUNCTION IF EXISTS GetSongsInPlaylist;
go
CREATE FUNCTION GetSongsInPlaylist (@PlaylistID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT ps.SongID
    FROM PlaylistSong ps
    WHERE ps.PlaylistID = @PlaylistID
);

go
CREATE FUNCTION dbo.TotalSongs()
RETURNS INT
AS
BEGIN
    DECLARE @TotalSongsCount INT;
    SELECT @TotalSongsCount = COUNT(*) FROM Song;
    RETURN @TotalSongsCount;
END;
GO

CREATE FUNCTION dbo.TotalAlbums()
RETURNS INT
AS
BEGIN
    DECLARE @TotalAlbumsCount INT;
    SELECT @TotalAlbumsCount = COUNT(*) FROM Album;
    RETURN @TotalAlbumsCount;
END;
GO

CREATE FUNCTION dbo.TotalArtists()
RETURNS INT
AS
BEGIN
    DECLARE @TotalArtistsCount INT;
    SELECT @TotalArtistsCount = COUNT(*) FROM Artist;
    RETURN @TotalArtistsCount;
END;
GO

CREATE FUNCTION dbo.TotalPlaylists()
RETURNS INT
AS
BEGIN
    DECLARE @TotalPlaylistsCount INT;
    SELECT @TotalPlaylistsCount = COUNT(*) FROM Playlist;
    RETURN @TotalPlaylistsCount;
END;
GO

CREATE FUNCTION dbo.AverageSongDuration()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgSongDuration FLOAT;
    SELECT @AvgSongDuration = AVG(Duration) FROM Song;
    RETURN @AvgSongDuration;
END;
GO

CREATE FUNCTION dbo.MostPopularSongGenre()
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1 Genre, COUNT(*) AS GenreCount
    FROM Song
    GROUP BY Genre
    ORDER BY GenreCount DESC
);
GO

CREATE FUNCTION dbo.MostPopularPlaylistGenre()
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1 Genre, COUNT(*) AS GenreCount
    FROM Playlist
    GROUP BY Genre
    ORDER BY GenreCount DESC
);
GO

CREATE FUNCTION dbo.MostPopularArtist()
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1 ArtistID, SUM(Streams) AS TotalStreams
    FROM Song
    GROUP BY ArtistID
    ORDER BY TotalStreams DESC
);
GO

CREATE FUNCTION dbo.AverageNumSongsPerArtist()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgNumSongsPerArtist FLOAT;
    SELECT @AvgNumSongsPerArtist = AVG(SongCount)
    FROM (SELECT COUNT(*) AS SongCount FROM Song GROUP BY ArtistID) AS ArtistSongs;
    RETURN @AvgNumSongsPerArtist;
END;
GO

CREATE FUNCTION dbo.AverageNumSongsPerAlbum()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgNumSongsPerAlbum FLOAT;
    SELECT @AvgNumSongsPerAlbum = AVG(SongCount)
    FROM (SELECT COUNT(*) AS SongCount FROM Song WHERE AlbumID IS NOT NULL GROUP BY AlbumID) AS AlbumSongs;
    RETURN @AvgNumSongsPerAlbum;
END;
GO

CREATE FUNCTION dbo.AverageSongDuration ()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgSongDuration FLOAT;
    SELECT @AvgSongDuration = AVG(Duration)
    FROM Song;
    RETURN @AvgSongDuration;
END;
GO

CREATE FUNCTION dbo.AverageNumSongsPerArtist ()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgNumSongsPerArtist FLOAT;
    SELECT @AvgNumSongsPerArtist = AVG(SongCount)
    FROM (
        SELECT COUNT(*) AS SongCount 
        FROM Song 
        GROUP BY ArtistID
    ) AS ArtistSongs;
    RETURN @AvgNumSongsPerArtist;
END;
GO

CREATE FUNCTION dbo.AverageNumSongsPerAlbum ()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgNumSongsPerAlbum FLOAT;
    SELECT @AvgNumSongsPerAlbum = AVG(SongCount)
    FROM (
        SELECT COUNT(*) AS SongCount 
        FROM Song 
        WHERE AlbumID IS NOT NULL 
        GROUP BY AlbumID
    ) AS AlbumSongs;
    RETURN @AvgNumSongsPerAlbum;
END;
GO

CREATE FUNCTION dbo.TotalSongs ()
RETURNS INT
AS
BEGIN
    DECLARE @TotalSongsCount INT;
    SELECT @TotalSongsCount = COUNT(*)
    FROM Song;
    RETURN @TotalSongsCount;
END;
GO

CREATE FUNCTION dbo.TotalAlbums ()
RETURNS INT
AS
BEGIN
    DECLARE @TotalAlbumsCount INT;
    SELECT @TotalAlbumsCount = COUNT(*)
    FROM Album;
    RETURN @TotalAlbumsCount;
END;
GO

CREATE FUNCTION dbo.TotalArtists ()
RETURNS INT
AS
BEGIN
    DECLARE @TotalArtistsCount INT;
    SELECT @TotalArtistsCount = COUNT(*)
    FROM Artist;
    RETURN @TotalArtistsCount;
END;
GO

CREATE FUNCTION dbo.TotalPlaylists ()
RETURNS INT
AS
BEGIN
    DECLARE @TotalPlaylistsCount INT;
    SELECT @TotalPlaylistsCount = COUNT(*)
    FROM Playlist;
    RETURN @TotalPlaylistsCount;
END;
GO

