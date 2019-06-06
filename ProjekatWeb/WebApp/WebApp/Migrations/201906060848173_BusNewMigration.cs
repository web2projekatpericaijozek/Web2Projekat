namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusNewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimetableTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Timetables", "TimetableTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Timetables", "TimetableTypeId");
            AddForeignKey("dbo.Timetables", "TimetableTypeId", "dbo.TimetableTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timetables", "TimetableTypeId", "dbo.TimetableTypes");
            DropIndex("dbo.Timetables", new[] { "TimetableTypeId" });
            DropColumn("dbo.Timetables", "TimetableTypeId");
            DropTable("dbo.TimetableTypes");
        }
    }
}
