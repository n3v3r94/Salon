using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Salon.Data.Migrations
{
    public partial class EditManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonsId",
                table: "ProductWorker",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductWorker_SalonsId",
                table: "ProductWorker",
                column: "SalonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWorker_Salons_SalonsId",
                table: "ProductWorker",
                column: "SalonsId",
                principalTable: "Salons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWorker_Salons_SalonsId",
                table: "ProductWorker");

            migrationBuilder.DropIndex(
                name: "IX_ProductWorker_SalonsId",
                table: "ProductWorker");

            migrationBuilder.DropColumn(
                name: "SalonsId",
                table: "ProductWorker");
        }
    }
}
