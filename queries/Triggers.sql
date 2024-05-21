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