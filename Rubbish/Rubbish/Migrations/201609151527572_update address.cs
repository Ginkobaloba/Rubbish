namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "RouteNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "RouteNumber");
        }
    }
}
