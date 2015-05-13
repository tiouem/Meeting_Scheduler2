namespace Meeting_Scheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Room", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "Image");
        }
    }
}
