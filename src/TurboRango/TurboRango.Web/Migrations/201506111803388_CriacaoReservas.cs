namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoReservas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        QtdePessoas = c.Int(nullable: false),
                        Nome = c.String(),
                        Telefone = c.String(),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Restarurante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurantes", t => t.Restarurante_Id)
                .Index(t => t.Restarurante_Id);
            
            AddColumn("dbo.Restaurantes", "ValorPessoa", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Restarurante_Id", "dbo.Restaurantes");
            DropIndex("dbo.Reservas", new[] { "Restarurante_Id" });
            DropColumn("dbo.Restaurantes", "ValorPessoa");
            DropTable("dbo.Reservas");
        }
    }
}
