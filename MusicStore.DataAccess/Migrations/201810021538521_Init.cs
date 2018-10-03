namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Continent = c.String(maxLength: 20),
                        Country = c.String(maxLength: 20),
                        City = c.String(maxLength: 40),
                        Street = c.String(maxLength: 40),
                        House = c.String(maxLength: 10),
                        Flat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusicUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        Money = c.Decimal(nullable: false, precision: 6, scale: 2),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MusicAddress", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.MusicBoughtSong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoughtPrice = c.Decimal(nullable: false, precision: 3, scale: 2),
                        BoughtDate = c.DateTime(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        Song_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MusicSong", t => t.Song_Id)
                .ForeignKey("dbo.MusicUser", t => t.User_Id)
                .Index(t => t.Song_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MusicSong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 3, scale: 2),
                        Album_Id = c.Int(),
                        Artist_Id = c.Int(),
                        Genre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MusicAlbum", t => t.Album_Id)
                .ForeignKey("dbo.MusicArtist", t => t.Artist_Id)
                .ForeignKey("dbo.MusicGenre", t => t.Genre_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.MusicAlbum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        DiscountIfBuyAllSongs = c.Decimal(nullable: false, precision: 2, scale: 1),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusicArtist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusicGenre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicBoughtSong", "User_Id", "dbo.MusicUser");
            DropForeignKey("dbo.MusicSong", "Genre_Id", "dbo.MusicGenre");
            DropForeignKey("dbo.MusicBoughtSong", "Song_Id", "dbo.MusicSong");
            DropForeignKey("dbo.MusicSong", "Artist_Id", "dbo.MusicArtist");
            DropForeignKey("dbo.MusicSong", "Album_Id", "dbo.MusicAlbum");
            DropForeignKey("dbo.MusicUser", "Address_Id", "dbo.MusicAddress");
            DropIndex("dbo.MusicSong", new[] { "Genre_Id" });
            DropIndex("dbo.MusicSong", new[] { "Artist_Id" });
            DropIndex("dbo.MusicSong", new[] { "Album_Id" });
            DropIndex("dbo.MusicBoughtSong", new[] { "User_Id" });
            DropIndex("dbo.MusicBoughtSong", new[] { "Song_Id" });
            DropIndex("dbo.MusicUser", new[] { "Address_Id" });
            DropTable("dbo.MusicGenre");
            DropTable("dbo.MusicArtist");
            DropTable("dbo.MusicAlbum");
            DropTable("dbo.MusicSong");
            DropTable("dbo.MusicBoughtSong");
            DropTable("dbo.MusicUser");
            DropTable("dbo.MusicAddress");
        }
    }
}
