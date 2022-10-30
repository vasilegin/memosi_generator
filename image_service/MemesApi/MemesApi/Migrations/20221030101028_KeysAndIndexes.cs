using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemesApi.Migrations
{
    public partial class KeysAndIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Files_MetaId",
                table: "Files",
                column: "MetaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_ClientId",
                table: "Estimates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_FileId",
                table: "Estimates",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_Files_FileId",
                table: "Estimates",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Metas_MetaId",
                table: "Files",
                column: "MetaId",
                principalTable: "Metas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_Files_FileId",
                table: "Estimates");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Metas_MetaId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_MetaId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_ClientId",
                table: "Estimates");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_FileId",
                table: "Estimates");
        }
    }
}
