using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemesApi.Migrations
{
    public partial class AddMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Format = table.Column<String>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalEstimates = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.Id);
                }
            );
            
            migrationBuilder.AlterColumn<string>(
                table: "Estimates",
                name: "ClientId",
                type: "TEXT",
                nullable: false,
                oldNullable: true
            );
            migrationBuilder.AddColumn<int>(
                table: "Files",
                name: "MetaId",
                type: "INTEGER",
                nullable: true
            );
            migrationBuilder.AddForeignKey(
                table: "Files",
                column: "MetaId",
                name: "FK_Metas_Files_MetaId",
                principalTable: "Metas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );
            migrationBuilder.AddForeignKey(
                table: "Estimates",
                column: "FileId",
                name: "FK_Files_Estimates_FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Metas_Files_MetaId", "Files");
            migrationBuilder.DropForeignKey("FK_Files_Estimates_FileId", "Estimates");
            migrationBuilder.DropColumn("MetaId", "Files");
            migrationBuilder.AlterColumn<string>(
                table: "Estimates",
                name: "ClientId",
                type: "TEXT",
                nullable: true,
                oldNullable: false
            );
            migrationBuilder.DropTable(name: "Metas");
        }
    }
}
