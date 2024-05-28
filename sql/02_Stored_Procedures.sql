DROP PROCEDURE GetAllArtists;
DROP PROCEDURE GetSongGenres;
DROP PROCEDURE GetPlaylistGenres;
DROP PROCEDURE GetAllPlaylists;
DROP PROCEDURE GetAllSongs;
DROP PROCEDURE GetAllAlbums;
DROP PROCEDURE GetSongsByAlbumID;
DROP PROCEDURE GetAllUsers;
DROP PROCEDURE DeleteAlbumWithSongs;

go
CREATE PROCEDURE GetAllArtists
AS
BEGIN
    SELECT ID, ArtistName, Streams
    FROM Artist
END
go

CREATE PROCEDURE GetSongGenres
AS
BEGIN
    SELECT DISTINCT Genre
    FROM Song
END
go

CREATE PROCEDURE GetPlaylistGenres
AS
BEGIN
    SELECT DISTINCT Genre
    FROM Playlist
END
go

CREATE PROCEDURE GetAllPlaylists
AS
BEGIN
    SELECT ID, Name, Genre, Visibility, TotalDuration, AuthorID
    FROM Playlist
END
go

CREATE PROCEDURE GetAllSongs
AS
BEGIN
    SELECT ID, Name, ArtistID, Genre, Duration, Lyrics, ReleaseDate, AlbumID, Streams
    FROM Song
END
go

CREATE PROCEDURE GetAllAlbums
AS
BEGIN
    SELECT ID, Name, ArtistID, TotalDuration, ReleaseDate
    FROM Album
END
go

CREATE PROCEDURE GetSongsByAlbumID
    @AlbumID INT
AS
BEGIN
    SELECT
        ID,
        ArtistID,
        Streams,
        Genre,
        Duration,
        Lyrics,
        Name,
        ReleaseDate,
        AlbumID
    FROM
        Song
    WHERE 
        AlbumID = @AlbumID;
END
go

CREATE PROCEDURE GetAllUsers
AS
BEGIN
    SELECT ID, Username FROM [User];
END;
go

CREATE PROCEDURE DeleteAlbumWithSongs
    @AlbumID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Update songs associated with the album to set AlbumID to NULL
        UPDATE Song
        SET AlbumID = NULL
        WHERE AlbumID = @AlbumID;

        -- Now delete the album
        DELETE FROM Album
        WHERE ID = @AlbumID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Raise error or handle as per your application's requirement
        THROW;
    END CATCH;
END;

go
-- Create stored procedure to find the most popular song genre
CREATE PROCEDURE dbo.MostPopularSongGenre
AS
BEGIN
    SELECT 
        TOP 1 Genre, 
        COUNT(*) AS GenreCount
    FROM 
        Song
    GROUP BY 
        Genre
    ORDER BY 
        GenreCount DESC;
END;
GO

-- Create stored procedure to find the most popular playlist genre
CREATE PROCEDURE dbo.MostPopularPlaylistGenre
AS
BEGIN
    SELECT 
        TOP 1 Genre, 
        COUNT(*) AS GenreCount
    FROM 
        Playlist
    GROUP BY 
        Genre
    ORDER BY 
        GenreCount DESC;
END;
GO

-- Create stored procedure to find the most popular artist
CREATE PROCEDURE dbo.MostPopularArtist
AS
BEGIN
    SELECT 
        TOP 1 ArtistID,
        SUM(Streams) AS TotalStreams
    FROM 
        Song
    GROUP BY 
        ArtistID
    ORDER BY 
        TotalStreams DESC;
END;
GO
