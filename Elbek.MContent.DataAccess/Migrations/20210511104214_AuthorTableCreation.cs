using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elbek.MContent.DataAccess.Migrations
{
    ///TODO 4 
    /// миграции тут нам не нужны, с базой работает проект Database, он должен создавать базу и заполнять ее данными

    public partial class AuthorTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
