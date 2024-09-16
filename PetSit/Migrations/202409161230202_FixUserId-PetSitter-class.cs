namespace PetSit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserIdPetSitterclass : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PetSitters", new[] { "User_Id" });
            DropColumn("dbo.PetSitters", "UserID");
            RenameColumn(table: "dbo.PetSitters", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.PetSitters", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.PetSitters", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PetSitters", new[] { "UserID" });
            AlterColumn("dbo.PetSitters", "UserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PetSitters", name: "UserID", newName: "User_Id");
            AddColumn("dbo.PetSitters", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.PetSitters", "User_Id");
        }
    }
}
