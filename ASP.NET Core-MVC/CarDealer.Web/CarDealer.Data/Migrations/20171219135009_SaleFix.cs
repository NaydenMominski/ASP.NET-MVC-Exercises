using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarDealer.Data.Migrations
{
    public partial class SaleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CutomerId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "CutomerId",
                table: "Sales",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CutomerId",
                table: "Sales",
                newName: "IX_Sales_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Sales",
                newName: "CutomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                newName: "IX_Sales_CutomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CutomerId",
                table: "Sales",
                column: "CutomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
