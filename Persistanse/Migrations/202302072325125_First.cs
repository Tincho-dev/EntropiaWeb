namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Letras", "Name", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Letras", "Name", c => c.String(nullable: false));
        }
    }
}
