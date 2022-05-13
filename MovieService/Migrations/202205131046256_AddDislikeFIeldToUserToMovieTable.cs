namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDislikeFIeldToUserToMovieTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserToMovies", "IsDisLiked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserToMovies", "IsDisLiked");
        }
    }
}
