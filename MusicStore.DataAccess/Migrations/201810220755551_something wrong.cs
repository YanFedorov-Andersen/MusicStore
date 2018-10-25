namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somethingwrong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artist", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Genre", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Genre", "Discriminator");
            DropColumn("dbo.Artist", "Discriminator");
        }
    }
}
