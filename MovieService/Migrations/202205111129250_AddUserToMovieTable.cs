namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToMovieTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserToMovies",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.Int(nullable: false),
                        IsLiked = c.Boolean(nullable: false),
                        IsWatched = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.MovieId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserToMovies");
        }
    }
}
