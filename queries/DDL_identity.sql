DROP TABLE IF EXISTS BelongsTo;
DROP TABLE IF EXISTS GlobalLeaderboard;
DROP TABLE IF EXISTS ArtistLeaderboard;
DROP TABLE IF EXISTS Leaderboard;
DROP TABLE IF EXISTS Song;
DROP TABLE IF EXISTS Album;
DROP TABLE IF EXISTS Playlist;
DROP TABLE IF EXISTS Artist;
DROP TABLE IF EXISTS [User];

CREATE TABLE Artist (
  ID INT NOT NULL IDENTITY(1,1),
  ArtistName VARCHAR(255) NOT NULL,
  Streams INT,
  PRIMARY KEY(ID)
);

CREATE TABLE [User] (
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Username VARCHAR(255)
);

CREATE TABLE Playlist (
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  TotalDuration INT,
  Genre VARCHAR(255),
  Visibility BIT,
  Name VARCHAR(255),
  AuthorID INT NOT NULL,
  FOREIGN KEY (AuthorID) REFERENCES [User](ID)
);

CREATE TABLE Album (
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(255),
  ReleaseDate DATE,
  TotalDuration INT,
  ArtistID INT NOT NULL,
  FOREIGN KEY (ArtistID) REFERENCES Artist(ID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Song (
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  ArtistID INT NOT NULL,
  Streams INT,
  Genre VARCHAR(255),
  Duration INT,
  Lyrics TEXT,
  Name VARCHAR(255),
  ReleaseDate DATE,
  AlbumID INT,
  FOREIGN KEY (ArtistID) REFERENCES Artist(ID) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (AlbumID) REFERENCES Album(ID)
);

CREATE TABLE BelongsTo (
  LeaderID INT NOT NULL,
  SongID INT NOT NULL,
  PRIMARY KEY (LeaderID, SongID),
  FOREIGN KEY (LeaderID) REFERENCES [User](ID),
  FOREIGN KEY (SongID) REFERENCES Song(ID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Leaderboard (
  LeaderID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Size INT
);

CREATE TABLE ArtistLeaderboard (
  LeaderID INT NOT NULL,
  ID INT NOT NULL,
  AuthorID INT NOT NULL,
  ArtistName VARCHAR(255) NOT NULL,
  PRIMARY KEY (LeaderID, ID),
  FOREIGN KEY (LeaderID) REFERENCES Leaderboard(LeaderID),
  FOREIGN KEY (AuthorID) REFERENCES Artist(ID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE GlobalLeaderboard (
  LeaderID INT NOT NULL,
  FOREIGN KEY (LeaderID) REFERENCES Leaderboard(LeaderID)
);

-- Trigger for INSERT and UPDATE
CREATE TRIGGER UpdateArtistStreamsOnSongInsertUpdate
ON Song
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Update artist's streams for the artists of the inserted or updated songs
    UPDATE Artist
    SET Streams = (
        SELECT SUM(Streams)
        FROM Song
        WHERE Song.ArtistID = Artist.ID
    )
    WHERE ID IN (SELECT DISTINCT ArtistID FROM inserted);
END;

-- Trigger for DELETE
CREATE TRIGGER UpdateArtistStreamsOnSongDelete
ON Song
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Update artist's streams for the artists of the deleted songs
    UPDATE Artist
    SET Streams = (
        SELECT SUM(Streams)
        FROM Song
        WHERE Song.ArtistID = Artist.ID
    )
    WHERE ID IN (SELECT DISTINCT ArtistID FROM deleted);
END;
