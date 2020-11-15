using Microsoft.EntityFrameworkCore.Migrations;

namespace angular.Web.Migrations
{
    public partial class defaultcreditcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultCreditCardId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultCreditCardId",
                table: "AspNetUsers",
                column: "DefaultCreditCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CreditCard_DefaultCreditCardId",
                table: "AspNetUsers",
                column: "DefaultCreditCardId",
                principalTable: "CreditCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CreditCard_DefaultCreditCardId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DefaultCreditCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DefaultCreditCardId",
                table: "AspNetUsers");
        }
    }
}
