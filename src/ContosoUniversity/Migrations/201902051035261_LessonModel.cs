namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lesson",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InstructorID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        StartHour = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.InstructorID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lesson", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.Lesson", "CourseID", "dbo.Course");
            DropIndex("dbo.Lesson", new[] { "CourseID" });
            DropIndex("dbo.Lesson", new[] { "InstructorID" });
            DropTable("dbo.Lesson");
        }
    }
}
