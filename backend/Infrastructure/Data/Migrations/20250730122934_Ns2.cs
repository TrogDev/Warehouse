using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomingItems_Resources_ResourceId",
                table: "IncomingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingItems_Units_UnitId",
                table: "IncomingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingItems_Resources_ResourceId",
                table: "OutgoingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingItems_Units_UnitId",
                table: "OutgoingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Outgoings_Clients_ClientId",
                table: "Outgoings");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingItems_Resources_ResourceId",
                table: "IncomingItems",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingItems_Units_UnitId",
                table: "IncomingItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingItems_Resources_ResourceId",
                table: "OutgoingItems",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingItems_Units_UnitId",
                table: "OutgoingItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Outgoings_Clients_ClientId",
                table: "Outgoings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomingItems_Resources_ResourceId",
                table: "IncomingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingItems_Units_UnitId",
                table: "IncomingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingItems_Resources_ResourceId",
                table: "OutgoingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingItems_Units_UnitId",
                table: "OutgoingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Outgoings_Clients_ClientId",
                table: "Outgoings");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingItems_Resources_ResourceId",
                table: "IncomingItems",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingItems_Units_UnitId",
                table: "IncomingItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingItems_Resources_ResourceId",
                table: "OutgoingItems",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingItems_Units_UnitId",
                table: "OutgoingItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Outgoings_Clients_ClientId",
                table: "Outgoings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
