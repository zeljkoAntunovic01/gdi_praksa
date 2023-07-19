using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class projectiontype3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projections_ProjectionType_ProjectionTypeId",
                table: "Projections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectionType",
                table: "ProjectionType");

            migrationBuilder.RenameTable(
                name: "ProjectionType",
                newName: "ProjectionTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectionTypes",
                table: "ProjectionTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_ProjectionTypes_ProjectionTypeId",
                table: "Projections",
                column: "ProjectionTypeId",
                principalTable: "ProjectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projections_ProjectionTypes_ProjectionTypeId",
                table: "Projections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectionTypes",
                table: "ProjectionTypes");

            migrationBuilder.RenameTable(
                name: "ProjectionTypes",
                newName: "ProjectionType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectionType",
                table: "ProjectionType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_ProjectionType_ProjectionTypeId",
                table: "Projections",
                column: "ProjectionTypeId",
                principalTable: "ProjectionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
