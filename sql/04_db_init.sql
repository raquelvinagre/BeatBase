-- Clean the values from the tables before inserting new ones
DELETE FROM ArtistLeaderboard;
DELETE FROM BelongsTo;
DELETE FROM Song;
DELETE FROM Album;
DELETE FROM Playlist;
DELETE FROM [User];
DELETE FROM Artist;
DELETE FROM Leaderboard;
DELETE FROM GlobalLeaderboard;

-- Insert into Artist without specifying ID
INSERT INTO Artist (ArtistName, Streams) VALUES
('The Weeknd', 2839132),
('Drake', 483922),
('Taylor Swift', 7381211),
('Kendrick Lamar', 1312312),
('Billie Eilish', 5434222),
('Justin Bieber', 4324321),
('Ariana Grande', 7945834),
('Ed Sheeran', 3423121),
('Post Malone', 7428223),
('Bad Bunny', 3218712),
('Raquel Vinagre',3627),
-- New artists
('Beyonce', 8539211),
('Jay-Z', 7328821),
('Eminem', 9538120),
('Rihanna', 8739201),
('Adele', 6937200),
('Bruno Mars', 5723100),
('Lady Gaga', 6329200),
('Shakira', 4829310),
('Katy Perry', 5923182),
('Shawn Mendes', 4728192),
('Camila Cabello', 3739100),
('Cardi B', 4839211),
('Nicki Minaj', 5938122),
('J Balvin', 3829101),
('Maluma', 4928100),
('Sia', 5729182),
('Zayn', 3829102),
('Sam Smith', 5728181),
('Dua Lipa', 6829100),
('Harry Styles', 5927182),
('Halsey', 4928101),
('Khalid', 5928182);

-- Insert into [User] without specifying ID
INSERT INTO [User] (Username) VALUES
('user1'),
('user2'),
('user3'),
('user4'),
('user5'),
-- New users
('user6'),
('user7'),
('user8'),
('user9'),
('user10');

-- Insert into Playlist without specifying ID
INSERT INTO Playlist (TotalDuration, Genre, Visibility, Name, AuthorID) VALUES
(3600, 'Pop', 1, 'Liked Songs', 1),
(5400, 'Hip-Hop', 0, 'Workout Playlist', 2),
(4200, 'Electronic', 1, 'Party Mix', 3),
-- New playlists
(4800, 'R&B', 1, 'Chill Vibes', 4),
(3000, 'Rock', 0, 'Rock Anthems', 5),
(3600, 'Pop', 1, 'Top Hits', 6),
(4500, 'Hip-Hop', 0, 'Rap Caviar', 7),
(4200, 'Electronic', 1, 'EDM Hits', 8),
(3000, 'Country', 0, 'Country Roads', 9),
(4800, 'Jazz', 1, 'Smooth Jazz', 10);

-- Insert into Album without specifying ID
INSERT INTO Album (Name, ReleaseDate, TotalDuration, ArtistID, CoverImage) VALUES
('After Hours', '2020-03-20', 3600, 1, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Scorpion', '2018-06-29', 5400, 2, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Folklore', '2020-07-23', 4200, 3, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Good Kid, M.A.A.D City', '2012-10-22', 3000, 4, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('When We All Fall Asleep, Where Do We Go?', '2019-03-29', 2800, 5, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Lemonade', '2016-04-23', 3500, 12, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('4:44', '2017-06-30', 3200, 13, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Revival', '2017-12-15', 3000, 14, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('ANTI', '2016-01-28', 3100, 15, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('25', '2015-11-20', 2900, 16, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('24K Magic', '2016-11-18', 2800, 17, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Joanne', '2016-10-21', 2600, 18, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('El Dorado', '2017-05-26', 2400, 19, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Witness', '2017-06-09', 2200, 20, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Illuminate', '2016-09-23', 2000, 21, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Camila', '2018-01-12', 1800, 22, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Invasion of Privacy', '2018-04-06', 1600, 23, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Queen', '2018-08-10', 1400, 24, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Colores', '2020-03-19', 1200, 25, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('11:11', '2019-05-17', 1000, 26, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('This Is Acting', '2016-01-29', 3600, 27, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Mind of Mine', '2016-03-25', 3400, 28, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('The Thrill of It All', '2017-11-03', 3200, 29, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Future Nostalgia', '2020-03-27', 3000, 30, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Fine Line', '2019-12-13', 2800, 31, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Manic', '2020-01-17', 2600, 32, 0xFFD8FFE000104A46494600010101006000600000FFD9),
('Free Spirit', '2019-04-05', 2400, 33, 0xFFD8FFE000104A46494600010101006000600000FFD9);

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
(7, 550000, 'Pop', 240, '...', 'Thank U, Next', '2019-02-08', NULL),
-- New songs
(12, 700000, 'R&B', 260, '...', 'Formation', '2016-04-23', 6),
(12, 600000, 'R&B', 250, '...', 'Hold Up', '2016-04-23', 6),
(13, 650000, 'Hip-Hop', 300, '...', 'The Story of O.J.', '2017-06-30', 7),
(14, 800000, 'Hip-Hop', 320, '...', 'Walk on Water', '2017-12-15', 8),
(15, 750000, 'Pop', 290, '...', 'Work', '2016-01-28', 9),
(16, 900000, 'Pop', 310, '...', 'Hello', '2015-11-20', 10),
(17, 550000, 'R&B', 240, '...', '24K Magic', '2016-11-18', 11),
(18, 650000, 'Pop', 230, '...', 'Million Reasons', '2016-10-21', 12),
(19, 700000, 'Latin', 250, '...', 'Chantaje', '2017-05-26', 13),
(20, 800000, 'Pop', 280, '...', 'Chained to the Rhythm', '2017-06-09', 14),
(21, 750000, 'Pop', 220, '...', 'Treat You Better', '2016-09-23', 15),
(22, 900000, 'Pop', 210, '...', 'Havana', '2018-01-12', 16),
(23, 650000, 'Hip-Hop', 260, '...', 'I Like It', '2018-04-06', 17),
(24, 750000, 'Hip-Hop', 240, '...', 'Barbie Dreams', '2018-08-10', 18),
(25, 700000, 'Latin', 230, '...', 'Blanco', '2020-03-19', 19),
(26, 800000, 'Latin', 220, '...', 'HP', '2019-05-17', 20),
(27, 900000, 'Pop', 200, '...', 'Cheap Thrills', '2016-01-29', 21),
(28, 550000, 'R&B', 240, '...', 'PILLOWTALK', '2016-03-25', 22),
(29, 650000, 'Pop', 230, '...', 'Too Good at Goodbyes', '2017-11-03', 23),
(30, 800000, 'Pop', 300, '...', 'Watermelon Sugar', '2019-12-13', 25),
(31, 750000, 'Pop', 280, '...', 'WithoutMe', '2020-01-17', 26),
(32, 900000, 'R&B', 260, '...', 'Talk', '2019-04-05', 27);

-- Insert sample data into PlaylistSong table
INSERT INTO PlaylistSong (PlaylistID, SongID) VALUES
(1, 1), 
(1, 3), 
(1, 5),
(2, 2), 
(2, 4), 
(2, 6),
(3, 7),
(3, 9),
(3, 11),
-- New playlist-song associations
(4, 13),
(4, 15),
(4, 17),
(5, 19),
(5, 21),
(5, 23),
(6, 25),
(6, 27),
(6, 29),
(7, 31),
(7, 12),
(8, 14),
(8, 16),
(8, 18),
(9, 20),
(9, 22),
(9, 24),
(10, 26),
(10, 28),
(10, 30);

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
(5, 9),
-- New belongs to data
(1, 10),
(2, 11),
(3, 12),
(4, 13),
(5, 14),
(1, 15),
(2, 16),
(3, 17),
(4, 18),
(5, 19);

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
(5, 1, 9, 'Post Malone'),
-- New artist leaderboard data
(1, 6, 12, 'Beyonce'),
(2, 7, 13, 'Jay-Z'),
(3, 8, 14, 'Eminem'),
(4, 9, 15, 'Rihanna'),
(5, 10, 16, 'Adele');

-- Insert into GlobalLeaderboard without specifying LeaderID
INSERT INTO GlobalLeaderboard (LeaderID) VALUES
(1),
(2),
(3),
(4),
(5);
