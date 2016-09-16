namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypeupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "VacationID", "dbo.Vacations");
            DropIndex("dbo.Customers", new[] { "AddressID" });
            DropIndex("dbo.Customers", new[] { "VacationID" });
            AlterColumn("dbo.Customers", "DayOfWeek", c => c.Int());
            AlterColumn("dbo.Customers", "AddressID", c => c.Int());
            AlterColumn("dbo.Customers", "VacationID", c => c.Int());
            CreateIndex("dbo.Customers", "AddressID");
            CreateIndex("dbo.Customers", "VacationID");
            AddForeignKey("dbo.Customers", "AddressID", "dbo.Addresses", "ID");
            AddForeignKey("dbo.Customers", "VacationID", "dbo.Vacations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "VacationID", "dbo.Vacations");
            DropForeignKey("dbo.Customers", "AddressID", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "VacationID" });
            DropIndex("dbo.Customers", new[] { "AddressID" });
            AlterColumn("dbo.Customers", "VacationID", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "AddressID", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "DayOfWeek", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "VacationID");
            CreateIndex("dbo.Customers", "AddressID");
            AddForeignKey("dbo.Customers", "VacationID", "dbo.Vacations", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "AddressID", "dbo.Addresses", "ID", cascadeDelete: true);
        }
    }
}
