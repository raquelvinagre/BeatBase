namespace BeatBaseForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.songsTab = new System.Windows.Forms.TabPage();
            this.listBoxSongs = new System.Windows.Forms.ListBox();
            this.albumsTab = new System.Windows.Forms.TabPage();
            this.buttonAddAlbum = new System.Windows.Forms.Button();
            this.listBoxAlbums = new System.Windows.Forms.ListBox();
            this.artistsTab = new System.Windows.Forms.TabPage();
            this.artistList = new System.Windows.Forms.ListBox();
            this.playlistsTab = new System.Windows.Forms.TabPage();
            this.buttonCreatePlaylist = new System.Windows.Forms.Button();
            this.listBoxPlaylists = new System.Windows.Forms.ListBox();
            this.profileTab = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonSaveProfile = new System.Windows.Forms.Button();
            this.leaderboardTab = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewLeaderboard = new System.Windows.Forms.DataGridView();
            this.buttonAddArtist = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.songsTab.SuspendLayout();
            this.albumsTab.SuspendLayout();
            this.artistsTab.SuspendLayout();
            this.playlistsTab.SuspendLayout();
            this.profileTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.leaderboardTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeaderboard)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.mainTabControl.Controls.Add(this.songsTab);
            this.mainTabControl.Controls.Add(this.albumsTab);
            this.mainTabControl.Controls.Add(this.artistsTab);
            this.mainTabControl.Controls.Add(this.playlistsTab);
            this.mainTabControl.Controls.Add(this.profileTab);
            this.mainTabControl.Controls.Add(this.leaderboardTab);
            this.mainTabControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.mainTabControl.Multiline = true;
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(600, 366);
            this.mainTabControl.TabIndex = 0;
            // 
            // songsTab
            // 
            this.songsTab.Controls.Add(this.button3);
            this.songsTab.Controls.Add(this.button2);
            this.songsTab.Controls.Add(this.listBoxSongs);
            this.songsTab.Location = new System.Drawing.Point(4, 25);
            this.songsTab.Margin = new System.Windows.Forms.Padding(2);
            this.songsTab.Name = "songsTab";
            this.songsTab.Size = new System.Drawing.Size(592, 337);
            this.songsTab.TabIndex = 0;
            this.songsTab.Text = "Songs";
            this.songsTab.Click += new System.EventHandler(this.songsTab_Click);
            // 
            // listBoxSongs
            // 
            this.listBoxSongs.Location = new System.Drawing.Point(16, 15);
            this.listBoxSongs.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxSongs.Name = "listBoxSongs";
            this.listBoxSongs.Size = new System.Drawing.Size(198, 160);
            this.listBoxSongs.TabIndex = 1;
            // 
            // albumsTab
            // 
            this.albumsTab.Controls.Add(this.buttonAddAlbum);
            this.albumsTab.Controls.Add(this.listBoxAlbums);
            this.albumsTab.Location = new System.Drawing.Point(4, 25);
            this.albumsTab.Margin = new System.Windows.Forms.Padding(2);
            this.albumsTab.Name = "albumsTab";
            this.albumsTab.Size = new System.Drawing.Size(592, 337);
            this.albumsTab.TabIndex = 1;
            this.albumsTab.Text = "Albums";
            // 
            // buttonAddAlbum
            // 
            this.buttonAddAlbum.Location = new System.Drawing.Point(274, 136);
            this.buttonAddAlbum.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddAlbum.Name = "buttonAddAlbum";
            this.buttonAddAlbum.Size = new System.Drawing.Size(118, 19);
            this.buttonAddAlbum.TabIndex = 0;
            this.buttonAddAlbum.Text = "addAlbum";
            this.buttonAddAlbum.Click += new System.EventHandler(this.buttonAddAlbum_Click);
            // 
            // listBoxAlbums
            // 
            this.listBoxAlbums.Location = new System.Drawing.Point(132, 64);
            this.listBoxAlbums.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxAlbums.Name = "listBoxAlbums";
            this.listBoxAlbums.Size = new System.Drawing.Size(91, 69);
            this.listBoxAlbums.TabIndex = 1;
            // 
            // artistsTab
            // 
            this.artistsTab.Controls.Add(this.artistList);
            this.artistsTab.Location = new System.Drawing.Point(4, 25);
            this.artistsTab.Margin = new System.Windows.Forms.Padding(2);
            this.artistsTab.Name = "artistsTab";
            this.artistsTab.Size = new System.Drawing.Size(592, 337);
            this.artistsTab.TabIndex = 2;
            this.artistsTab.Text = "Artists";
            // 
            // artistList
            // 
            this.artistList.Location = new System.Drawing.Point(177, 24);
            this.artistList.Margin = new System.Windows.Forms.Padding(2);
            this.artistList.Name = "artistList";
            this.artistList.Size = new System.Drawing.Size(91, 69);
            this.artistList.TabIndex = 1;
            // 
            // playlistsTab
            // 
            this.playlistsTab.Controls.Add(this.buttonCreatePlaylist);
            this.playlistsTab.Controls.Add(this.listBoxPlaylists);
            this.playlistsTab.Location = new System.Drawing.Point(4, 25);
            this.playlistsTab.Margin = new System.Windows.Forms.Padding(2);
            this.playlistsTab.Name = "playlistsTab";
            this.playlistsTab.Size = new System.Drawing.Size(592, 337);
            this.playlistsTab.TabIndex = 3;
            this.playlistsTab.Text = "Playlists";
            // 
            // buttonCreatePlaylist
            // 
            this.buttonCreatePlaylist.Location = new System.Drawing.Point(256, 150);
            this.buttonCreatePlaylist.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreatePlaylist.Name = "buttonCreatePlaylist";
            this.buttonCreatePlaylist.Size = new System.Drawing.Size(148, 19);
            this.buttonCreatePlaylist.TabIndex = 0;
            this.buttonCreatePlaylist.Text = "createPlaylist";
            this.buttonCreatePlaylist.Click += new System.EventHandler(this.buttonCreatePlaylist_Click);
            // 
            // listBoxPlaylists
            // 
            this.listBoxPlaylists.Location = new System.Drawing.Point(122, 100);
            this.listBoxPlaylists.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxPlaylists.Name = "listBoxPlaylists";
            this.listBoxPlaylists.Size = new System.Drawing.Size(91, 69);
            this.listBoxPlaylists.TabIndex = 1;
            // 
            // profileTab
            // 
            this.profileTab.Controls.Add(this.button1);
            this.profileTab.Controls.Add(this.listBox2);
            this.profileTab.Controls.Add(this.label4);
            this.profileTab.Controls.Add(this.listBox1);
            this.profileTab.Controls.Add(this.label3);
            this.profileTab.Controls.Add(this.label2);
            this.profileTab.Controls.Add(this.textBox1);
            this.profileTab.Controls.Add(this.label1);
            this.profileTab.Controls.Add(this.pictureBox1);
            this.profileTab.Controls.Add(this.labelName);
            this.profileTab.Controls.Add(this.textBoxName);
            this.profileTab.Controls.Add(this.buttonSaveProfile);
            this.profileTab.Location = new System.Drawing.Point(4, 25);
            this.profileTab.Margin = new System.Windows.Forms.Padding(2);
            this.profileTab.Name = "profileTab";
            this.profileTab.Size = new System.Drawing.Size(592, 337);
            this.profileTab.TabIndex = 4;
            this.profileTab.Text = "Profile";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 216);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 19);
            this.button1.TabIndex = 11;
            this.button1.Text = "Edit Profile";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(463, 91);
            this.listBox2.Margin = new System.Windows.Forms.Padding(2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(91, 69);
            this.listBox2.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Favorite Songs";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(270, 91);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(91, 69);
            this.listBox1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(223, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Playlists";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nome";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(270, 48);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Antónia Pereira";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 4;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(26, 33);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 147);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(75, 19);
            this.labelName.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(0, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(76, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // buttonSaveProfile
            // 
            this.buttonSaveProfile.Location = new System.Drawing.Point(0, 0);
            this.buttonSaveProfile.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveProfile.Name = "buttonSaveProfile";
            this.buttonSaveProfile.Size = new System.Drawing.Size(56, 19);
            this.buttonSaveProfile.TabIndex = 2;
            // 
            // leaderboardTab
            // 
            this.leaderboardTab.Controls.Add(this.label5);
            this.leaderboardTab.Controls.Add(this.dataGridViewLeaderboard);
            this.leaderboardTab.Location = new System.Drawing.Point(4, 25);
            this.leaderboardTab.Margin = new System.Windows.Forms.Padding(2);
            this.leaderboardTab.Name = "leaderboardTab";
            this.leaderboardTab.Size = new System.Drawing.Size(592, 337);
            this.leaderboardTab.TabIndex = 5;
            this.leaderboardTab.Text = "Leaderboard";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Top Global Songs";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dataGridViewLeaderboard
            // 
            this.dataGridViewLeaderboard.ColumnHeadersHeight = 29;
            this.dataGridViewLeaderboard.Location = new System.Drawing.Point(110, 65);
            this.dataGridViewLeaderboard.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewLeaderboard.Name = "dataGridViewLeaderboard";
            this.dataGridViewLeaderboard.RowHeadersWidth = 51;
            this.dataGridViewLeaderboard.Size = new System.Drawing.Size(180, 122);
            this.dataGridViewLeaderboard.TabIndex = 0;
            // 
            // buttonAddArtist
            // 
            this.buttonAddArtist.Location = new System.Drawing.Point(0, 0);
            this.buttonAddArtist.Name = "buttonAddArtist";
            this.buttonAddArtist.Size = new System.Drawing.Size(75, 23);
            this.buttonAddArtist.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(114, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 74);
            this.button2.TabIndex = 2;
            this.button2.Text = "Show a song";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(371, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 39);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.mainTabControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "BeatBase";
            this.mainTabControl.ResumeLayout(false);
            this.songsTab.ResumeLayout(false);
            this.albumsTab.ResumeLayout(false);
            this.artistsTab.ResumeLayout(false);
            this.playlistsTab.ResumeLayout(false);
            this.profileTab.ResumeLayout(false);
            this.profileTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.leaderboardTab.ResumeLayout(false);
            this.leaderboardTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeaderboard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage songsTab;
        private System.Windows.Forms.TabPage albumsTab;
        private System.Windows.Forms.TabPage artistsTab;
        private System.Windows.Forms.TabPage playlistsTab;
        private System.Windows.Forms.TabPage profileTab;
        private System.Windows.Forms.TabPage leaderboardTab;
        private System.Windows.Forms.Button buttonAddAlbum;
        private System.Windows.Forms.Button buttonAddArtist;
        private System.Windows.Forms.ListBox artistList;
        private System.Windows.Forms.Button buttonCreatePlaylist;
        private System.Windows.Forms.Button buttonSaveProfile;
        private System.Windows.Forms.ListBox listBoxSongs;
        private System.Windows.Forms.ListBox listBoxAlbums;
        private System.Windows.Forms.ListBox listBoxPlaylists;
        private System.Windows.Forms.DataGridView dataGridViewLeaderboard;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
