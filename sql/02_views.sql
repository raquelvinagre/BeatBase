DROP VIEW IF EXISTS SongsWithoutAlbums;
DROP VIEW IF EXISTS AverageSongDuration;
DROP VIEW IF EXISTS TotalSongs;
DROP VIEW IF EXISTS TotalAlbums;
DROP VIEW IF EXISTS TotalArtists;
DROP VIEW IF EXISTS TotalPlaylists;
DROP VIEW IF EXISTS MostPopularSongGenre;
DROP VIEW IF EXISTS MostPopularPlaylistGenre;
DROP VIEW IF EXISTS MostPopularArtist;
DROP VIEW IF EXISTS AverageNumSongsPerArtist;
DROP VIEW IF EXISTS AverageNumSongsPerAlbum;

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

CREATE VIEW TotalSongs AS
SELECT 
    COUNT(*) AS TotalSongsCount
FROM 
    Song;
go

CREATE VIEW TotalAlbums AS
SELECT 
    COUNT(*) AS TotalAlbumsCount
FROM 
    Album;

go

CREATE VIEW TotalArtists AS
SELECT 
    COUNT(*) AS TotalArtistsCount
FROM 
    Artist;
go

CREATE VIEW TotalPlaylists AS
SELECT 
    COUNT(*) AS TotalPlaylistsCount
FROM 
    Playlist;
go

CREATE VIEW AverageSongDuration AS
SELECT 
    AVG(Duration) AS AvgSongDuration
FROM 
    Song;
go

CREATE VIEW MostPopularSongGenre AS
SELECT 
    TOP 1 Genre, 
    COUNT(*) AS GenreCount
FROM 
    Song
GROUP BY 
    Genre
ORDER BY 
    GenreCount DESC;
go

CREATE VIEW MostPopularPlaylistGenre AS
SELECT 
    TOP 1 Genre, 
    COUNT(*) AS GenreCount
FROM 
    Playlist
GROUP BY 
    Genre
ORDER BY 
    GenreCount DESC;
go

CREATE VIEW MostPopularArtist AS
SELECT 
    TOP 1 ArtistID,
    SUM(Streams) AS TotalStreams
FROM 
    Song
GROUP BY 
    ArtistID
ORDER BY 
    TotalStreams DESC;
go

CREATE VIEW AverageNumSongsPerArtist AS
SELECT 
    AVG(SongCount) AS AvgNumSongsPerArtist
FROM 
    (SELECT COUNT(*) AS SongCount FROM Song GROUP BY ArtistID) AS ArtistSongs;
GO

CREATE VIEW AverageNumSongsPerAlbum AS
SELECT 
    AVG(SongCount) AS AvgNumSongsPerAlbum
FROM 
    (SELECT COUNT(*) AS SongCount FROM Song WHERE AlbumID IS NOT NULL GROUP BY AlbumID) AS AlbumSongs;
GO

