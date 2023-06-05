namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Actors", new[] { "MovieId" });
            AlterColumn("dbo.Actors", "MovieId", c => c.Int());
            CreateIndex("dbo.Actors", "MovieId");
            DropColumn("dbo.Movies", "ActorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "ActorId", c => c.Int(nullable: false));
            DropIndex("dbo.Actors", new[] { "MovieId" });
            AlterColumn("dbo.Actors", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Actors", "MovieId");
        }
    }
}
