// <auto-generated />
namespace MovieService.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class AddImdbIdToMovieModel : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddImdbIdToMovieModel));
        
        string IMigrationMetadata.Id
        {
            get { return "202205241708411_AddImdbIdToMovieModel"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
