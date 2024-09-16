namespace PetSit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerIdChanged : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pets", new[] { "Owner_Id" });
            DropColumn("dbo.Pets", "OwnerID");
            RenameColumn(table: "dbo.Pets", name: "Owner_Id", newName: "OwnerID");
            AlterColumn("dbo.Pets", "OwnerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pets", "OwnerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pets", new[] { "OwnerID" });
            AlterColumn("dbo.Pets", "OwnerID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Pets", name: "OwnerID", newName: "Owner_Id");
            AddColumn("dbo.Pets", "OwnerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pets", "Owner_Id");
        }
    }
}
