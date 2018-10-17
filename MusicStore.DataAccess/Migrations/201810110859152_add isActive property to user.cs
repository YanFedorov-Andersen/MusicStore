namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisActivepropertytouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccount", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccount", "IsActive");
        }
    }
}
