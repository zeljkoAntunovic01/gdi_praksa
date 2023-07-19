using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class projectiontype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectionTypeId",
                table: "Projections",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ProjectionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectionType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projections_ProjectionTypeId",
                table: "Projections",
                column: "ProjectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_ProjectionType_ProjectionTypeId",
                table: "Projections",
                column: "ProjectionTypeId",
                principalTable: "ProjectionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projections_ProjectionType_ProjectionTypeId",
                table: "Projections");

            migrationBuilder.DropTable(
                name: "ProjectionType");

            migrationBuilder.DropIndex(
                name: "IX_Projections_ProjectionTypeId",
                table: "Projections");

            migrationBuilder.DropColumn(
                name: "ProjectionTypeId",
                table: "Projections");
        }
    }
}
