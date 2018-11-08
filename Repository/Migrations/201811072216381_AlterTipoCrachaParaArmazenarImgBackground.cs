namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTipoCrachaParaArmazenarImgBackground : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCrachas", "ImgFundoCracha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoCrachas", "ImgFundoCracha");
        }
    }
}
