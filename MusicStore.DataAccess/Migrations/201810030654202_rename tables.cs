namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renametables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MusicAddress", newName: "Address");
            RenameTable(name: "dbo.MusicUser", newName: "UserAccount");
            RenameTable(name: "dbo.MusicBoughtSong", newName: "BoughtSong");
            RenameTable(name: "dbo.MusicSong", newName: "Song");
            RenameTable(name: "dbo.MusicAlbum", newName: "Album");
            RenameTable(name: "dbo.MusicArtist", newName: "Artist");
            RenameTable(name: "dbo.MusicGenre", newName: "Genre");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Genre", newName: "MusicGenre");
            RenameTable(name: "dbo.Artist", newName: "MusicArtist");
            RenameTable(name: "dbo.Album", newName: "MusicAlbum");
            RenameTable(name: "dbo.Song", newName: "MusicSong");
            RenameTable(name: "dbo.BoughtSong", newName: "MusicBoughtSong");
            RenameTable(name: "dbo.UserAccount", newName: "MusicUser");
            RenameTable(name: "dbo.Address", newName: "MusicAddress");
        }
    }
}
