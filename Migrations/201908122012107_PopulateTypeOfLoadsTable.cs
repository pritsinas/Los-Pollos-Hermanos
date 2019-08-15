namespace LosPollosHermanos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTypeOfLoadsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TypeOfLoads (Id, Name) VALUES (1, 'Chicken')");
            Sql("INSERT INTO TypeOfLoads (Id, Name) VALUES (2, 'Potatoes')");
            Sql("INSERT INTO TypeOfLoads (Id, Name) VALUES (3, 'Oil')");
            Sql("INSERT INTO TypeOfLoads (Id, Name) VALUES (4, 'Salt')");
            Sql("INSERT INTO TypeOfLoads (Id, Name) VALUES (5, 'Sauce')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM TypeOfLoads WHERE Id IN (1, 2, 3, 4, 5)");
        }
    }
}
