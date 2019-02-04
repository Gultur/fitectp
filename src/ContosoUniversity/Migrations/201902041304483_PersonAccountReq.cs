namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonAccountReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Password", c => c.String());
            AlterColumn("dbo.Person", "Login", c => c.String());
        }
    }
}
