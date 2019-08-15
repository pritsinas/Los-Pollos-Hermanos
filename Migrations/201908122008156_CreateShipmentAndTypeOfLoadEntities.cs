namespace LosPollosHermanos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateShipmentAndTypeOfLoadEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        TypeOfLoadId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfLoads", t => t.TypeOfLoadId, cascadeDelete: true)
                .Index(t => t.TypeOfLoadId);
            
            CreateTable(
                "dbo.TypeOfLoads",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "TypeOfLoadId", "dbo.TypeOfLoads");
            DropIndex("dbo.Shipments", new[] { "TypeOfLoadId" });
            DropTable("dbo.TypeOfLoads");
            DropTable("dbo.Shipments");
        }
    }
}
