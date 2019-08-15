namespace LosPollosHermanos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDriverToShipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipments", "DriverId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Shipments", "DriverId");
            AddForeignKey("dbo.Shipments", "DriverId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "DriverId", "dbo.AspNetUsers");
            DropIndex("dbo.Shipments", new[] { "DriverId" });
            DropColumn("dbo.Shipments", "DriverId");
        }
    }
}
