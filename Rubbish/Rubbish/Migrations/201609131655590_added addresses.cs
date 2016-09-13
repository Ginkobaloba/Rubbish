namespace Rubbish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaddresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StreetNumber = c.String(),
                        StreetName = c.String(),
                        State = c.String(),
                        ZipCodeID = c.String(),
                        CityID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Addresses");
        }
    }
}
