using System;
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
        private Dictionary<int?, Album> albums = new Dictionary<int?, Album>();
        private Dictionary<int, Playlist> playlists = new Dictionary<int, Playlist>();
        private ImageList tabImageList;
        private List<Song> songsWithoutAlbum_global = new List<Song>();
        private Dictionary<int, String> userMap = new Dictionary<int, String>();

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
            InitializeImageList();
            InitializeTabControl();
            LoadSongsWithoutAlbum();
            FillUserMap();
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
            InitializeImageList();
            InitializeTabControl();
        }

        private void InitializeImageList()
        {
            tabImageList = new ImageList();
            tabImageList.Images.Add("songs", Properties.Resources.songs); // Replace with your actual resource name
            tabImageList.Images.Add("albums", Properties.Resources.album); // Replace with your actual resource name
            tabImageList.Images.Add("artists", Properties.Resources.artist); // Replace with your actual resource name
            tabImageList.Images.Add("playlists", Properties.Resources.playlist); // Replace with your actual resource name
            tabImageList.Images.Add("leaderboard", Properties.Resources.leaderboard); // Replace with your actual resource name
        }

        private void loadSongs()
        {
            try
            {
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox17.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox17.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox17.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox14.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox14.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox14.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox18.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox18.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox18.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox27.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox27.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox27.DropDownStyle = ComboBoxStyle.DropDown;

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
                        }

                            dataGridView1.DataSource = songs.Values.ToList();
                            dataGridView1.Columns[2].HeaderText = "Artist";
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                var artistID = row.Cells["songArtist"].Value.ToString();
                                int artistID_int = Int32.Parse(artistID);
                                if (artists.TryGetValue(artistID_int, out var artistName))
                                {
                                    row.Cells["songArtist"].Value = artistName;
                                }
                            }
                        reader.Close(); // Ensure reader is closed

                        // Add songs to the combobox
                        comboBox4.DataSource = songs.Values.ToList();
                        comboBox4.DisplayMember = "songName";
                        comboBox4.ValueMember = "SongID";

                        List<Song> stupid_song_list = songs.Values.ToList();
                        stupid_song_list.Insert(
                            0,
                            new Song { SongID = -1, songName = "Select song" }
                        );
                        comboBox17.DataSource = stupid_song_list;
                        // comboBox17.DisplayMember = "songName";
                        // comboBox17.ValueMember = "songName";
                        comboBox27.DataSource = stupid_song_list;
                        comboBox27.DisplayMember = "songName";
                        comboBox27.ValueMember = "SongID";
                        // comboBox14.DataSource = stupid_song_list;
                        // comboBox14.DisplayMember = "songName";
                        // comboBox14.ValueMember = "songName";

                        // comboBox18.DataSource = stupid_song_list;
                        // comboBox18.DataSource = stupid_song_list;
                        // comboBox18.DisplayMember = "songName";
                        // comboBox18.ValueMember = "SongID";
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
                comboBox20.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox20.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox20.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox21.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox21.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox21.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox16.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox16.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox16.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox26.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox26.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox26.DropDownStyle = ComboBoxStyle.DropDown;

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
                            album.albumArtist = (int?)reader["ArtistID"];
                            album.albumDuration = reader["TotalDuration"].ToString();
                            album.albumReleaseDate = (DateTime)reader["ReleaseDate"];

                            albums[album.albumID] = album;

                        }
                        List<Album> temp_list = albums.Values.ToList();
                        temp_list.Insert(
                            0,
                            new Album { albumID = -1, albumName = "Please Select an Album" }
                        );
                        comboBox9.DataSource = temp_list;
                        comboBox9.DisplayMember = "albumName";
                        comboBox9.ValueMember = "albumID";
                        dataGridView2.DataSource = albums.Values.ToList();
                        reader.Close();
                        List<Album> stupid_album_list = albums.Values.ToList();
                        stupid_album_list.Insert(
                            0,
                            new Album { albumID = -1, albumName = "Please Select an Album" }
                        );
                        comboBox20.DataSource = stupid_album_list;
                        comboBox20.DisplayMember = "albumName";
                        comboBox20.ValueMember = "albumID";
                        comboBox16.DataSource = stupid_album_list;
                        comboBox16.DisplayMember = "albumName";
                        comboBox16.ValueMember = "albumID";
                        comboBox26.DataSource = stupid_album_list;
                        comboBox26.DisplayMember = "albumName";
                        comboBox26.ValueMember = "albumID";


                        comboBox21.DataSource = getAlbumsByArtist(
                            artists.Values.ToList()[0].artistID
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private List<Album> getAlbumsByArtist(int artistID)
        {
            List<Album> artistAlbums = new List<Album>();
            foreach (Album album in albums.Values)
            {
                if (album.albumArtist == artistID)
                {
                    artistAlbums.Add(album);
                }
            }
            return artistAlbums;
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
                comboBox22.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox22.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox22.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox15.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox15.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox15.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox13.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox13.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox13.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox25.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox25.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox25.DropDownStyle = ComboBoxStyle.DropDown;

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
                        }

                        dataGridView3.DataSource = artists.Values.ToList();
                        reader.Close();

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
                        List<Artist> stupid_artist_list = artists.Values.ToList();
                        stupid_artist_list.Insert(
                            0,
                            new Artist { artistID = -1, artistName = "Please Select an Artist" }
                        );
                        comboBox13.DataSource = stupid_artist_list;
                        comboBox13.DisplayMember = "artistName";
                        comboBox13.ValueMember = "artistID";
                        comboBox6.DataSource = stupid_artist_list;
                        comboBox6.DisplayMember = "artistName";
                        comboBox6.ValueMember = "artistID";
                        comboBox11.DataSource = artists.Values.ToList();
                        comboBox11.DisplayMember = "artistName";
                        comboBox11.ValueMember = "artistID";
                        comboBox22.DataSource = artists.Values.ToList();
                        comboBox22.DisplayMember = "artistName";
                        comboBox22.ValueMember = "artistID";
                        comboBox15.DataSource = artists.Values.ToList();
                        comboBox15.DisplayMember = "artistName";
                        comboBox15.ValueMember = "artistID";
                        comboBox25.DataSource = artists.Values.ToList();
                        comboBox25.DisplayMember = "artistName";
                        comboBox25.ValueMember = "artistID";
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
                comboBox23.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox23.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox23.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox19.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox19.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox19.DropDownStyle = ComboBoxStyle.DropDown;
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
                        playlists.Clear();

                        // Read each playlist and add it to the listbox
                        while (reader.Read())
                        {
                            // Creating a playlist object
                            Playlist playlist = new Playlist();
                            playlist.playlistID = (int)reader["ID"];
                            playlist.playlistName = reader["Name"].ToString();
                            playlist.genre = reader["Genre"].ToString();
                            playlist.visibility = (bool)reader["Visibility"];
                            playlist.totalDuration = (int)reader["TotalDuration"];
                            playlist.authorID = (int)reader["AuthorID"];
                            playlists[playlist.playlistID] = playlist;

                        }
                        dataGridView4.DataSource = playlists.Values.ToList();


                        List<Playlist> stupid_playlist_list = playlists.Values.ToList();
                        stupid_playlist_list.Insert(
                            0,
                            new Playlist { playlistID = -1, playlistName = "Please Select a Playlist" }
                        );
                        comboBox23.DataSource = stupid_playlist_list;
                        comboBox23.DisplayMember = "playlistName";
                        comboBox23.ValueMember = "playlistID";
                        comboBox19.DataSource = stupid_playlist_list;
                        comboBox19.DisplayMember = "playlistName";
                        comboBox19.ValueMember = "playlistID";
                        reader.Close();
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
            ) =>
            { /* Add Song Logic */
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
            ) =>
            { /* Add Album Logic */
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
            ) =>
            { /* Add Artist Logic */
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
            ) =>
            { /* Create Playlist Logic */
            };
            playlistsTab.Controls.Add(createPlaylistButton);

            ListBox playlistListBox = new ListBox();
            playlistListBox.Location = new Point(10, 50);
            playlistListBox.Size = new Size(300, 200);
            playlistsTab.Controls.Add(playlistListBox);
        }

        private void InitializeLeaderboardTab()
        {
        }

        private void InitializeTabControl()
        {
            mainTabControl.ImageList = tabImageList;

            // Set the ImageIndex for each TabPage
            songsTab.ImageIndex = 0; // Index of the icon in the ImageList
            albumsTab.ImageIndex = 1; // Index of the icon in the ImageList
            artistsTab.ImageIndex = 2; // Index of the icon in the ImageList
            playlistsTab.ImageIndex = 3; // Index of the icon in the ImageList
            leaderboardTab.ImageIndex = 4; // Index of the icon in the ImageList
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
                        reader.Close();
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
                        reader.Close();
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
            int? songArtist;
            try
            {
                songArtist = (int?)comboBox1.SelectedValue;
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
            int? songAlbumID = 1; //can be null

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
                songAlbumID = null;
            }

            if (radioButton1.Checked) //part of album
            {
                // n sei bem o que fazer aqui
                songAlbumID = (int?)comboBox1.SelectedValue;
            }

            //string insertCommand = "INSERT INTO Song (ID, ArtistID, Streams, Genre, Duration, Lyrics, Name, ReleaseDate, AlbumID) " +
            //                          "VALUES (@ID, @ArtistID, @Streams, @Genre, @Duration, @Lyrics, @Name, @ReleaseDate, @AlbumID)";




            // we have to make sure that the album id is from the selected artist
            if (albums[songAlbumID].albumArtist != songArtist)
            {
                MessageBox.Show("Album ID must be from the selected artist.");
                return;
            }

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
            // This should later be the sum of durations of all songs in the album
            int albumDuration = 10;
            DateTime albumReleaseDate = DateTime.Now;

            // Ensure the artist combobox has a selected value
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Please select an artist.");
                return;
            }

            int artistID = (int)comboBox2.SelectedValue;

            // Validate inputs
            if (string.IsNullOrEmpty(albumName))
            {
                MessageBox.Show("Please enter an album name.");
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
                        // Optionally, refresh the album list or other UI components
                        loadAlbums();
                    }
                    else
                    {
                        MessageBox.Show("Error adding album to the database.");
                    }

                    // Clear the input fields
                    textBox7.Text = "";
                    comboBox2.SelectedIndex = -1; // Reset combobox selection
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

        private void button1_Click(object sender, EventArgs e) {

            Song selectedSong = (Song)dataGridView1.CurrentRow.DataBoundItem;
            int songID = selectedSong.SongID;
            int playlistID = 1; //liked songs id
            AddSongToPlaylist(songID, playlistID);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox21.DataSource = getAlbumsByArtist((int)comboBox1.SelectedValue);
        }

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
                string selectCommand =
                    "SELECT Name, Genre, ReleaseDate, Duration, ArtistID, Lyrics FROM Song WHERE ID = @SongID";

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
                        reader.Close();

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

                List<Playlist> playlists_filter = new List<Playlist>();

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Genre", genre);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        dataGridView4.DataSource = null;

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

                            playlists_filter.Add(playlist);
                        }
                        reader.Close();
                        dataGridView4.DataSource = playlists_filter;
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

                List<Playlist> playlists_filter = new List<Playlist>();

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@Visibility", visibility);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear the listbox
                        playlists.Clear();

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

                            playlists_filter.Add(playlist);
                        }
                        reader.Close();
                        dataGridView4.DataSource = playlists_filter;
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

                        // Read each song and add it to the datagridview
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
                        reader.Close();

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
            if (comboBox6.SelectedItem != null && comboBox6.SelectedIndex != 0)
            {
                int artistID = (int)comboBox6.SelectedValue;

                // Example: Filter songs by the selected artist ID
                FilterSongsByArtistID(artistID);
            }
        }

        public void loadArtistLeaderboard(int topN)
        {
            try
            {
                // Query string to call the UDF with a parameter
                string query = "SELECT * FROM dbo.GetTopArtists(@TopN)";

                List<Artist> artists_filter = new List<Artist>();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    // Adding the parameter required by the UDF
                    cmd.Parameters.AddWithValue("@TopN", topN);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Reading data from the UDF result set
                        while (reader.Read())
                        {
                            Artist artist = new Artist
                            {
                                artistID = (int)reader["ID"],
                                artistName = reader["ArtistName"].ToString(),
                                streams = (int)reader["Streams"]
                            };

                            artists_filter.Add(artist);
                        }
                        reader.Close();
                    }
                }

                // Set the DataSource outside the loop
                dataGridView5.DataSource = artists_filter;
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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

                List<Song> songs_filter = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TopN", topN);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        songs_filter.Clear();

                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                SongID = (int)reader["ID"],
                                songName = reader["Name"].ToString(),
                                streams = (int)reader["Streams"]
                                // Include any other fields as needed
                            };

                            songs_filter.Add(song);
                        }
                        reader.Close();
                    }
                }

                dataGridView6.DataSource = songs_filter;
                dataGridView6.Columns["songArtist"].Visible = false;
                dataGridView6.Columns["songGenre"].Visible = false;
                dataGridView6.Columns["songDuration"].Visible = false;
                dataGridView6.Columns["songLyrics"].Visible = false;
                dataGridView6.Columns["songReleaseDate"].Visible = false;
                dataGridView6.Columns["songAlbumID"].Visible = false;
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
                                albumArtist = (int)reader["ArtistID"],
                                albumDuration = reader["TotalDuration"].ToString(),
                                albumReleaseDate = (DateTime)reader["ReleaseDate"]
                            };

                            albums_filter.Add(album);
                        }
                        reader.Close();

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

        private void label21_Click(object sender, EventArgs e) { }

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
                string updateCommand =
                    "UPDATE Song SET Name = @Name, Genre = @Genre, ReleaseDate = @ReleaseDate, Duration = @Duration, ArtistID = @ArtistID, Lyrics=@Lyrics WHERE ID = @SongID";

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
                        // loadSongs();

                        textBox8.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        richTextBox2.Text = string.Empty;
                        comboBox4.SelectedIndex = -1;
                        comboBox3.SelectedIndex = -1;
                        dateTimePicker3.Value = DateTime.Now; // Reset to current date
                        loadSongs();
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

        private void artistList_SelectedIndexChanged_1(object sender, EventArgs e) { }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Artist selectedArtist = (Artist)dataGridView3.CurrentRow.DataBoundItem;
            MessageBox.Show($"You selected {selectedArtist.artistName}");
        }

        private void PopulateAlbumDetails(int albumID)
        {
            try
            {
                // Construct the SQL SELECT command to get the album details
                string selectCommand =
                    "SELECT Name, ReleaseDate, TotalDuration, ArtistID FROM Album WHERE ID = @AlbumID";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@AlbumID", albumID);


                    using (SqlDataReader reader1 = cmd.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            // Populate the controls with the album details
                            textBox15.Text = reader1["Name"].ToString();
                            dateTimePicker5.Value = (DateTime)reader1["ReleaseDate"];
                            comboBox15.SelectedValue = (int)reader1["ArtistID"];
                        }
                        else
                        {
                            //MessageBox.Show("Album not found.");
                        }
                        reader1.Close(); // Ensure reader is closed
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erroraa: " + ex.Message);
            }
        }


        private void PopulatePlaylistDetails(int playlistID)
        {
            try
            {
                // Construct the SQL SELECT command to get the playlist details
                string selectCommand =
                    "SELECT Name, Genre, Visibility, TotalDuration, AuthorID FROM Playlist WHERE ID = @PlaylistID";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistID", playlistID);

                    using (SqlDataReader reader1 = cmd.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            // Populate the controls with the playlist details
                            textBox19.Text = reader1["Name"].ToString();
                            textBox18.Text = reader1["Genre"].ToString();
                            int visibility = (bool)reader1["Visibility"] ? 1 : 0;
                            if (visibility == 0)
                            {
                                radioButton6.Checked = true;
                                radioButton5.Checked = false;
                            }
                            else
                            {
                                radioButton5.Checked = true;
                                radioButton6.Checked = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Playlist not found.");
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


        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox16.SelectedValue is int selectedAlbumID)
            {
                int artistID = (int)comboBox15.SelectedValue;
                comboBox14.DataSource = songsWithoutAlbum_global.Where(song => song.songArtist == artistID.ToString()).ToList();
                PopulateAlbumDetails(selectedAlbumID);
            }
        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox17.SelectedValue != null && comboBox17.SelectedIndex != 0)
            {
                // Get the selected song
                Song selectedSong = (Song)comboBox17.SelectedItem;
                dataGridView1.DataSource = new List<Song> { selectedSong };
            }
            else if (comboBox17.SelectedIndex == 0)
            {
                dataGridView1.DataSource = songs.Values.ToList();
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e) { }

        private void label36_Click(object sender, EventArgs e) { }

        private void textBox11_TextChanged(object sender, EventArgs e) { }

        private void label31_Click(object sender, EventArgs e) { }

        private void textBox9_TextChanged(object sender, EventArgs e) { }

        private void label1_Click_1(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label2_Click_1(object sender, EventArgs e) { }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button8_Click(object sender, EventArgs e) { }

        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if (comboBox17.SelectedValue != null && comboBox17.SelectedIndex != 0)
            // {
            //     // Get the selected song
            //     Song selectedSong = (Song)comboBox17.SelectedItem;
            //     dataGridView1.DataSource = new List<Song> { selectedSong };
            // }
            // else if (comboBox17.SelectedIndex == 0)
            // {
            //     dataGridView1.DataSource = songs.Values.ToList();
            // }
            if (comboBox20.SelectedIndex != 0)
            {
                int albumID = (int)comboBox20.SelectedValue;
                LoadAlbumSongs(albumID);
            }
        }

        private void LoadAlbumSongs(int? AlbumID)
        {
            // uses stored procedure to get all songs from an album and loads it into a List<Song> the stored procedure is GetSongsByAlbumID

            try
            {
                // Query string to call the stored procedure
                string query = "EXEC dbo.GetSongsByAlbumID @AlbumID";

                List<Song> songs_album = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    // Adding the parameter required by the stored procedure
                    cmd.Parameters.AddWithValue("@AlbumID", AlbumID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        songs_album.Clear();

                        // Reading data from the stored procedure result set
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
                            songs_album.Add(song);
                        }
                        reader.Close();
                        comboBox10.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox10.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBox10.DropDownStyle = ComboBoxStyle.DropDown;

                        dataGridView2.DataSource = songs_album;
                        dataGridView7.DataSource = songs_album;
                        comboBox10.DataSource = songs_album;
                        comboBox10.DisplayMember = "songName";
                        comboBox10.ValueMember = "songID";
                    }
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadSongsWithoutAlbum()
        {
            try
            {
                // Query string to call the stored procedure
                string query = "EXEC GetSongsWithoutAlbum";

                List<Song> songsWithoutAlbum = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        songsWithoutAlbum.Clear();

                        // Reading data from the stored procedure result set
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
                                songAlbumID = null,
                                streams = (int)reader["Streams"]
                            };
                            // before adding the song to the list, check if the song is of the artist selected in the combobox15
                            // MessageBox.Show("Song Artist is " + song.songArtist + " and combobox15 value is " + comboBox15.SelectedValue.ToString());
                            //if (song.songArtist == comboBox15.SelectedValue.ToString())
                            //{
                            //    songsWithoutAlbum.Add(song);
                            //}
                            songsWithoutAlbum.Add(song);
                            songsWithoutAlbum_global.Add(song);
                        }
                        reader.Close();
                        comboBox14.DataSource = songsWithoutAlbum;
                        comboBox14.DisplayMember = "songName";
                        comboBox14.ValueMember = "SongID";
                    }
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // private List<Song> LoadSongsWithoutAlbum()
        // {
        //     List<Song> songsWithoutAlbum = new List<Song>();

        //     try
        //     {
        //         // Query string to call the stored procedure
        //         string query = "EXEC GetSongsWithoutAlbum";

        //         using (SqlCommand cmd = new SqlCommand(query, conn))
        //         {
        //             // Setting the command type to Text
        //             cmd.CommandType = CommandType.Text;

        //             using (SqlDataReader reader = cmd.ExecuteReader())
        //             {
        //                 songsWithoutAlbum.Clear();

        //                 // Reading data from the stored procedure result set
        //                 while (reader.Read())
        //                 {
        //                     Song song = new Song
        //                     {
        //                         SongID = (int)reader["ID"],
        //                         songName = reader["Name"].ToString(),
        //                         songArtist = reader["ArtistID"].ToString(),
        //                         songGenre = reader["Genre"].ToString(),
        //                         songDuration = reader["Duration"].ToString(),
        //                         songLyrics = reader["Lyrics"].ToString(),
        //                         songReleaseDate = (DateTime)reader["ReleaseDate"],
        //                         songAlbumID = null,
        //                         streams = (int)reader["Streams"]
        //                     };
        //                     songsWithoutAlbum.Add(song);
        //                 }
        //                 reader.Close();
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Displaying an error message in case of an exception
        //         MessageBox.Show("Error: " + ex.Message);
        //     }

        //     return songsWithoutAlbum;
        // }



        private void button21_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = albums.Values.ToList();
        }

        private void tabPage1_Click(object sender, EventArgs e) { }

        private void label68_Click(object sender, EventArgs e) { }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox21.Visible = true;
        }

        private void textBox20_TextChanged(object sender, EventArgs e) { }

        private void label69_Click(object sender, EventArgs e) { }

        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e) { }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox21.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e) { }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the new values from the text boxes and comboboxes
                int albumID = (int)comboBox4.SelectedValue;
                string albumName = textBox15.Text;
                DateTime albumReleaseDate = dateTimePicker5.Value;
                int artistID = (int)comboBox15.SelectedValue;

                // Construct the SQL UPDATE command
                string updateCommand =
                    "UPDATE Album SET Name = @Name, ReleaseDate = @ReleaseDate, ArtistID = @ArtistID WHERE ID = @AlbumID";

                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    // Set the parameters
                    cmd.Parameters.AddWithValue("@Name", albumName);
                    cmd.Parameters.AddWithValue("@ReleaseDate", albumReleaseDate);
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);
                    cmd.Parameters.AddWithValue("@AlbumID", albumID);

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Album updated successfully!");
                        // Optionally, refresh the list of albums
                        // loadAlbums();

                        textBox15.Text = string.Empty;
                        comboBox16.SelectedIndex = -1;
                        comboBox15.SelectedIndex = -1;
                        dateTimePicker5.Value = DateTime.Now; // Reset to current date
                        loadAlbums();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the album.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Get the selected artist ID and new name from the combobox and textbox
                int artistID = (int)comboBox22.SelectedValue;
                string newArtistName = textBox16.Text;

                // Validate the new artist name input
                if (string.IsNullOrEmpty(newArtistName))
                {
                    MessageBox.Show("Artist name cannot be empty.");
                    return;
                }

                // Construct the SQL UPDATE command
                string updateCommand = "UPDATE Artist SET ArtistName = @ArtistName WHERE ID = @ArtistID";

                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    // Set the parameters
                    cmd.Parameters.AddWithValue("@ArtistName", newArtistName);
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Artist name updated successfully!");

                        // Optionally, refresh the list of artists
                        //loadArtists();

                        textBox16.Text = string.Empty;
                        comboBox22.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the artist name.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void PopulateArtistDetails(int artistID)
        {
            try
            {
                // Construct the SQL SELECT command to get the artist details
                string selectCommand = "SELECT ArtistName FROM Artist WHERE ID = @ArtistID";

                using (SqlCommand cmd = new SqlCommand(selectCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    // Open the connection if it's not already open
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the TextBox with the artist name
                            textBox16.Text = reader["ArtistName"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Artist not found.");
                        }
                        reader.Close(); // Ensure reader is closed
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox22_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox22.SelectedValue is int selectedArtistID)
            {
                PopulateArtistDetails(selectedArtistID);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void listBoxPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Playlist selectedPlaylist = (Playlist)dataGridView4.CurrentRow.DataBoundItem;
            MessageBox.Show($"You selected {selectedPlaylist.playlistName}");
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddSongToAlbum(int songID, int albumID)
        {
            try
            {
                // Construct the SQL UPDATE command to set the AlbumID for the song
                string updateCommand = "UPDATE Song SET AlbumID = @AlbumID WHERE ID = @SongID";

                int? currentAlbumID = (int?)comboBox16.SelectedValue;

                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@AlbumID", albumID);
                    cmd.Parameters.AddWithValue("@SongID", songID);

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Song added to the album successfully!");
                        // Optionally, refresh the list of songs
                        LoadSongsWithoutAlbum();
                        LoadAlbumSongs(currentAlbumID);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add the song to the album.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void RemoveSongFromAlbum(int songID)
        {
            try
            {
                // Retrieve the current AlbumID of the song
                string selectCommand = "SELECT AlbumID FROM Song WHERE ID = @SongID";
                int? currentAlbumID = null;

                using (SqlCommand selectCmd = new SqlCommand(selectCommand, conn))
                {
                    selectCmd.Parameters.AddWithValue("@SongID", songID);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentAlbumID = reader["AlbumID"] != DBNull.Value ? (int?)reader["AlbumID"] : null;
                        }
                        reader.Close();
                    }
                }

                // Check if the current AlbumID matches the ID in comboBox16
                if (currentAlbumID == (int?)comboBox16.SelectedValue)
                {
                    // Proceed to update the AlbumID to DBNull.Value
                    string updateCommand = "UPDATE Song SET AlbumID = @AlbumID WHERE ID = @SongID";

                    using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@AlbumID", DBNull.Value);  // Use DBNull.Value to represent null
                        cmd.Parameters.AddWithValue("@SongID", songID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Song removed successfully!");
                            // Optionally, refresh the list of songs
                            LoadSongsWithoutAlbum();
                            LoadAlbumSongs(currentAlbumID);
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove the song.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The song's AlbumID does not match the selected AlbumID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            // Ensure a song is selected in the ComboBox
            if (comboBox10.SelectedItem == null)
            {
                MessageBox.Show("Please select a song to remove from the album.");
                return;
            }

            try
            {
                // Get the selected song ID from the ComboBox
                int selectedSongID = (int)comboBox10.SelectedValue;

                // Call the function to remove the song from the album
                RemoveSongFromAlbum(selectedSongID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox13_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            int artistID = (int)comboBox15.SelectedValue;
            //remove 
            comboBox14.DataSource = songsWithoutAlbum_global.Where(song => song.songArtist == artistID.ToString()).ToList();
            // combo box 14 source is the global songs without album list without the first song

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (comboBox14.SelectedValue is int selectedSongID && comboBox16.SelectedValue is int selectedAlbumID)
            {
                AddSongToAlbum(selectedSongID, selectedAlbumID);
            }
            else
            {
                MessageBox.Show("Please select both a song and an album.");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox15.SelectedValue.ToString());

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            // Get the artist details from the textboxes
            string artistName = textBox10.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(artistName))
            {
                MessageBox.Show("Please enter an artist name.");
                return;
            }

            // SQL insert command
            string insertCommand = "INSERT INTO Artist (ArtistName) VALUES (@ArtistName)";

            try
            {
                // Create a SQL command to insert an artist into the database
                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistName", artistName);

                    int queryChanged = cmd.ExecuteNonQuery();
                    if (queryChanged > 0)
                    {
                        MessageBox.Show("Artist added to the database.");
                        // Optionally, refresh the artist list or other UI components
                        loadArtists();
                    }
                    else
                    {
                        MessageBox.Show("Error adding artist to the database.");
                    }

                    // Clear the input fields
                    textBox10.Text = "";
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

        private void comboBox22_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox22.SelectedValue is int selectedArtistID)
            {
                PopulateArtistDetails(selectedArtistID);
            }
        }

        private void button22_Click_2(object sender, EventArgs e)
        {
            // Get the artist details from the textboxes
            string artistName = textBox16.Text;

            // Ensure the artist combobox has a selected value
            if (comboBox22.SelectedValue == null)
            {
                MessageBox.Show("Please select an artist.");
                return;
            }

            int artistID = (int)comboBox22.SelectedValue;

            // Validate inputs
            if (string.IsNullOrEmpty(artistName))
            {
                MessageBox.Show("Please enter an artist name.");
                return;
            }

            // SQL update command
            string updateCommand = "UPDATE Artist SET ArtistName = @ArtistName WHERE ID = @ArtistID";

            try
            {
                // Create a SQL command to update the artist in the database
                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistName", artistName);
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Artist updated successfully.");
                        // Optionally, refresh the artist list or other UI components
                        loadArtists();
                    }
                    else
                    {
                        MessageBox.Show("Error updating artist in the database.");
                    }

                    // Clear the input fields
                    textBox16.Text = "";
                    comboBox22.SelectedIndex = -1; // Reset combobox selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox19.SelectedValue is int selectedPlaylistID && comboBox19.SelectedIndex != 0)
            {
                int PlaylistID = (int)comboBox19.SelectedValue;
                PopulatePlaylistDetails(PlaylistID);
                loadPlaylistSongs(PlaylistID);
                // List<Song> box18src = comboBox18.DataSource as List<Song>;
                // we have to remove the songs that are already in the playlist from the combobox
                // ! TODO diogu 
                // List<Song> songs_playlist = dataGridView8.DataSource as List<Song>;
                // comboBox18.DataSource = stupid_song_list;
                // comboBox18.DisplayMember = "songName";
                // comboBox18.ValueMember = "SongID";
                changeBox18();
                // List<Song> playlistSongs = dataGridView8.DataSource as List<Song>;


                // List<Song> box18src = songs.Values.ToList();

                // foreach (Song song in playlistSongs)
                // {
                //     box18src.Remove(song);
                // }
                // box18src.Insert(0, new Song { SongID = 0, songName = "Select a song" });

                // comboBox18.DataSource = box18src;
                // comboBox18.DisplayMember = "songName";
                // comboBox18.ValueMember = "SongID";
            }
        }

        private void changeBox18()
        {
            List<Song> playlistSongs = dataGridView8.DataSource as List<Song>;
            List<Song> box18src = songs.Values.ToList();

            foreach (Song song in playlistSongs)
            {
                box18src.Remove(song);
            }
            box18src.Insert(0, new Song { SongID = 0, songName = "Select a song" });

            comboBox18.DataSource = box18src;
            comboBox18.DisplayMember = "songName";
            comboBox18.ValueMember = "SongID";
        }
        private void FillUserMap()
        {
            //use the stored procedure GetAllUsers to fill the user map
            try
            {
                comboBox12.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox12.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox12.DropDownStyle = ComboBoxStyle.DropDown;
                // Query string to call the stored procedure
                string query = "EXEC GetAllUsers";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        userMap.Clear();

                        // Reading data from the stored procedure result set
                        while (reader.Read())
                        {
                            userMap.Add((int)reader["ID"], reader["Username"].ToString());
                        }
                        comboBox12.DataSource = new BindingSource(userMap, null);
                        comboBox12.DisplayMember = "Value";
                        comboBox12.ValueMember = "Key";
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox12.SelectedValue.ToString());
        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void loadPlaylistSongs(int PlaylistID)
        {
            try
            {
                // Query string to call the UDF
                comboBox24.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox24.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox24.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox23.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox23.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox23.DropDownStyle = ComboBoxStyle.DropDown;
                string query = "SELECT * FROM dbo.GetSongsInPlaylist(@PlaylistID)";

                List<Song> songs_playlist = new List<Song>();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    // Adding the parameter required by the UDF
                    cmd.Parameters.AddWithValue("@PlaylistID", PlaylistID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        songs_playlist.Clear();

                        // Reading data from the UDF result set
                        while (reader.Read())
                        {
                            //get the song from the global list of songs
                            Song song = songs[(int)reader["SongID"]];
                            songs_playlist.Add(song);
                        }
                        reader.Close();
                        dataGridView8.DataSource = songs_playlist;
                        comboBox24.DataSource = songs_playlist;
                        comboBox24.DisplayMember = "songName";
                        comboBox24.ValueMember = "SongID";
                    }
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (comboBox18.SelectedIndex != 0 && comboBox19.SelectedIndex != 0)
            {
                int songID = (int)comboBox18.SelectedValue;
                int playlistID = (int)comboBox19.SelectedValue;
                AddSongToPlaylist(songID, playlistID);
            }
            else
            {
                MessageBox.Show("Please select a song to add to the playlist.");
            }
        }

        private void AddSongToPlaylist(int songID, int playlistID)
        {
            try
            {
                // Construct the SQL SELECT command to check if the song is already in the playlist
                string checkCommand = "SELECT COUNT(*) FROM PlaylistSong WHERE PlaylistID = @PlaylistID AND SongID = @SongID";

                using (SqlCommand checkCmd = new SqlCommand(checkCommand, conn))
                {
                    checkCmd.Parameters.AddWithValue("@PlaylistID", playlistID);
                    checkCmd.Parameters.AddWithValue("@SongID", songID);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("The song is already in the playlist.");
                        return;
                    }
                }

                // Construct the SQL INSERT command to add the song to the playlist
                string insertCommand = "INSERT INTO PlaylistSong (PlaylistID, SongID) VALUES (@PlaylistID, @SongID)";

                using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistID", playlistID);
                    cmd.Parameters.AddWithValue("@SongID", songID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Song added to 'Liked Songs'!");
                        // Optionally, refresh the list of songs in the playlist
                        loadPlaylistSongs(playlistID);
                        changeBox18();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add the song to the playlist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
            int songID = (int)comboBox24.SelectedValue;
            int playlistID = (int)comboBox19.SelectedValue;
            RemovePlaylistSong(playlistID, songID);
        }

        private void RemovePlaylistSong(int playlistID, int songID)
        {
            //uses the stored procedure to delete a song, the song procedure

            try
            {
                // Query string to call the stored procedure
                string query = "EXEC DeletePlaylistSong @PlaylistID, @SongID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    // Adding the parameter required by the stored procedure
                    cmd.Parameters.AddWithValue("@PlaylistID", playlistID);
                    cmd.Parameters.AddWithValue("@SongID", songID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Song removed from the playlist successfully!");
                        // Optionally, refresh the list of songs in the playlist
                        loadPlaylistSongs(playlistID);
                        changeBox18();
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the song from the playlist.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // this is for editing a playlist
            if (comboBox19.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a playlist to edit.");
                return;
            }
            try
            {
                // Get the new values from the text boxes and comboboxes
                int playlistID = (int)comboBox19.SelectedValue;
                string playlistName = textBox19.Text;
                string playlistGenre = textBox18.Text;
                int visibility = radioButton5.Checked ? 1 : 0;
                int authorID = (int)comboBox12.SelectedValue;

                // Construct the SQL UPDATE command
                string updateCommand =
                    "UPDATE Playlist SET Name = @Name, AuthorId = @authorID, Genre = @Genre, Visibility = @Visibility WHERE ID = @PlaylistID";

                using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                {
                    // Set the parameters
                    cmd.Parameters.AddWithValue("@Name", playlistName);
                    cmd.Parameters.AddWithValue("@Genre", playlistGenre);
                    cmd.Parameters.AddWithValue("@Visibility", visibility);
                    cmd.Parameters.AddWithValue("@PlaylistID", playlistID);
                    cmd.Parameters.AddWithValue("@authorID", authorID);

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Playlist updated successfully!");
                        // Optionally, refresh the list of playlists
                        loadPlaylists();

                        textBox19.Text = string.Empty;
                        textBox18.Text = string.Empty;
                        radioButton5.Checked = false;
                        radioButton6.Checked = false;
                        comboBox19.SelectedIndex = 0;
                        comboBox12.SelectedIndex = 0;
                        dataGridView6.DataSource = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the playlist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void comboBox9_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox9.SelectedIndex != 0)
            {
                int albumID = (int)comboBox9.SelectedValue;
                List<Album> temp = new List<Album>();
                temp.Add(albums[albumID]);
                dataGridView2.DataSource = temp;
            } else if (comboBox9.SelectedIndex == 0)
            {
                dataGridView2.DataSource = albums.Values.ToList();
            }
        }

        private void comboBox13_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            if (comboBox13.SelectedIndex != 0)
            {
                int artistID = (int)comboBox13.SelectedValue;
                List<Artist> temp = new List<Artist>();
                temp.Add(artists[artistID]);
                dataGridView3.DataSource = temp;
            } else if (comboBox13.SelectedIndex == 0)
            {
                dataGridView3.DataSource = artists.Values.ToList();
            }
        }

        private void comboBox23_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox23.SelectedIndex != 0)
            {
                int PlaylistID = (int)comboBox23.SelectedValue;
                List<Playlist> temp = new List<Playlist>();
                temp.Add(playlists[PlaylistID]);
                dataGridView4.DataSource = temp;
            } else if (comboBox23.SelectedIndex == 0)
            {
                dataGridView4.DataSource = playlists.Values.ToList();
            }
            
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void comboBox25_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_3(object sender, EventArgs e)
        {
            // Get the artist ID from a relevant input (e.g., a combobox)
            if (comboBox25.SelectedValue == null)
            {
                MessageBox.Show("Please select an artist to remove.");
                return;
            }

            int artistID = (int)comboBox25.SelectedValue;

            // SQL delete command
            string deleteCommand = "DELETE FROM Artist WHERE ID = @ArtistID";

            try
            {
                // Create a SQL command to delete an artist from the database
                using (SqlCommand cmd = new SqlCommand(deleteCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Artist removed from the database.");
                        // Optionally, refresh the artist list or other UI components
                        loadArtists();
                    }
                    else
                    {
                        MessageBox.Show("Error removing artist from the database.");
                    }

                    // Clear the input fields
                    comboBox25.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            // Ensure the album combobox has a selected value
            if (comboBox26.SelectedValue == null)
            {
                MessageBox.Show("Please select an album to delete.");
                return;
            }

            int albumID = (int)comboBox26.SelectedValue;

            // uses DeleteAlbumWithSongs procedure to delete an album

            try
            {
                // Query string to call the stored procedure
                string query = "EXEC DeleteAlbumWithSongs @AlbumID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Setting the command type to Text
                    cmd.CommandType = CommandType.Text;

                    // Adding the parameter required by the stored procedure
                    cmd.Parameters.AddWithValue("@AlbumID", albumID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    
                        MessageBox.Show("Album removed from the database.");
                        // Optionally, refresh the list of albums
                        loadAlbums();

                    // Clear the input fields
                    comboBox26.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                // Displaying an error message in case of an exception
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                // Construct the SQL DELETE command to remove the song

                if (comboBox27.SelectedValue == null)
                {
                    MessageBox.Show("Please select a song to delete.");
                    return;
                }

                int songID = (int)comboBox27.SelectedValue;

                string deleteCommand = "DELETE FROM Song WHERE ID = @SongID";

                using (SqlCommand cmd = new SqlCommand(deleteCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@SongID", songID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Song removed successfully!");
                        // Optionally, refresh the list of songs or other UI components
                        loadSongs();
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the song.");
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
