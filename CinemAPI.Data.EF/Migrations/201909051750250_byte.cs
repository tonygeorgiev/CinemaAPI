namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _byte : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projections", "AvailableSeatsCount", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projections", "AvailableSeatsCount", c => c.Int(nullable: false));
        }
    }
}
