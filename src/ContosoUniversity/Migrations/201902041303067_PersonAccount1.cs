namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonAccount1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "Login", c => c.String());
            AlterColumn("dbo.Person", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Login", c => c.String(nullable: false));
        }
    }
}
