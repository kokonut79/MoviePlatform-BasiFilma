namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieFixActorId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ActorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ActorId");
        }
    }
}
