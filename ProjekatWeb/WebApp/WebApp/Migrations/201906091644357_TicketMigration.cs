namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Checked = c.Boolean(nullable: false),
                        Tip = c.String(),
                        VaziDo = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        PriceOfTicket = c.Int(nullable: false),
                        CenaKarte_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.PriceOfTickets", t => t.CenaKarte_Id)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CenaKarte_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "CenaKarte_Id", "dbo.PriceOfTickets");
            DropForeignKey("dbo.Tickets", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "CenaKarte_Id" });
            DropIndex("dbo.Tickets", new[] { "ApplicationUserId" });
            DropTable("dbo.Tickets");
        }
    }
}
