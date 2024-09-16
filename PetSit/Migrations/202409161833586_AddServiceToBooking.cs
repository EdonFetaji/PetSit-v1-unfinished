namespace PetSit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceToBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "SelectedService_name", c => c.String());
            AddColumn("dbo.Bookings", "SelectedService_description", c => c.String());
            AddColumn("dbo.Bookings", "SelectedService_price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "SelectedService_price");
            DropColumn("dbo.Bookings", "SelectedService_description");
            DropColumn("dbo.Bookings", "SelectedService_name");
        }
    }
}
