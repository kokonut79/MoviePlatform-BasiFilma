namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "StudioId", "dbo.Studios");
            DropIndex("dbo.Movies", new[] { "StudioId" });
            AlterColumn("dbo.Movies", "StudioId", c => c.Int());
            CreateIndex("dbo.Movies", "StudioId");
            AddForeignKey("dbo.Movies", "StudioId", "dbo.Studios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "StudioId", "dbo.Studios");
            DropIndex("dbo.Movies", new[] { "StudioId" });
            AlterColumn("dbo.Movies", "StudioId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "StudioId");
            AddForeignKey("dbo.Movies", "StudioId", "dbo.Studios", "Id", cascadeDelete: true);
        }
    }
}
