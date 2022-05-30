namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedMovieModelAccordingToImdbData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Year", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Budget", c => c.String());
            AlterColumn("dbo.Movies", "BoxOffice", c => c.String());
            AlterColumn("dbo.Movies", "RatingIMDb", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "RatingIMDb", c => c.Double());
            AlterColumn("dbo.Movies", "BoxOffice", c => c.Int());
            AlterColumn("dbo.Movies", "Budget", c => c.Int());
            AlterColumn("dbo.Movies", "Year", c => c.Int(nullable: false));
        }
    }
}
