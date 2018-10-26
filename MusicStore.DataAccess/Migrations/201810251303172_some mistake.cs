namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somemistake : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Artist", "Discriminator");
            DropColumn("dbo.Genre", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genre", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Artist", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
