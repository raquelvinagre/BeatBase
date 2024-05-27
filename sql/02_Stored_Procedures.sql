DROP PROCEDURE GetAllArtists;
DROP PROCEDURE GetAllPlaylists;
DROP PROCEDURE GetAllSongs;
DROP PROCEDURE GetAllAlbums;
DROP PROCEDURE GetSongsByAlbumID;
DROP PROCEDURE GetSongsWithoutAlbum;
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

CREATE PROCEDURE GetSongsWithoutAlbum
AS
BEGIN
    SELECT ID, Name, ArtistID, Genre, Duration, Lyrics, ReleaseDate, AlbumID, Streams
    FROM Song
    WHERE AlbumID IS NULL;
END;
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
