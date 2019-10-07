namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNullPropertyOnReservationDataModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "IsCanceled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Reservations", "IsBought", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "IsBought", c => c.Boolean());
            AlterColumn("dbo.Reservations", "IsCanceled", c => c.Boolean());
        }
    }
}
