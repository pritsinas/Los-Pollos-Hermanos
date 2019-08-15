namespace LosPollosHermanos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCancelledPropertyToShipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipments", "IsCancelled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipments", "IsCancelled");
        }
    }
}
