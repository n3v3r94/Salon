using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Salon.Data.Migrations
{
    public partial class RelationUserToSalon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Salons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salons_UserId",
                table: "Salons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salons_AspNetUsers_UserId",
                table: "Salons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salons_AspNetUsers_UserId",
                table: "Salons");

            migrationBuilder.DropIndex(
                name: "IX_Salons_UserId",
                table: "Salons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Salons");
        }
    }
}
