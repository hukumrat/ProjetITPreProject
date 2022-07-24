using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class identity6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contents",
                table: "Comments",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Contents",
                table: "Actions",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Comments",
                newName: "Contents");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Actions",
                newName: "Contents");
        }
    }
}
