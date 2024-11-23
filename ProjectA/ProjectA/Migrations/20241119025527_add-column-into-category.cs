using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectA.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnintocategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Categories");
        }
    }
}
