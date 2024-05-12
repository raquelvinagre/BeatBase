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

namespace BeatBaseForms
{
    public partial class Form1 : Form
    {

        private SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            checkDBConn();

            //InitializeComponents();  
        }

        private SqlConnection getSqlConn()
        {
            // Needs to be changed to the online server
            return new SqlConnection("data source = DIOGU\\SQLEXPRESS;integrated security=true;initial catalog=Beatbase");
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
            profileTab = new TabPage("Profile");
            leaderboardTab = new TabPage("Leaderboard");

            // Add tabs to TabControl
            mainTabControl.Controls.Add(songsTab);
            mainTabControl.Controls.Add(albumsTab);
            mainTabControl.Controls.Add(artistsTab);
            mainTabControl.Controls.Add(playlistsTab);
            mainTabControl.Controls.Add(profileTab);
            mainTabControl.Controls.Add(leaderboardTab);

            // Add TabControl to the form
            this.Controls.Add(mainTabControl);

            // Call specialized initialization methods
            InitializeSongsTab();
            InitializeAlbumsTab();
            InitializeArtistsTab();
            InitializePlaylistsTab();
            InitializeProfileTab();
            InitializeLeaderboardTab();
        }

        private void InitializeSongsTab()
        {
            Button addButton = new Button();
            addButton.Text = "Add Song";
            addButton.Click += (sender, e) => { /* Add Song Logic */ };
            songsTab.Controls.Add(addButton);
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

            Button saveProfileButton = new Button();
            saveProfileButton.Text = "Save Changes";
            saveProfileButton.Location = new Point(100, 40);
            saveProfileButton.Size = new Size(100, 30);
            saveProfileButton.Click += (sender, e) => { /* Save Profile Logic */ };
            profileTab.Controls.Add(saveProfileButton);
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
            try
            {
                // Define a static song
                int ID = 1; // Assuming the song ID in your database
                string songName = "My Song";
                int artistID = 1; // Assuming the artist ID in your database
                int streams = 0;
                string genre = "Pop";
                int duration = 4; // Song duration, e.g., 4 minutes
                string lyrics = "These are the lyrics of my song.";
                DateTime releaseDate = DateTime.Now;
                int albumID = 1; // Assuming the album ID in your database

                // Create SQL command to insert the song into the database
                string insertCommand = "INSERT INTO Song (ID, ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) " +
                                       "VALUES (@ID, @ArtistID, @Streams, @Genre, @Duration, @Lyrics, @Name, @ReleaseDate, @AlbumID)";

                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    // Set parameters for the SQL command
                    cmd.Parameters.AddWithValue("@ID", ID); // ID is auto-incremented in the database
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);
                    cmd.Parameters.AddWithValue("@Streams", streams);
                    cmd.Parameters.AddWithValue("@Genre", genre);
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@Lyrics", lyrics);
                    cmd.Parameters.AddWithValue("@Name", songName);
                    cmd.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                    cmd.Parameters.AddWithValue("@AlbumID", albumID);

                    // Open connection and execute the SQL command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Song added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add song.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
