namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedPickups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PickupSites", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.PickupSites", "CustomerID", "dbo.Customers");
            DropIndex("dbo.PickupSites", new[] { "CustomerID" });
            DropIndex("dbo.PickupSites", new[] { "AddressID" });
            DropTable("dbo.PickupSites");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.PickupSites", "AddressID");
            CreateIndex("dbo.PickupSites", "CustomerID");
            AddForeignKey("dbo.PickupSites", "CustomerID", "dbo.Customers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PickupSites", "AddressID", "dbo.Addresses", "ID", cascadeDelete: true);
        }
    }
}
