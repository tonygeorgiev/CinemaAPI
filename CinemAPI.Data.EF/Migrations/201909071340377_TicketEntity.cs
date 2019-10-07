namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProjectionId = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        MovieName = c.String(),
                        CinemaName = c.String(),
                        Row = c.Byte(nullable: false),
                        Column = c.Byte(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tickets");
        }
    }
}
