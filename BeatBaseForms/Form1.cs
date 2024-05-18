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
            profileTab = new TabPage("Profile");


            // Add tabs to TabControl
            mainTabControl.Controls.Add(songsTab);
            mainTabControl.Controls.Add(albumsTab);
            mainTabControl.Controls.Add(artistsTab);
            mainTabControl.Controls.Add(playlistsTab);
            mainTabControl.Controls.Add(leaderboardTab);
            mainTabControl.Controls.Add(profileTab);


            // Add TabControl to the form
            this.Controls.Add(mainTabControl);

            // Call specialized initialization methods
            InitializeSongsTab();
            InitializeAlbumsTab();
            InitializeArtistsTab();
            InitializePlaylistsTab();
            InitializeLeaderboardTab();
            InitializeProfileTab();

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
                            listBoxSongs.Items.Add(song.ToString());

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

        private void InitializeProfileTab()
        {
            Label nameLabel = new Label();
            nameLabel.Text = "Name:";
            nameLabel.Location = new Point(10, 10);
            nameLabel.Size = new Size(80, 20);
            profileTab.Controls.Add(nameLabel);

            TextBox nameTextBox = new TextBox();
            nameTextBox.Location = new Point(100, 10);
            nameTextBox.Size = new Size(200, 20);
            profileTab.Controls.Add(nameTextBox);

          
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
            int songID = 420;
            int songArtist = 2; // This is a placeholder
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


            if (radioButton2.Checked)
            {
                songAlbumID = -1;
            }

            if (radioButton1.Checked)
            {
                // n sei bem o que fazer aqui
                songAlbumID = 10;
            }

            string insertCommand = "INSERT INTO Song (ID, ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) " +
                                       "VALUES (@ID, @ArtistID, @Streams, @Genre, @Duration, @Lyrics, @Name, @ReleaseDate, @AlbumID)";

            try
            {
                // Create a SQL command to insert a song into the database

                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", songID);
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

        private void buttonSaveProfile_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedItem != null)
            {
                string selectedSong = listBoxSongs.SelectedItem.ToString();
                MessageBox.Show($"Selected song: {selectedSong}");
            }
            // get the selected song for removal in a button


        }
    }
}
