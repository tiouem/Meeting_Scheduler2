namespace Meeting_Scheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        Meeting_Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 10, fixedLength: true),
                        Description = c.String(maxLength: 50, unicode: false),
                        Room_Id = c.Int(),
                        Date = c.DateTime(),
                        Duration = c.Int(),
                    })
                .PrimaryKey(t => t.Meeting_Id)
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Room_Id = c.Int(nullable: false),
                        Type = c.String(maxLength: 50, unicode: false),
                        Capacity = c.Int(),
                        Projector = c.Boolean(),
                        FlipChart = c.Boolean(),
                        Phone = c.Boolean(),
                        Camera = c.Boolean(),
                    })
                .PrimaryKey(t => t.Room_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meeting", "Room_Id", "dbo.Room");
            DropIndex("dbo.Meeting", new[] { "Room_Id" });
            DropTable("dbo.Room");
            DropTable("dbo.Meeting");
        }
    }
}
