namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketIdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "CenaKarte_Id", "dbo.PriceOfTickets");
            DropIndex("dbo.Tickets", new[] { "CenaKarte_Id" });
            RenameColumn(table: "dbo.Tickets", name: "CenaKarte_Id", newName: "PriceOfTicketId");
            AlterColumn("dbo.Tickets", "PriceOfTicketId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "PriceOfTicketId");
            AddForeignKey("dbo.Tickets", "PriceOfTicketId", "dbo.PriceOfTickets", "Id", cascadeDelete: true);
            DropColumn("dbo.Tickets", "PriceOfTicket");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "PriceOfTicket", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tickets", "PriceOfTicketId", "dbo.PriceOfTickets");
            DropIndex("dbo.Tickets", new[] { "PriceOfTicketId" });
            AlterColumn("dbo.Tickets", "PriceOfTicketId", c => c.Int());
            RenameColumn(table: "dbo.Tickets", name: "PriceOfTicketId", newName: "CenaKarte_Id");
            CreateIndex("dbo.Tickets", "CenaKarte_Id");
            AddForeignKey("dbo.Tickets", "CenaKarte_Id", "dbo.PriceOfTickets", "Id");
        }
    }
}
