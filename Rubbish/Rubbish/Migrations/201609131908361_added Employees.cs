namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEmployees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        RouteNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PickupSites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayOfWeekID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        RouteNumber = c.Int(nullable: false),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vacation_Pickup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VacationID = c.Int(nullable: false),
                        PickupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PickupSites", t => t.PickupID, cascadeDelete: true)
                .ForeignKey("dbo.Vacations", t => t.VacationID, cascadeDelete: true)
                .Index(t => t.VacationID)
                .Index(t => t.PickupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacation_Pickup", "VacationID", "dbo.Vacations");
            DropForeignKey("dbo.Vacation_Pickup", "PickupID", "dbo.PickupSites");
            DropForeignKey("dbo.PickupSites", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.PickupSites", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Employees", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Vacation_Pickup", new[] { "PickupID" });
            DropIndex("dbo.Vacation_Pickup", new[] { "VacationID" });
            DropIndex("dbo.PickupSites", new[] { "AddressID" });
            DropIndex("dbo.PickupSites", new[] { "CustomerID" });
            DropIndex("dbo.Employees", new[] { "UserID" });
            DropTable("dbo.Vacation_Pickup");
            DropTable("dbo.Vacations");
            DropTable("dbo.PickupSites");
            DropTable("dbo.Employees");
        }
    }
}
