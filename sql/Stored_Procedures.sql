DROP PROCEDURE GetAllArtists;
DROP PROCEDURE GetAllPlaylists;
DROP PROCEDURE GetAllSongs;
DROP PROCEDURE GetAllAlbums;
DROP PROCEDURE GetSongsByAlbumID;


CREATE PROCEDURE GetAllArtists
AS
BEGIN
    SELECT ID, ArtistName, Streams
    FROM Artist
END

CREATE PROCEDURE GetAllPlaylists
AS
BEGIN
    SELECT Name, Genre, Visibility, TotalDuration, AuthorID
    FROM Playlist
END

CREATE PROCEDURE GetAllSongs
AS
BEGIN
    SELECT ID, Name, ArtistID, Genre, Duration, Lyrics, ReleaseDate, AlbumID, Streams
    FROM Song
END

CREATE PROCEDURE GetAllAlbums
AS
BEGIN
    SELECT ID, Name, ArtistID, TotalDuration, ReleaseDate
    FROM Album
END

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
END;
