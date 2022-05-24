namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImdbIdToMovieModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImdbId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ImdbId");
        }
    }
}
