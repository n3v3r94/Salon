using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Salon.Data.Migrations
{
    public partial class DeleteWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWorker_Worker_WorkerId",
                table: "ProductWorker");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWorker",
                table: "ProductWorker");

            migrationBuilder.DropIndex(
                name: "IX_ProductWorker_WorkerId",
                table: "ProductWorker");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "ProductWorker");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductWorker",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWorker",
                table: "ProductWorker",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWorker_UserId",
                table: "ProductWorker",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWorker_AspNetUsers_UserId",
                table: "ProductWorker",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWorker_AspNetUsers_UserId",
                table: "ProductWorker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWorker",
                table: "ProductWorker");

            migrationBuilder.DropIndex(
                name: "IX_ProductWorker_UserId",
                table: "ProductWorker");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductWorker");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "ProductWorker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWorker",
                table: "ProductWorker",
                columns: new[] { "ProductId", "WorkerId" });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWorker_WorkerId",
                table: "ProductWorker",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWorker_Worker_WorkerId",
                table: "ProductWorker",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
