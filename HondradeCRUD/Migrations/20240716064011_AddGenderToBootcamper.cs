using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HondradeCRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToBootcamper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Bootcampers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Bootcampers");
        }
    }
}
