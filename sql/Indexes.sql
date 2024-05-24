-- Drop and create index on Song table for ArtistID
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Song_ArtistID')
BEGIN
    DROP INDEX idx_Song_ArtistID ON Song;
END
CREATE INDEX idx_Song_ArtistID ON Song (ArtistID);

-- Drop and create index on Album table for ArtistID
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Album_ArtistID')
BEGIN
    DROP INDEX idx_Album_ArtistID ON Album;
END
CREATE INDEX idx_Album_ArtistID ON Album (ArtistID);

-- Drop and create index on Playlist table for AuthorID
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Playlist_AuthorID')
BEGIN
    DROP INDEX idx_Playlist_AuthorID ON Playlist;
END
CREATE INDEX idx_Playlist_AuthorID ON Playlist (AuthorID);

-- Drop and create index on Song table for Genre
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Song_Genre')
BEGIN
    DROP INDEX idx_Song_Genre ON Song;
END
CREATE INDEX idx_Song_Genre ON Song (Genre);

-- Drop and create index on Playlist table for Genre
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Playlist_Genre')
BEGIN
    DROP INDEX idx_Playlist_Genre ON Playlist;
END
CREATE INDEX idx_Playlist_Genre ON Playlist (Genre);
