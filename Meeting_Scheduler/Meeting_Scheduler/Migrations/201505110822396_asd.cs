namespace Meeting_Scheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Username = c.String(maxLength: 20),
                        Password = c.String(maxLength: 80),
                        Position = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
