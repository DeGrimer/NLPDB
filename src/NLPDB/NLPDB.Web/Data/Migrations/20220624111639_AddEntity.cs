using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLPDB.Web.Data.Migrations
{
    public partial class AddEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskAlg",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAlg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Algorithm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Algorithm_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTaskAlg",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TasksAlgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTaskAlg", x => new { x.CategoriesId, x.TasksAlgId });
                    table.ForeignKey(
                        name: "FK_CategoryTaskAlg_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTaskAlg_TaskAlg_TasksAlgId",
                        column: x => x.TasksAlgId,
                        principalTable: "TaskAlg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlgorithmTaskAlg",
                columns: table => new
                {
                    AlgorithmsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TasksAlgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlgorithmTaskAlg", x => new { x.AlgorithmsId, x.TasksAlgId });
                    table.ForeignKey(
                        name: "FK_AlgorithmTaskAlg_Algorithm_AlgorithmsId",
                        column: x => x.AlgorithmsId,
                        principalTable: "Algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlgorithmTaskAlg_TaskAlg_TasksAlgId",
                        column: x => x.TasksAlgId,
                        principalTable: "TaskAlg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Algorithm_CategoryId",
                table: "Algorithm",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AlgorithmTaskAlg_TasksAlgId",
                table: "AlgorithmTaskAlg",
                column: "TasksAlgId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTaskAlg_TasksAlgId",
                table: "CategoryTaskAlg",
                column: "TasksAlgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlgorithmTaskAlg");

            migrationBuilder.DropTable(
                name: "CategoryTaskAlg");

            migrationBuilder.DropTable(
                name: "Algorithm");

            migrationBuilder.DropTable(
                name: "TaskAlg");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
