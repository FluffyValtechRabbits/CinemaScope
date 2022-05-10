namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieTypes");
        }
    }
}
