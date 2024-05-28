-- Drop triggers
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'UpdateArtistStreamsOnInsert')
BEGIN
  DROP TRIGGER UpdateArtistStreamsOnInsert;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'UpdateArtistStreamsOnUpdate')
BEGIN
  DROP TRIGGER UpdateArtistStreamsOnUpdate;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'UpdateArtistStreamsOnDelete')
BEGIN
  DROP TRIGGER UpdateArtistStreamsOnDelete;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'trg_AfterInsert_PlaylistSong')
BEGIN
  DROP TRIGGER trg_AfterInsert_PlaylistSong;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'trg_AfterDelete_PlaylistSong')
BEGIN
  DROP TRIGGER trg_AfterDelete_PlaylistSong;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'trg_AfterUpdate_PlaylistSong')
BEGIN
  DROP TRIGGER trg_AfterUpdate_PlaylistSong;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'trg_AfterInsert_Song_Album')
BEGIN
  DROP TRIGGER trg_AfterInsert_Song_Album;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'trg_AfterDelete_Song_Album')
BEGIN
  DROP TRIGGER trg_AfterDelete_Song_Album;
END;
IF EXISTS (SELECT name FROM sys.triggers WHERE name = 'trg_AfterUpdate_Song_Album')
BEGIN
  DROP TRIGGER trg_AfterUpdate_Song_Album;
END;
GO


-- Create triggers to update Artist streams
CREATE TRIGGER UpdateArtistStreamsOnInsert
ON Song
AFTER INSERT
AS
BEGIN
  UPDATE Artist
  SET Streams = (
    SELECT SUM(Streams)
    FROM Song
    WHERE ArtistID = inserted.ArtistID
  )
  FROM inserted
  WHERE Artist.ID = inserted.ArtistID;
END;
GO

CREATE TRIGGER UpdateArtistStreamsOnUpdate
ON Song
AFTER UPDATE
AS
BEGIN
  UPDATE Artist
  SET Streams = (
    SELECT SUM(Streams)
    FROM Song
    WHERE ArtistID = inserted.ArtistID
  )
  FROM inserted
  WHERE Artist.ID = inserted.ArtistID;
END;
GO

CREATE TRIGGER UpdateArtistStreamsOnDelete
ON Song
AFTER DELETE
AS
BEGIN
  UPDATE Artist
  SET Streams = (
    SELECT SUM(Streams)
    FROM Song
    WHERE ArtistID = deleted.ArtistID
  )
  FROM deleted
  WHERE Artist.ID = deleted.ArtistID;
END;
GO

CREATE TRIGGER trg_AfterInsert_PlaylistSong
ON PlaylistSong
AFTER INSERT
AS
BEGIN
  UPDATE Playlist
  SET TotalDuration = COALESCE((SELECT SUM(s.Duration)
                       FROM PlaylistSong ps
                       JOIN Song s ON ps.SongID = s.ID
                       WHERE ps.PlaylistID = INSERTED.PlaylistID),0)
  FROM INSERTED
  WHERE Playlist.ID = INSERTED.PlaylistID;
END;
GO

CREATE TRIGGER trg_AfterDelete_PlaylistSong
ON PlaylistSong
AFTER DELETE
AS
BEGIN
  UPDATE Playlist
  SET TotalDuration = COALESCE((SELECT SUM(s.Duration)
                       FROM PlaylistSong ps
                       JOIN Song s ON ps.SongID = s.ID
                       WHERE ps.PlaylistID = DELETED.PlaylistID),0)
  FROM DELETED
  WHERE Playlist.ID = DELETED.PlaylistID;
END;
GO

CREATE TRIGGER trg_AfterUpdate_PlaylistSong
ON PlaylistSong
AFTER UPDATE
AS
BEGIN
  UPDATE Playlist
  SET TotalDuration = COALESCE((SELECT SUM(s.Duration)
                       FROM PlaylistSong ps
                       JOIN Song s ON ps.SongID = s.ID
                       WHERE ps.PlaylistID = INSERTED.PlaylistID),0)
  FROM INSERTED
  WHERE Playlist.ID = INSERTED.PlaylistID;

  UPDATE Playlist
  SET TotalDuration = COALESCE((SELECT SUM(s.Duration)
                       FROM PlaylistSong ps
                       JOIN Song s ON ps.SongID = s.ID
                       WHERE ps.PlaylistID = DELETED.PlaylistID),0)
  FROM DELETED
  WHERE Playlist.ID = DELETED.PlaylistID;
END;
GO

CREATE TRIGGER trg_AfterInsert_Song_Album
ON Song
AFTER INSERT
AS
BEGIN
  UPDATE Album
  SET TotalDuration = COALESCE((SELECT SUM(Duration)
                                FROM Song
                                WHERE AlbumID = INSERTED.AlbumID), 0)
  FROM INSERTED
  WHERE Album.ID = INSERTED.AlbumID;
END;
GO

CREATE TRIGGER trg_AfterDelete_Song_Album
ON Song
AFTER DELETE
AS
BEGIN
  UPDATE Album
  SET TotalDuration = COALESCE((SELECT SUM(Duration)
                                FROM Song
                                WHERE AlbumID = DELETED.AlbumID), 0)
  FROM DELETED
  WHERE Album.ID = DELETED.AlbumID;
END;
GO

CREATE TRIGGER trg_AfterUpdate_Song_Album
ON Song
AFTER UPDATE
AS
BEGIN
  -- Update for the new AlbumID
  UPDATE Album
  SET TotalDuration = COALESCE((SELECT SUM(Duration)
                                FROM Song
                                WHERE AlbumID = INSERTED.AlbumID), 0)
  FROM INSERTED
  WHERE Album.ID = INSERTED.AlbumID;

  -- Update for the old AlbumID (in case the AlbumID was changed)
  UPDATE Album
  SET TotalDuration = COALESCE((SELECT SUM(Duration)
                                FROM Song
                                WHERE AlbumID = DELETED.AlbumID), 0)
  FROM DELETED
  WHERE Album.ID = DELETED.AlbumID;
END;
GO

