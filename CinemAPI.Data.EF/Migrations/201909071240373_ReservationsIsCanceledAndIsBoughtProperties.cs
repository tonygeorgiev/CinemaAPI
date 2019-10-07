namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationsIsCanceledAndIsBoughtProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "IsCanceled", c => c.Boolean(nullable: true));
            AddColumn("dbo.Reservations", "IsBought", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "IsBought");
            DropColumn("dbo.Reservations", "IsCanceled");
        }
    }
}
