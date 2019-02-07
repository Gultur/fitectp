namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonWithDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lesson", "Launch", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Lesson", "Launch");
        }
    }
}
