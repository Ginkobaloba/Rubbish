namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        MoneyOwed = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "UserID" });
            DropTable("dbo.Customers");
        }
    }
}
