using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfferApplication.API.Migrations
{
    /// <inheritdoc />
    public partial class migrationGod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Providers_ProviderId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Providers_ProviderId",
                table: "Offers",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Providers_ProviderId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Providers_ProviderId",
                table: "Offers",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
