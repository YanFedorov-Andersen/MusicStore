namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resizedecimals : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccount", "Money", c => c.Decimal(nullable: false, precision: 8, scale: 2));
            AlterColumn("dbo.BoughtSong", "BoughtPrice", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Song", "Price", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Album", "DiscountIfBuyAllSongs", c => c.Decimal(nullable: false, precision: 3, scale: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Album", "DiscountIfBuyAllSongs", c => c.Decimal(nullable: false, precision: 2, scale: 1));
            AlterColumn("dbo.Song", "Price", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.BoughtSong", "BoughtPrice", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.UserAccount", "Money", c => c.Decimal(nullable: false, precision: 6, scale: 2));
        }
    }
}
