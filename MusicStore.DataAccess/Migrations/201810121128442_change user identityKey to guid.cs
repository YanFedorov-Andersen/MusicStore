namespace MusicStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuseridentityKeytoguid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccount", "IdentityKey", c => c.Guid(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccount", "IdentityKey", c => c.String());
        }
    }
}
