namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactorcode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artist", "FirstName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Artist", "LastName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artist", "LastName", c => c.String());
            AlterColumn("dbo.Artist", "FirstName", c => c.String());
        }
    }
}
