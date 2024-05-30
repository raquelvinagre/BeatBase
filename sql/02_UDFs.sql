DROP FUNCTION IF EXISTS FilterSongsByGenre;
GO
CREATE FUNCTION dbo.FilterSongsByGenre (@Genre VARCHAR(255))
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
CREATE FUNCTION dbo.FilterSongsByArtistID (@ArtistID INT)
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
CREATE FUNCTION dbo.FilterAlbumsByArtistID (@ArtistID INT)
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
CREATE FUNCTION dbo.FilterPlaylistsByGenre (@Genre VARCHAR(255))
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
CREATE FUNCTION dbo.FilterPlaylistsByVisibility (@Visibility BIT)
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
CREATE FUNCTION dbo.GetTopArtists (@TopN INT)
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
CREATE FUNCTION dbo.GetTopSongs (@TopN INT)
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
CREATE FUNCTION dbo.GetSongsInPlaylist (@PlaylistID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT ps.SongID
    FROM PlaylistSong ps
    WHERE ps.PlaylistID = @PlaylistID
);
go

DROP FUNCTION IF EXISTS TotalSongs;
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

drop FUNCTION IF EXISTS TotalAlbums;
go
CREATE FUNCTION dbo.TotalAlbums()
RETURNS INT
AS
BEGIN
    DECLARE @TotalAlbumsCount INT;
    SELECT @TotalAlbumsCount = COUNT(*) FROM Album;
    RETURN @TotalAlbumsCount;
END;
GO
drop FUNCTION IF EXISTS TotalArtists;

go
CREATE FUNCTION dbo.TotalArtists()
RETURNS INT
AS
BEGIN
    DECLARE @TotalArtistsCount INT;
    SELECT @TotalArtistsCount = COUNT(*) FROM Artist;
    RETURN @TotalArtistsCount;
END;
GO
drop FUNCTION IF EXISTS TotalPlaylists;
go
CREATE FUNCTION dbo.TotalPlaylists()
RETURNS INT
AS
BEGIN
    DECLARE @TotalPlaylistsCount INT;
    SELECT @TotalPlaylistsCount = COUNT(*) FROM Playlist;
    RETURN @TotalPlaylistsCount;
END;
GO

drop FUNCTION IF EXISTS AverageSongDuration;
go 
CREATE FUNCTION dbo.AverageSongDuration()
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgSongDuration FLOAT;
    SELECT @AvgSongDuration = AVG(Duration) FROM Song;
    RETURN @AvgSongDuration;
END;
GO
drop FUNCTION IF EXISTS MostPopularSongGenre;
go
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

drop FUNCTION IF EXISTS MostPopularPlaylistGenre;
go

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

drop FUNCTION IF EXISTS MostPopularArtist;
go

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
drop FUNCTION IF EXISTS AverageNumSongsPerArtist;
go

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
drop FUNCTION IF EXISTS AverageNumSongsPerAlbum;
go

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
drop FUNCTION IF EXISTS AverageSongDuration;
go
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

drop FUNCTION IF EXISTS AverageNumSongsPerArtist;
go

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

drop FUNCTION IF EXISTS AverageNumSongsPerAlbum;
go

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

drop FUNCTION IF EXISTS TotalSongs;
go

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

drop FUNCTION IF EXISTS TotalAlbums;
go

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

drop FUNCTION IF EXISTS TotalArtists;
go

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

drop FUNCTION IF EXISTS TotalPlaylists;
go

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

drop function if exists SearchSongByName;
go
CREATE FUNCTION dbo.SearchSongByName(@songName VARCHAR(255))
RETURNS TABLE
AS
RETURN
(
    SELECT 
        s.ID ,
        s.Name ,
        s.ArtistID ,
        s.Genre ,
        s.Duration ,
        s.Lyrics ,
        s.ReleaseDate ,
        s.AlbumID,
        s.Streams
    FROM 
        Song s
    JOIN 
        Artist a ON s.ArtistID = a.ID
    WHERE 
        s.Name LIKE '%' + @songName + '%'
);