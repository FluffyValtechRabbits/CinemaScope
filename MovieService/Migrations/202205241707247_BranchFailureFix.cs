namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchFailureFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "ImdbId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "ImdbId", c => c.String());
        }
    }
}
