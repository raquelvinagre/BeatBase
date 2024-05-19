-- Insert into Artist without specifying ID
INSERT INTO Artist (ArtistName, Streams) VALUES
('The Weeknd', 1000000),
('Drake', 500000),
('Taylor Swift', 800000),
('Kendrick Lamar', 400000),
('Billie Eilish', 300000),
('Justin Bieber', 200000),
('Ariana Grande', 600000),
('Ed Sheeran', 700000),
('Post Malone', 450000),
('Bad Bunny', 350000);

-- Insert into [User] without specifying ID
INSERT INTO [User] (Username) VALUES
('user1'),
('user2'),
('user3'),
('user4'),
('user5');

-- Insert into Playlist without specifying ID
INSERT INTO Playlist (TotalDuration, Genre, Visibility, Name, AuthorID) VALUES
(3600, 'Pop', 1, 'My Favorite Songs', 1),
(5400, 'Hip-Hop', 0, 'Workout Playlist', 2),
(4200, 'Electronic', 1, 'Party Mix', 3);

-- Insert into Album without specifying ID
INSERT INTO Album (Name, ReleaseDate, TotalDuration, ArtistID) VALUES
('After Hours', '2020-03-20', 3600, 1),
('Scorpion', '2018-06-29', 5400, 2),
('Folklore', '2020-07-23', 4200, 3),
('Good Kid, M.A.A.D City', '2012-10-22', 3000, 4),
('When We All Fall Asleep, Where Do We Go?', '2019-03-29', 2800, 5);

-- Insert into Song without specifying ID
INSERT INTO Song (ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) VALUES
(1, 500000, 'R&B', 240, '...', 'Blinding Lights', '2020-01-21', 1),
(1, 300000, 'R&B', 200, '...', 'Heartless', '2019-11-27', 1),
(2, 400000, 'Hip-Hop', 300, '...', 'Gods Plan', '2018-01-19', 2),
(2, 200000, 'Hip-Hop', 220, '...', 'One Dance', '2016-04-05', NULL),
(3, 600000, 'Pop', 210, '...', 'Cardigan', '2020-07-23', 3),
(3, 500000, 'Pop', 230, '...', 'Shake It Off', '2014-08-18', NULL),
(4, 350000, 'Hip-Hop', 250, '...', 'Humble.', '2017-03-30', NULL),
(5, 450000, 'Pop', 200, '...', 'Bad Guy', '2019-06-05', 5),
(6, 250000, 'Pop', 220, '...', 'Sorry', '2015-10-22', NULL),
(7, 550000, 'Pop', 240, '...', 'Thank U, Next', '2019-02-08', NULL);

-- Insert into BelongsTo without changes
INSERT INTO BelongsTo (LeaderID, SongID) VALUES
(1, 1),
(1, 3),
(1, 5),
(2, 2),
(2, 4),
(3, 6),
(3, 8),
(4, 7),
(5, 9);

-- Insert into Leaderboard without specifying LeaderID
INSERT INTO Leaderboard (Size) VALUES
(10),
(5),
(8),
(3),
(7);

-- Insert into ArtistLeaderboard with generated IDs
INSERT INTO ArtistLeaderboard (LeaderID, ID, AuthorID, ArtistName) VALUES
(1, 4, 4, 'Kendrick Lamar'),
(1, 5, 5, 'Billie Eilish'),
(2, 1, 1, 'The Weeknd'),
(2, 2, 6, 'Justin Bieber'),
(3, 1, 3, 'Taylor Swift'),
(3, 2, 7, 'Ariana Grande'),
(4, 1, 8, 'Ed Sheeran'),
(5, 1, 9, 'Post Malone');

-- Insert into GlobalLeaderboard without specifying LeaderID
INSERT INTO GlobalLeaderboard (LeaderID) VALUES
(1),
(2),
(3),
(4),
(5);
