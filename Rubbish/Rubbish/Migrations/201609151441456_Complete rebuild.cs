namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Completerebuild : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vacation_Pickup", "PickupID", "dbo.PickupSites");
            DropForeignKey("dbo.Vacation_Pickup", "VacationID", "dbo.Vacations");
            DropIndex("dbo.Vacation_Pickup", new[] { "VacationID" });
            DropIndex("dbo.Vacation_Pickup", new[] { "PickupID" });
            CreateTable(
                "dbo.Customer_Vacation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VacationID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Vacations", t => t.VacationID, cascadeDelete: true)
                .Index(t => t.VacationID)
                .Index(t => t.CustomerID);
            
            AddColumn("dbo.Addresses", "ZipCode", c => c.String());
            AddColumn("dbo.Addresses", "City", c => c.String());
            AddColumn("dbo.Addresses", "CustomerID", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "DayOfWeek", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "IsActive", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Addresses", "CustomerID");
            AddForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers", "ID", cascadeDelete: false);
            DropColumn("dbo.Addresses", "ZipCodeID");
            DropColumn("dbo.Addresses", "CityID");
            DropTable("dbo.Vacation_Pickup");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vacation_Pickup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VacationID = c.Int(nullable: false),
                        PickupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Addresses", "CityID", c => c.String());
            AddColumn("dbo.Addresses", "ZipCodeID", c => c.String());
            DropForeignKey("dbo.Customer_Vacation", "VacationID", "dbo.Vacations");
            DropForeignKey("dbo.Customer_Vacation", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Customer_Vacation", new[] { "CustomerID" });
            DropIndex("dbo.Customer_Vacation", new[] { "VacationID" });
            DropIndex("dbo.Addresses", new[] { "CustomerID" });
            DropColumn("dbo.Customers", "IsActive");
            DropColumn("dbo.Customers", "DayOfWeek");
            DropColumn("dbo.Addresses", "CustomerID");
            DropColumn("dbo.Addresses", "City");
            DropColumn("dbo.Addresses", "ZipCode");
            DropTable("dbo.Customer_Vacation");
            CreateIndex("dbo.Vacation_Pickup", "PickupID");
            CreateIndex("dbo.Vacation_Pickup", "VacationID");
            AddForeignKey("dbo.Vacation_Pickup", "VacationID", "dbo.Vacations", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Vacation_Pickup", "PickupID", "dbo.PickupSites", "ID", cascadeDelete: true);
        }
    }
}
