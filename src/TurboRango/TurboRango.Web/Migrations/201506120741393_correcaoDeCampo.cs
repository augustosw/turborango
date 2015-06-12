namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcaoDeCampo : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservas", name: "Restarurante_Id", newName: "Restaurante_Id");
            RenameIndex(table: "dbo.Reservas", name: "IX_Restarurante_Id", newName: "IX_Restaurante_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reservas", name: "IX_Restaurante_Id", newName: "IX_Restarurante_Id");
            RenameColumn(table: "dbo.Reservas", name: "Restaurante_Id", newName: "Restarurante_Id");
        }
    }
}
