﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatBaseForms
{
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        private Dictionary<int, Artist> artists = new Dictionary<int, Artist>();
        private Dictionary<int, Song> songs = new Dictionary<int, Song>();
        private Dictionary<int, Album> albums = new Dictionary<int, Album>();

        public Form1()
        {
            InitializeComponent();
            checkDBConn();
            loadArtists();
            loadSongs();
            loadAlbums();
            loadPlaylists();
            loadArtistLeaderboard(5);
            LoadSongLeaderboard(5);
        }

        private SqlConnection getSqlConn()
        {
            // Needs to be changed to the online server
            return new SqlConnection(
                "data source = tcp:mednat.ieeta.pt\\SQLSERVER, 8101; Initial Catalog = p4g6; uid = p4g6; password = euadorobasededados69$"
            );
        }

        private bool checkDBConn()
        {
            if (conn == null)
                conn = getSqlConn();

            if (conn.State != ConnectionState.Open)
                conn.Open();

            return conn.State == ConnectionState.Open;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void InitializeComponents()
        {
            // Setup mainTabControl
            mainTabControl = new TabControl();
            mainTabControl.Dock = DockStyle.Fill;

            // Tabs
            songsTab = new TabPage("Songs");
            albumsTab = new TabPage("Albums");
            artistsTab = new TabPage("Artists");
            playlistsTab = new TabPage("Playlists");
            leaderboardTab = new TabPage("Leaderboard");

            // Add tabs to TabControl
            mainTabControl.Controls.Add(songsTab);
            mainTabControl.Controls.Add(albumsTab);
            mainTabControl.Controls.Add(artistsTab);
            mainTabControl.Controls.Add(playlistsTab);
            mainTabControl.Controls.Add(leaderboardTab);

            // Add TabControl to the form
            this.Controls.Add(mainTabControl);

            // Call specialized initialization methods
            InitializeSongsTab();
            InitializeAlbumsTab();
            InitializeArtistsTab();
            InitializePlaylistsTab();
            InitializeLeaderboardTab();
        }

        private void loadSongs()
        {
            try
            {
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox9.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox9.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox9.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox17.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox17.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox17.DropDownStyle = ComboBoxStyle.DropDown;

                // Create a SQL command to call the stored procedure
                string storedProcedure = "GetAllSongs";

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    // Set the command type to StoredProcedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        //Clear the dictionary
                        songs.Clear();

                        // Read each song and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating a song object
                            Song song = new Song();
                            song.SongID = (int)reader["ID"];
                            song.songName = reader["Name"].ToString();
                            song.songArtist = reader["ArtistID"].ToString();
                            song.songGenre = reader["Genre"].ToString();
                            song.songDuration = reader["Duration"].ToString();
                            song.songLyrics = reader["Lyrics"].ToString();
                            song.songReleaseDate = (DateTime)reader["ReleaseDate"];
                            song.songAlbumID =
                                reader["AlbumID"] != DBNull.Value ? (int?)reader["AlbumID"] : null;
                            song.streams = (int)reader["Streams"];
                            // add to dictionary
                            songs[song.SongID] = song;
                            dataGridView1.DataSource = songs.Values.ToList();

                            // //add columns to the datagridview
                            // dataGridView1.ColumnCount = 8;
                            // dataGridView1.Columns[0].Name = "Name";
                            // dataGridView1.Columns[1].Name = "Artist";
                            // dataGridView1.Columns[2].Name = "Genre";
                            // dataGridView1.Columns[3].Name = "Duration";
                            // dataGridView1.Columns[4].Name = "Lyrics";
                            // dataGridView1.Columns[5].Name = "Release Date";
                            // dataGridView1.Columns[6].Name = "Album ID";
                            // dataGridView1.Columns[7].Name = "Streams";
                            // //add rows to the datagridview
                            // dataGridView1.Rows.Add(song.songName, song.songArtist, song.songGenre, song.songDuration, song.songLyrics, song.songReleaseDate, song.songAlbumID, song.streams);
                        }

                        // Add songs to the combobox
                        comboBox4.DataSource = songs.Values.ToList();
                        comboBox4.DisplayMember = "songName";
                        comboBox4.ValueMember = "SongID";
                        comboBox9.DataSource = songs.Values.ToList();
                        comboBox9.DisplayMember = "songName";
                        comboBox9.ValueMember = "SongID";
                        comboBox17.DataSource = songs.Values.ToList();
                        comboBox17.DisplayMember = "songName";
                        comboBox17.ValueMember = "songName";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void loadAlbums()
        {
            try
            {
                // Create a SQL command to call the stored procedure
                string storedProcedure = "GetAllAlbums";

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    // Set the command type to StoredProcedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        albums.Clear();

                        // Read each album and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating an album object
                            Album album = new Album();
                            album.albumID = (int)reader["ID"];
                            album.albumName = reader["Name"].ToString();
                            album.albumArtist = reader["ArtistID"].ToString();
                            album.albumDuration = reader["TotalDuration"].ToString();
                            album.albumReleaseDate = (DateTime)reader["ReleaseDate"];

                            albums[album.albumID] = album;
                            dataGridView2.DataSource = albums.Values.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void loadArtists()
        {
            try
            {
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox6.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox11.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox11.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;

                // Create a SQL command to call the stored procedure
                string storedProcedure = "GetAllArtists";

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    // Set the command type to StoredProcedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the dictionary
                        artists.Clear();

                        // Read each artist and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating an artist object
                            Artist artist = new Artist();
                            artist.artistID = (int)reader["ID"];
                            artist.artistName = reader["ArtistName"].ToString();
                            artist.streams = (int)reader["Streams"];
                            artists[artist.artistID] = artist;
                            dataGridView3.DataSource = artists.Values.ToList();
                        }

                        // Add artists to the combobox
                        comboBox1.DataSource = artists.Values.ToList();
                        comboBox1.DisplayMember = "artistName";
                        comboBox1.ValueMember = "artistID";
                        comboBox2.DataSource = artists.Values.ToList();
                        comboBox2.DisplayMember = "artistName";
                        comboBox2.ValueMember = "artistID";
                        comboBox3.DataSource = artists.Values.ToList();
                        comboBox3.DisplayMember = "artistName";
                        comboBox3.ValueMember = "artistID";
                        comboBox6.DataSource = artists.Values.ToList();
                        comboBox6.DisplayMember = "artistName";
                        comboBox6.ValueMember = "artistID";
                        comboBox11.DataSource = artists.Values.ToList();
                        comboBox11.DisplayMember = "artistName";
                        comboBox11.ValueMember = "artistID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void loadPlaylists()
        {
            try
            {
                // Create a SQL command to call the stored procedure
                string storedProcedure = "GetAllPlaylists";

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    // Set the command type to StoredProcedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        listBoxPlaylists.Items.Clear();

                        // Read each playlist and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating a playlist object
                            Playlist playlist = new Playlist();
                            playlist.playlistName = reader["Name"].ToString();
                            playlist.genre = reader["Genre"].ToString();
                            playlist.visibility = (bool)reader["Visibility"];
                            playlist.totalDuration = (int)reader["TotalDuration"];
                            playlist.authorID = (int)reader["AuthorID"];
                            listBoxPlaylists.Items.Add(playlist);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InitializeSongsTab()
        {
            // Create a TabControl for the Songs tab
            TabControl songsSubTabControl = new TabControl();
            songsSubTabControl.Dock = DockStyle.Fill;

            // Create the "Add Song" tab
            TabPage addSongTab = new TabPage("Add Song");
            InitializeAddSongTab(addSongTab);

            // Create the "View Songs" tab
            TabPage viewSongsTab = new TabPage("View Songs");

            // Add the sub-tabs to the TabControl
            songsSubTabControl.Controls.Add(addSongTab);
            songsSubTabControl.Controls.Add(viewSongsTab);

            dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true; // Make the DataGridView read-only
            dataGridView1.AllowUserToAddRows = false; // Prevent adding rows
            dataGridView1.AllowUserToDeleteRows = false; // Prevent deleting rows
            dataGridView1.AllowUserToOrderColumns = true; // Allow column reordering
            dataGridView1.MultiSelect = false;

            viewSongsTab.Controls.Add(dataGridView1);

            // Add the TabControl to the songsTab
            songsTab.Controls.Add(songsSubTabControl);

            dataGridView1.SelectionChanged += new System.EventHandler(
                this.dataGridView1_SelectionChanged
            );
        }

        private void InitializeAddSongTab(TabPage addSongTab)
        {
            Button addButton = new Button();
            addButton.Text = "Add Song";
            addButton.Location = new Point(10, 10); // Position the button
            addButton.Size = new Size(100, 30);
            addButton.Click += (
                sender,
                e
            ) => { /* Add Song Logic */
            };
            addSongTab.Controls.Add(addButton);
        }

        private void InitializeAlbumsTab()
        {
            Button addAlbumButton = new Button();
            addAlbumButton.Text = "Add Album";
            addAlbumButton.Location = new Point(10, 10); // Position the button
            addAlbumButton.Size = new Size(100, 30);
            addAlbumButton.Click += (
                sender,
                e
            ) => { /* Add Album Logic */
            };
            albumsTab.Controls.Add(addAlbumButton);

            ListView albumList = new ListView();
            albumList.Location = new Point(10, 50);
            albumList.Size = new Size(500, 300);
            albumList.View = View.List; // Display in list view, you can change to Details for more complex data
            albumsTab.Controls.Add(albumList);
        }

        private void InitializeArtistsTab()
        {
            // Creating and setting properties of the add artist button
            Button addArtistButton = new Button();
            addArtistButton.Text = "Add Artist";
            addArtistButton.Location = new Point(10, 10); // Position the button
            addArtistButton.Size = new Size(100, 30);
            addArtistButton.Click += (
                sender,
                e
            ) => { /* Add Artist Logic */
            };
            artistsTab.Controls.Add(addArtistButton);

            // Creating and setting properties of the list view for artists
            ListView artistList = new ListView();
            artistList.Location = new Point(10, 50);
            artistList.Size = new Size(500, 300);
            artistList.View = View.List; // Display in list view, you can change to Details for more complex data
            artistsTab.Controls.Add(artistList);
        }

        private void InitializePlaylistsTab()
        {
            Button createPlaylistButton = new Button();
            createPlaylistButton.Text = "Create Playlist";
            createPlaylistButton.Location = new Point(10, 10);
            createPlaylistButton.Size = new Size(120, 30);
            createPlaylistButton.Click += (
                sender,
                e
            ) => { /* Create Playlist Logic */
            };
            playlistsTab.Controls.Add(createPlaylistButton);

            ListBox playlistListBox = new ListBox();
            playlistListBox.Location = new Point(10, 50);
            playlistListBox.Size = new Size(300, 200);
            playlistsTab.Controls.Add(playlistListBox);
        }

        private void InitializeLeaderboardTab()
        {
            listBox1 = new ListBox();
            listBox1.Dock = DockStyle.Fill;
            leaderboardTab.Controls.Add(listBox1);
            listBox1.SelectedIndexChanged += new System.EventHandler(
                this.listBox1_SelectedIndexChanged
            );

            listBox2 = new ListBox();
            listBox2.Dock = DockStyle.Fill;
            leaderboardTab.Controls.Add(listBox2);
            listBox2.SelectedIndexChanged += new System.EventHandler(
                this.listBox2_SelectedIndexChanged
            );
        }

        private void buttonCreatePlaylist_Click(object sender, EventArgs e) { }

        private void buttonAddAlbum_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void songsTab_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            // This makes a message box with one song from the database

            try
            {
                // Create a SQL command to select a song from the database
                string selectCommand = "SELECT TOP 1 * FROM Song";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get the song details from the reader
                            string songName = reader["Name"].ToString();
                            string artistName = reader["ArtistID"].ToString();
                            string genre = reader["Genre"].ToString();
                            string duration = reader["Duration"].ToString();
                            string lyrics = reader["Lyrics"].ToString();
                            string releaseDate = reader["ReleaseDate"].ToString();
                            string albumID = reader["AlbumID"].ToString();

                            // Display the song details in a message box
                            MessageBox.Show(
                                $"Song Name: {songName}\nArtist Name: {artistName}\nGenre: {genre}\nDuration: {duration}\nLyrics: {lyrics}\nRelease Date: {releaseDate}\nAlbum ID: {albumID}"
                            );
                        }
                        else
                        {
                            MessageBox.Show("No songs found in the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void label6_Click(object sender, EventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

        private void label9_Click(object sender, EventArgs e) { }

        private void artistsTab_Click(object sender, EventArgs e) { }

        private void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e) { }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonAddAlbum_Click_1(object sender, EventArgs e) { }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Create a SQL command to select a song from the database
                string selectCommand = "SELECT TOP 1 * FROM Song";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get the song details from the reader
                            string songName = reader["Name"].ToString();
                            string artistName = reader["ArtistID"].ToString();
                            string genre = reader["Genre"].ToString();
                            string duration = reader["Duration"].ToString();
                            string lyrics = reader["Lyrics"].ToString();
                            string releaseDate = reader["ReleaseDate"].ToString();
                            string albumID = reader["AlbumID"].ToString();

                            // Display the song details in a message box
                            MessageBox.Show(
                                $"Song Name: {songName}\nArtist Name: {artistName}\nGenre: {genre}\nDuration: {duration}\nLyrics: {lyrics}\nRelease Date: {releaseDate}\nAlbum ID: {albumID}"
                            );
                        }
                        else
                        {
                            MessageBox.Show("No songs found in the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // function to add a song to the database
        private void button3_Click_1(object sender, EventArgs e)
        {
            // Get the song details from the textboxes
            int songArtist;
            try
            {
                songArtist = (int)comboBox1.SelectedValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select an artist.");
                return;
            }
            string songName = textBox2.Text;
            string songGenre = textBox3.Text;
            string songDuration = textBox4.Text;
            string songLyrics = richTextBox1.Text;
            DateTime songReleaseDate = DateTime.Now;
            int songAlbumID = 0;

            // Checking if we are missing any values from the textboxes
            if (
                string.IsNullOrEmpty(songName)
                || string.IsNullOrEmpty(songGenre)
                || string.IsNullOrEmpty(songDuration)
                || string.IsNullOrEmpty(songLyrics)
            )
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Check if songDuration is a int or float
            if (
                !int.TryParse(songDuration, out int n) && !float.TryParse(songDuration, out float f)
            )
            {
                MessageBox.Show("Duration must be a number.");
                return;
            }

            if (radioButton2.Checked) //single
            {
                songAlbumID = -1;
            }

            if (radioButton1.Checked) //part of album
            {
                // n sei bem o que fazer aqui
                songAlbumID = 10; // ent se o album id existir, mete se como esse
            }

            //string insertCommand = "INSERT INTO Song (ID, ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) " +
            //                          "VALUES (@ID, @ArtistID, @Streams, @Genre, @Duration, @Lyrics, @Name, @ReleaseDate, @AlbumID)";

            string insertCommand =
                "INSERT INTO Song (ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) "
                + "VALUES (@ArtistID, @Streams, @Genre, @Duration, @Lyrics, @Name, @ReleaseDate, @AlbumID)";

            try
            {
                // Create a SQL command to insert a song into the database

                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    //cmd.Parameters.AddWithValue("@ID", songID);
                    cmd.Parameters.AddWithValue("@ArtistID", songArtist);
                    cmd.Parameters.AddWithValue("@Streams", 0); // Starts at 0
                    cmd.Parameters.AddWithValue("@Genre", songGenre);
                    cmd.Parameters.AddWithValue("@Duration", songDuration);
                    cmd.Parameters.AddWithValue("@Lyrics", songLyrics);
                    cmd.Parameters.AddWithValue("@Name", songName);
                    cmd.Parameters.AddWithValue("@ReleaseDate", songReleaseDate);
                    if (songAlbumID == -1)
                    {
                        cmd.Parameters.AddWithValue("@AlbumID", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AlbumID", songAlbumID);
                    }

                    int query_changed = cmd.ExecuteNonQuery();
                    if (query_changed > 0)
                    {
                        MessageBox.Show("Song added to the database.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding song to the database.");
                    }
                    // Clear the textboxes
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    richTextBox1.Text = "";

                    // Reload the songs list
                    loadSongs();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY constraint"))
                {
                    MessageBox.Show("Song already exists in the database.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label22_Click(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void button5_Click(object sender, EventArgs e)
        {
            // Get the song
            Song selectedSong = (Song)dataGridView1.CurrentRow.DataBoundItem;
            // Add a stream to the song
            string updateCommand = "UPDATE Song SET Streams = Streams + 1 WHERE ID = @ID";

            try
            {
                // Create a SQL command to update the song in the database
                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", selectedSong.SongID);

                    int query_changed = cmd.ExecuteNonQuery();
                    if (query_changed > 0)
                    {
                        MessageBox.Show("Song stream added.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding stream to the song.");
                    }

                    loadSongs();
                    loadArtists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a song.");
                return;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            // check
        }


        private void label27_Click(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e) { }

        // function to add an album to the database

        private void buttonAddAlbum_Click_2(object sender, EventArgs e)
        {
            // Get the album details from the textboxes
            string albumName = textBox7.Text;
            int albumDuration = 10; // This should later be the sum of durations of all songs in the album
            int artistID = (int)comboBox2.SelectedValue;
            DateTime albumReleaseDate = DateTime.Now;

            // SQL insert command
            string insertCommand =
                "INSERT INTO Album (Name, ReleaseDate, TotalDuration, ArtistID) "
                + "VALUES (@Name, @ReleaseDate, @TotalDuration, @ArtistID)";

            try
            {
                // Create a SQL command to insert an album into the database
                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", albumName);
                    cmd.Parameters.AddWithValue("@ReleaseDate", albumReleaseDate);
                    cmd.Parameters.AddWithValue("@TotalDuration", albumDuration);
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    int queryChanged = cmd.ExecuteNonQuery();
                    if (queryChanged > 0)
                    {
                        MessageBox.Show("Album added to the database.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding album to the database.");
                    }

                    textBox7.Text = "";
                    loadAlbums();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY constraint"))
                {
                    MessageBox.Show("Album already exists in the database.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Get the artist details from the textboxes
            string artistName = textBox10.Text;
            int streams = 0; // Initialize streams to 0 or calculate it based on your logic

            // Checking if we are missing any values from the textboxes
            if (string.IsNullOrEmpty(artistName))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // SQL insert command
            string insertCommand =
                "INSERT INTO Artist (ArtistName, Streams) " + "VALUES (@ArtistName, @Streams)";

            try
            {
                // Create a SQL command to insert an artist into the database
                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistName", artistName);
                    cmd.Parameters.AddWithValue("@Streams", streams);

                    int queryChanged = cmd.ExecuteNonQuery();
                    if (queryChanged > 0)
                    {
                        MessageBox.Show("Artist added to the database.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding artist to the database.");
                    }

                    // Clear the textboxes
                    textBox10.Text = "";

                    // Reload the artists list
                    loadArtists();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY constraint"))
                {
                    MessageBox.Show("Artist already exists in the database.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonCreatePlaylist_Click_1(object sender, EventArgs e)
        {
            // Get the playlist details from the textboxes
            string playlistName = textBox11.Text;
            string genre = textBox9.Text;
            int authorID;
            bool visibility = false;
            int totalDuration = 0; // Initialize or calculate this based on your logic

            // Checking if we are missing any values from the textboxes
            if (
                string.IsNullOrEmpty(playlistName)
                || string.IsNullOrEmpty(genre)
                || !int.TryParse(textBox1.Text, out authorID)
            )
            {
                MessageBox.Show("Please fill in all the fields and ensure Author ID is a number.");
                return;
            }

            // Set visibility based on the selected radio button
            if (radioButton3.Checked)
            {
                visibility = false; // Private
            }
            else if (radioButton4.Checked)
            {
                visibility = true; // Public
            }

            // SQL insert command
            string insertCommand =
                "INSERT INTO Playlist (TotalDuration, Genre, Visibility, Name, AuthorID) "
                + "VALUES (@TotalDuration, @Genre, @Visibility, @Name, @AuthorID)";

            try
            {
                // Create a SQL command to insert a playlist into the database
                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@TotalDuration", totalDuration);
                    cmd.Parameters.AddWithValue("@Genre", genre);
                    cmd.Parameters.AddWithValue("@Visibility", visibility);
                    cmd.Parameters.AddWithValue("@Name", playlistName);
                    cmd.Parameters.AddWithValue("@AuthorID", authorID);

                    int queryChanged = cmd.ExecuteNonQuery();
                    if (queryChanged > 0)
                    {
                        MessageBox.Show("Playlist added to the database.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding playlist to the database.");
                    }

                    // Clear the textboxes
                    textBox11.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox1.Text = "";

                    // Reload the playlists list
                    loadPlaylists();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY constraint"))
                {
                    MessageBox.Show("Playlist already exists in the database.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }

        private void textBox4_TextChanged(object sender, EventArgs e) { }

        private void AddSong_SelectedIndexChanged(object sender, EventArgs e) { }

        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }

        private void label10_Click(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e) { }

        private void textBox8_TextChanged(object sender, EventArgs e) { }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue is int selectedSongID)
            {
                PopulateSongDetails(selectedSongID);
            }
        }

        private void PopulateSongDetails(int songID)
        {
            try
            {
                // Construct the SQL SELECT command to get the song details
                string selectCommand = "SELECT Name, Genre, ReleaseDate, Duration, ArtistID, Lyrics FROM Song WHERE ID = @SongID";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@SongID", songID);

                    // Open the connection if it's not already open
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (SqlDataReader reader1 = cmd.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            // Populate the controls with the song details
                            textBox8.Text = reader1["Name"].ToString();
                            textBox6.Text = reader1["Genre"].ToString();
                            dateTimePicker3.Value = (DateTime)reader1["ReleaseDate"];
                            textBox5.Text = reader1["Duration"].ToString();
                            comboBox3.SelectedValue = (int)reader1["ArtistID"];
                            richTextBox2.Text = reader1["Lyrics"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Song not found.");
                        }
                        reader1.Close(); // Ensure reader is closed
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }

        private void FilterSongsByGenre(string genre)
        {
            try
            {
                // Call the UDF to filter songs by genre
                string selectCommand = "SELECT * FROM dbo.FilterSongsByGenre(@Genre)";

                List<Song> songs_filter = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Genre", genre);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Clear data grid view
                        dataGridView1.DataSource = null;

                        // Read each song and add it to the listbox
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongID = (int)reader["ID"],
                                songName = reader["Name"].ToString(),
                                songArtist = reader["ArtistID"].ToString(),
                                songGenre = reader["Genre"].ToString(),
                                songDuration = reader["Duration"].ToString(),
                                songLyrics = reader["Lyrics"].ToString(),
                                songReleaseDate = (DateTime)reader["ReleaseDate"],
                                songAlbumID =
                                    reader["AlbumID"] != DBNull.Value
                                        ? (int?)reader["AlbumID"]
                                        : null,
                                streams = (int)reader["Streams"]
                            };
                            songs_filter.Add(song);
                        }

                        dataGridView1.DataSource = songs_filter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FilterPlaylistsByGenre(string genre)
        {
            try
            {
                // Call the UDF to filter playlists by genre
                string selectCommand = "SELECT * FROM dbo.FilterPlaylistsByGenre(@Genre)";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Genre", genre);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        listBoxPlaylists.Items.Clear();

                        // Read each playlist and add it to the listbox
                        while (reader.Read())
                        {
                            Playlist playlist = new Playlist
                            {
                                playlistName = reader["Name"].ToString(),
                                authorID = (int)reader["AuthorID"],
                                totalDuration = (int)reader["TotalDuration"],
                                genre = reader["Genre"].ToString(),
                                visibility = (bool)reader["Visibility"]
                            };

                            listBoxPlaylists.Items.Add(playlist);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FilterPlaylistsByVisibility(bool visibility)
        {
            try
            {
                // Call the UDF to filter playlists by visibility
                string selectCommand = "SELECT * FROM dbo.FilterPlaylistsByVisibility(@Visibility)";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Visibility", visibility);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        listBoxPlaylists.Items.Clear();

                        // Read each playlist and add it to the listbox
                        while (reader.Read())
                        {
                            Playlist playlist = new Playlist
                            {
                                playlistName = reader["Name"].ToString(),
                                authorID = (int)reader["AuthorID"],
                                totalDuration = (int)reader["TotalDuration"],
                                genre = reader["Genre"].ToString(),
                                visibility = (bool)reader["Visibility"]
                            };

                            listBoxPlaylists.Items.Add(playlist);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FilterSongsByArtistID(int artistID)
        {
            try
            {
                // Call the UDF to filter songs by artist ID
                string selectCommand = "SELECT * FROM dbo.FilterSongsByArtistID(@ArtistID)";

                List<Song> songs_filter = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox

                        //Clear data grid view
                        dataGridView1.DataSource = null;

                        // Read each song and add it to the listbox
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongID = (int)reader["ID"],
                                songName = reader["Name"].ToString(),
                                songArtist = reader["ArtistID"].ToString(),
                                songGenre = reader["Genre"].ToString(),
                                songDuration = reader["Duration"].ToString(),
                                songLyrics = reader["Lyrics"].ToString(),
                                songReleaseDate = (DateTime)reader["ReleaseDate"],
                                songAlbumID =
                                    reader["AlbumID"] != DBNull.Value
                                        ? (int?)reader["AlbumID"]
                                        : null,
                                streams = (int)reader["Streams"]
                            };

                            songs_filter.Add(song);
                        }

                        dataGridView1.DataSource = songs_filter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = comboBox5.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedFilter))
            {
                FilterSongsByGenre(selectedFilter);
            }
            else
            {
                loadSongs();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem != null)
            {
                int artistID = (int)comboBox6.SelectedValue;

                // Example: Filter songs by the selected artist ID
                FilterSongsByArtistID(artistID);
            }
        }

        public void loadArtistLeaderboard(int topN)
        {
            // get the UDF GetTopArtists and load everything into listbox1

            try
            {
                // Query string to call the UDF with a parameter
                string query = "SELECT * FROM dbo.GetTopArtists(@TopN)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    // Adding the parameter required by the UDF
                    cmd.Parameters.AddWithValue("@TopN", topN);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox1.Items.Clear();

                        // Reading data from the UDF result set
                        while (reader.Read())
                        {
                            Artist artist = artists[(int)reader["ID"]];
                            listBox1.Items.Add(artist);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // this gets the number from textbox12, and then calls loadLeaderboard with that number
            int topN = 0;
            if (!int.TryParse(textBox12.Text, out topN))
            {
                MessageBox.Show("Please enter a valid number.");
                return;
            }

            loadArtistLeaderboard(topN);
        }

        public void LoadSongLeaderboard(int topN)
        {
            try
            {
                string query = "SELECT * FROM dbo.GetTopSongs(@TopN)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TopN", topN);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox2.Items.Clear();

                        while (reader.Read())
                        {
                            Song song = songs[(int)reader["ID"]];
                            listBox2.Items.Add(song);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int topN = 0;
            if (!int.TryParse(textBox13.Text, out topN))
            {
                MessageBox.Show("Please enter a valid number.");
                return;
            }

            LoadSongLeaderboard(topN);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void textBox13_TextChanged(object sender, EventArgs e) { }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = comboBox7.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedFilter))
            {
                FilterPlaylistsByGenre(selectedFilter);
            }
            else
            {
                loadPlaylists();
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            string visibility = comboBox8.SelectedItem.ToString();

            if (visibility != "")
            {
                int visibility_int = Int32.Parse(visibility);
                FilterPlaylistsByVisibility(Convert.ToBoolean(visibility_int));
            }
            else
            {
                loadPlaylists();
            }
        }

        private void FilterAlbumsByArtistID(int artistID)
        {
            try
            {
                // Call the UDF to filter albums by artist ID
                string selectCommand = "SELECT * FROM dbo.FilterAlbumsByArtistID(@ArtistID)";

                List<Album> albums_filter = new List<Album>();

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the data grid view
                        dataGridView2.DataSource = null;

                        // Read each album and add it to the list
                        while (reader.Read())
                        {
                            Album album = new Album
                            {
                                albumID = (int)reader["ID"],
                                albumName = reader["Name"].ToString(),
                                albumArtist = reader["ArtistID"].ToString(),
                                albumDuration = reader["TotalDuration"].ToString(),
                                albumReleaseDate = (DateTime)reader["ReleaseDate"]

                        };

                        albums_filter.Add(album);
                    }

                    dataGridView2.DataSource = albums_filter;
                }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox11.SelectedItem != null)
            {
                int artistID = (int)comboBox11.SelectedValue;

                // Example: Filter songs by the selected artist ID
                FilterAlbumsByArtistID(artistID);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Message box to show the selected song

            Song selectedSong2 = (Song)dataGridView1.CurrentRow.DataBoundItem;
            label21.Text = $"{selectedSong2.songName} is playing";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // if (dataGridView1.SelectedRows.Count > 0)
            // {
            //     int selectedSongId = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            //     Song selectedSong;
            //     if (songs.TryGetValue(selectedSongId, out selectedSong))
            //     {
            //         // Handle the selected song
            //         MessageBox.Show($"You selected {selectedSong.songName}");
            //     }
            // }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Album selectedAlbum = (Album)dataGridView2.CurrentRow.DataBoundItem;
            MessageBox.Show($"You selected {selectedAlbum.albumName}");
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            try
            {
                // Get the new values from the text boxes and comboboxes
                int songID = (int)comboBox4.SelectedValue; 
                string songName = textBox8.Text;
                string songGenre = textBox6.Text;
                DateTime songReleaseDate = dateTimePicker3.Value; 
                int songDuration;
                int artistID = (int)comboBox3.SelectedValue;
                string lyrics = richTextBox2.Text;

                // Validate the duration input
                if (!int.TryParse(textBox5.Text, out songDuration))
                {
                    MessageBox.Show("Duration must be a valid number.");
                    return;
                }

                // Construct the SQL UPDATE command
                string updateCommand = "UPDATE Song SET Name = @Name, Genre = @Genre, ReleaseDate = @ReleaseDate, Duration = @Duration, ArtistID = @ArtistID, Lyrics=@Lyrics WHERE ID = @SongID";

                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    // Set the parameters
                    cmd.Parameters.AddWithValue("@Name", songName);
                    cmd.Parameters.AddWithValue("@Genre", songGenre);
                    cmd.Parameters.AddWithValue("@ReleaseDate", songReleaseDate);
                    cmd.Parameters.AddWithValue("@Duration", songDuration);
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);
                    cmd.Parameters.AddWithValue("@SongID", songID);
                    cmd.Parameters.AddWithValue("@Lyrics", lyrics);

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery(); 

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Song updated successfully!");
                        // Optionally, refresh the list of songs
                        loadSongs();

                        textBox8.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        richTextBox2.Text = string.Empty;
                        comboBox4.SelectedIndex = -1;
                        comboBox3.SelectedIndex = -1;
                        dateTimePicker3.Value = DateTime.Now; // Reset to current date
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the song.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            loadSongs();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            loadAlbums();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            loadPlaylists();
        }

        private void artistList_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Artist selectedArtist = (Artist)dataGridView3.CurrentRow.DataBoundItem;
            MessageBox.Show($"You selected {selectedArtist.artistName}");
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchSong(string songName)
        {
            try
            {
                // Call the UDF to filter songs by name
                string selectCommand = "SELECT * FROM dbo.FilterSongsByName(@Name)";

                List<Song> songs_filter = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", songName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the data grid view or list box
                        dataGridView1.DataSource = null;

                        // Read each song and add it to the list
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongID = (int)reader["ID"],
                                songName = reader["Name"].ToString(),
                                songArtist = reader["ArtistID"].ToString(),
                                songGenre = reader["Genre"].ToString(),
                                songDuration = reader["Duration"].ToString(),
                                songLyrics = reader["Lyrics"].ToString(),
                                songReleaseDate = (DateTime)reader["ReleaseDate"],
                                songAlbumID =
                                    reader["AlbumID"] != DBNull.Value
                                        ? (int?)reader["AlbumID"]
                                        : null,
                                streams = (int)reader["Streams"]
                            };

                            songs_filter.Add(song);
                        }

                        dataGridView1.DataSource = songs_filter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox17.SelectedValue != null)
            {
                string selectedSongName = comboBox17.SelectedValue.ToString();
                SearchSong(selectedSongName);
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
