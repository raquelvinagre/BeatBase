using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace BeatBaseForms
{
    public partial class Form1 : Form
    {

        private SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            checkDBConn();
            loadSongs();
            loadAlbums();
            loadArtists();
            loadPlaylists();
            //
            //InitializeComponents();  
        }

        private SqlConnection getSqlConn()
        {
            // Needs to be changed to the online server
            return new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER, 8101; Initial Catalog = p4g6; uid = p4g6; password = euadorobasededados69$");
        }

        private bool checkDBConn()
        {
            if (conn == null)
                conn = getSqlConn();

            if (conn.State != ConnectionState.Open)
                conn.Open();

            return conn.State == ConnectionState.Open;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

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


        private void loadSongs(){

            try {
                // Create a SQL command to select all songs from the database
                string selectCommand = "SELECT * FROM Song";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        listBoxSongs.Items.Clear();

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
                            song.songReleaseDate = (System.DateTime)reader["ReleaseDate"];
                            song.songAlbumID = reader["AlbumID"] != DBNull.Value ? (int?)reader["AlbumID"] : null;
                            song.streams = (int)reader["Streams"];
                            // listBoxSongs.Items.Add(song.ToString());
                            listBoxSongs.Items.Add(song);

                            // W/o the song object
                            // string songName = reader["Name"].ToString();
                            // string artistName = reader["ArtistID"].ToString();
                            // string genre = reader["Genre"].ToString();
                            // string duration = reader["Duration"].ToString();
                            // string lyrics = reader["Lyrics"].ToString();
                            // string releaseDate = reader["ReleaseDate"].ToString();
                            // string albumID = reader["AlbumID"].ToString();
                            // listBoxSongs.Items.Add($"{songName} by {artistName}");

                        }
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
                // Create a SQL command to select all songs from the database
                string selectCommand = "SELECT * FROM Album";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        listBoxAlbums.Items.Clear();

                        // Read each song and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating an album object
                            Album album = new Album();
                            album.albumID = (int)reader["ID"];
                            album.albumName = reader["Name"].ToString();
                            album.albumArtist = reader["ArtistID"].ToString();
                            //album.albumGenre = reader["Genre"].ToString();
                            album.albumDuration = reader["TotalDuration"].ToString();
                            album.albumReleaseDate = (System.DateTime)reader["ReleaseDate"];
                            //album.streams = (int)reader["Streams"];
                            // listBoxalbums.Items.Add(album.ToString());
                            listBoxAlbums.Items.Add(album);

                            // W/o the album object
                            // string albumName = reader["Name"].ToString();
                            // string artistName = reader["ArtistID"].ToString();
                            // string genre = reader["Genre"].ToString();
                            // string duration = reader["Duration"].ToString();
                            // string lyrics = reader["Lyrics"].ToString();
                            // string releaseDate = reader["ReleaseDate"].ToString();
                            // string albumID = reader["AlbumID"].ToString();
                            // listBoxAlbums.Items.Add($"{albumName} by {artistName}");

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

                // Create a SQL command to select all artists from the database
                string selectCommand = "SELECT * FROM Artist";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    // Execute the SQL command and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        artistList.Items.Clear();

                        // Read each artist and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating an artist object
                            Artist artist = new Artist();
                            artist.artistID = (int)reader["ID"];
                            artist.artistName = reader["ArtistName"].ToString();
                            artist.streams = (int)reader["Streams"];
                            artistList.Items.Add(artist);
                        }
                        // Add artists to the combobox
                        comboBox1.DataSource = artistList.Items;
                        comboBox1.DisplayMember = "artistName";
                        comboBox1.ValueMember = "artistID";
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
                // Create a SQL command to select all playlists from the database
                string selectCommand = "SELECT * FROM Playlist";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
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

            listBoxSongs = new ListBox();
            listBoxSongs.Dock = DockStyle.Fill;
            viewSongsTab.Controls.Add(listBoxSongs);


            // Add the TabControl to the songsTab
            songsTab.Controls.Add(songsSubTabControl);

            listBoxSongs.SelectedIndexChanged += new System.EventHandler(this.listBoxSongs_SelectedIndexChanged);

        }

        private void InitializeAddSongTab(TabPage addSongTab)
        {
            Button addButton = new Button();
            addButton.Text = "Add Song";
            addButton.Location = new Point(10, 10);  // Position the button
            addButton.Size = new Size(100, 30);
            addButton.Click += (sender, e) => { /* Add Song Logic */ };
            addSongTab.Controls.Add(addButton);

            // Add additional controls for song details here, such as textboxes for song name, artist, etc.
        }


        private void InitializeAlbumsTab()
        {
            Button addAlbumButton = new Button();
            addAlbumButton.Text = "Add Album";
            addAlbumButton.Location = new Point(10, 10);  // Position the button
            addAlbumButton.Size = new Size(100, 30);
            addAlbumButton.Click += (sender, e) => { /* Add Album Logic */ };
            albumsTab.Controls.Add(addAlbumButton);

            ListView albumList = new ListView();
            albumList.Location = new Point(10, 50);
            albumList.Size = new Size(500, 300);
            albumList.View = View.List;  // Display in list view, you can change to Details for more complex data
            albumsTab.Controls.Add(albumList);
        }

        private void InitializeArtistsTab()
        {
            // Creating and setting properties of the add artist button
            Button addArtistButton = new Button();
            addArtistButton.Text = "Add Artist";
            addArtistButton.Location = new Point(10, 10);  // Position the button
            addArtistButton.Size = new Size(100, 30);
            addArtistButton.Click += (sender, e) => { /* Add Artist Logic */ };
            artistsTab.Controls.Add(addArtistButton);

            // Creating and setting properties of the list view for artists
            ListView artistList = new ListView();
            artistList.Location = new Point(10, 50);
            artistList.Size = new Size(500, 300);
            artistList.View = View.List;  // Display in list view, you can change to Details for more complex data
            artistsTab.Controls.Add(artistList);
        }


        private void InitializePlaylistsTab()
        {
            Button createPlaylistButton = new Button();
            createPlaylistButton.Text = "Create Playlist";
            createPlaylistButton.Location = new Point(10, 10);
            createPlaylistButton.Size = new Size(120, 30);
            createPlaylistButton.Click += (sender, e) => { /* Create Playlist Logic */ };
            playlistsTab.Controls.Add(createPlaylistButton);

            ListBox playlistListBox = new ListBox();
            playlistListBox.Location = new Point(10, 50);
            playlistListBox.Size = new Size(300, 200);
            playlistsTab.Controls.Add(playlistListBox);
        }


        private void InitializeLeaderboardTab()
        {
            DataGridView leaderboardDataGridView = new DataGridView();
            leaderboardDataGridView.Location = new Point(10, 10);
            leaderboardDataGridView.Size = new Size(780, 300);
            leaderboardDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Example columns
            leaderboardDataGridView.Columns.Add("Rank", "Rank");
            leaderboardDataGridView.Columns.Add("ArtistName", "Artist Name");
            leaderboardDataGridView.Columns.Add("Album", "Album");
            leaderboardDataGridView.Columns.Add("Plays", "Plays");

            leaderboardTab.Controls.Add(leaderboardDataGridView);
        }

        private void buttonCreatePlaylist_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddAlbum_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void songsTab_Click(object sender, EventArgs e)
        {

        }

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
                            MessageBox.Show($"Song Name: {songName}\nArtist Name: {artistName}\nGenre: {genre}\nDuration: {duration}\nLyrics: {lyrics}\nRelease Date: {releaseDate}\nAlbum ID: {albumID}");
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

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void artistsTab_Click(object sender, EventArgs e)
        {

        }

        private void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddAlbum_Click_1(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

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
                            MessageBox.Show($"Song Name: {songName}\nArtist Name: {artistName}\nGenre: {genre}\nDuration: {duration}\nLyrics: {lyrics}\nRelease Date: {releaseDate}\nAlbum ID: {albumID}");
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
            // Id that is autoincremented
            // int songID = 420;
            // int songArtist = 2; // This is a placeholder
            //get the artistId from the comboBox1
            int songArtist = (int)comboBox1.SelectedValue;
            string songName = textBox2.Text;
            string songGenre = textBox3.Text;
            string songDuration = textBox4.Text;
            string songLyrics = richTextBox1.Text;
            DateTime songReleaseDate = DateTime.Now;
            int songAlbumID = 0;



            // Checking if we are missing any values from the textboxes
            if (string.IsNullOrEmpty(songName) || string.IsNullOrEmpty(songGenre) || string.IsNullOrEmpty(songDuration) || string.IsNullOrEmpty(songLyrics))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Check if songDuration is a int or float
            if (!int.TryParse(songDuration, out int n) && !float.TryParse(songDuration, out float f))
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

            string insertCommand = "INSERT INTO Song (ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) " +
                           "VALUES (@ArtistID, @Streams, @Genre, @Duration, @Lyrics, @Name, @ReleaseDate, @AlbumID)";

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
                // MessageBox.Show("Error: " + ex.Message);
                // // Clear the textboxes
                // textBox2.Text = "";
                // textBox3.Text = "";
                // textBox4.Text = "";
                // richTextBox1.Text = "";

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

        private void label22_Click(object sender, EventArgs e)
        {

        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedItem != null)
            {
                Song selectedSong = (Song)listBoxSongs.SelectedItem;
                // Show streams
                MessageBox.Show($"Song: {selectedSong.songName}\nStreams: {selectedSong.streams}");
            }
  


        }

        private void button5_Click(object sender, EventArgs e)
        {

            // Get the song
            Song selectedSong = (Song)listBoxSongs.SelectedItem;
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

        private void listBoxAlbums_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBoxAlbums.SelectedItem != null)
            {
                Album selectedAlbum = (Album)listBoxAlbums.SelectedItem;
                // Show streams
                MessageBox.Show($"Album: {selectedAlbum.albumName}\n");
            }
        }

        private void artistList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (artistList.SelectedItem != null)
            {
                Artist selectedArtist = (Artist)artistList.SelectedItem;
                // Show artist details or perform any other action
                MessageBox.Show($"Artist: {selectedArtist.artistName}\nStreams: {selectedArtist.streams}");
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected file
                    string filePath = openFileDialog.FileName;

                    // Load the image into the PictureBox
                    pictureBox2.Image = Image.FromFile(filePath);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected file
                    string filePath = openFileDialog.FileName;

                    // Load the image into the PictureBox
                    pictureBox4.Image = Image.FromFile(filePath);
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected file
                    string filePath = openFileDialog.FileName;

                    // Load the image into the PictureBox
                    pictureBox4.Image = Image.FromFile(filePath);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected file
                    string filePath = openFileDialog.FileName;

                    // Load the image into the PictureBox
                    pictureBox4.Image = Image.FromFile(filePath);
                }
            }
        }

        // function to add an album to the database

        private void buttonAddAlbum_Click_2(object sender, EventArgs e)
        {
            // Get the album details from the textboxes
            string albumName = textBox7.Text;
            int albumDuration = 10; // This should later be the sum of durations of all songs in the album
            int artistID;
            DateTime albumReleaseDate = DateTime.Now;

            // Checking if we are missing any values from the textboxes
            if (string.IsNullOrEmpty(albumName) || !int.TryParse(textBox6.Text, out artistID))
            {
                MessageBox.Show("Please fill in all the fields and ensure Artist ID is a number.");
                return;
            }

            // SQL insert command
            string insertCommand = "INSERT INTO Album (Name, ReleaseDate, TotalDuration, ArtistID) " +
                                   "VALUES (@Name, @ReleaseDate, @TotalDuration, @ArtistID)";

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

                    // Clear the textboxes
                    textBox7.Text = "";
                    textBox6.Text = "";

                    // Reload the albums list
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
            string insertCommand = "INSERT INTO Artist (ArtistName, Streams) " +
                                   "VALUES (@ArtistName, @Streams)";

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
            if (string.IsNullOrEmpty(playlistName) || string.IsNullOrEmpty(genre) || !int.TryParse(textBox1.Text, out authorID))
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
            string insertCommand = "INSERT INTO Playlist (TotalDuration, Genre, Visibility, Name, AuthorID) " +
                                   "VALUES (@TotalDuration, @Genre, @Visibility, @Name, @AuthorID)";

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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // This will show every artist in the database



        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_3(object sender, EventArgs e)
        {

            int songArtist = (int)comboBox1.SelectedValue;
            MessageBox.Show(songArtist.ToString());
        }
    }
}
