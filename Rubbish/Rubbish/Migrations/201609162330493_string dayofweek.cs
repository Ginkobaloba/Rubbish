namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringdayofweek : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "DayOfWeek", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "DayOfWeek", c => c.Int());
        }
    }
}
