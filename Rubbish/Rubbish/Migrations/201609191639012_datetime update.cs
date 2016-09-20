namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vacations", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Vacations", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vacations", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vacations", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
