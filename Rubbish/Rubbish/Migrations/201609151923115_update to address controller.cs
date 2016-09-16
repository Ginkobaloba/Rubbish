namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetoaddresscontroller : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Customer_Vacation", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Customer_Vacation", "VacationID", "dbo.Vacations");
            DropIndex("dbo.Addresses", new[] { "CustomerID" });
            DropIndex("dbo.Customer_Vacation", new[] { "VacationID" });
            DropIndex("dbo.Customer_Vacation", new[] { "CustomerID" });
            AddColumn("dbo.Customers", "AddressID", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "VacationID", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "AddressID");
            CreateIndex("dbo.Customers", "VacationID");
            AddForeignKey("dbo.Customers", "AddressID", "dbo.Addresses", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "VacationID", "dbo.Vacations", "ID", cascadeDelete: true);
            DropColumn("dbo.Addresses", "CustomerID");
            DropTable("dbo.Customer_Vacation");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Customer_Vacation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VacationID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Addresses", "CustomerID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Customers", "VacationID", "dbo.Vacations");
            DropForeignKey("dbo.Customers", "AddressID", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "VacationID" });
            DropIndex("dbo.Customers", new[] { "AddressID" });
            DropColumn("dbo.Customers", "VacationID");
            DropColumn("dbo.Customers", "AddressID");
            CreateIndex("dbo.Customer_Vacation", "CustomerID");
            CreateIndex("dbo.Customer_Vacation", "VacationID");
            CreateIndex("dbo.Addresses", "CustomerID");
            AddForeignKey("dbo.Customer_Vacation", "VacationID", "dbo.Vacations", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Customer_Vacation", "CustomerID", "dbo.Customers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers", "ID", cascadeDelete: true);
        }
    }
}
