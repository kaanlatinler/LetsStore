using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetsStore.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    StorageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true, defaultValueSql: "(getdate())"),
                    StoredFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadedDevice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Storage__8A247E37C4215146", x => x.StorageID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACE40DE002", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "StorageMap",
                columns: table => new
                {
                    StorageMapID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StorageM__02975C2C7470A5D4", x => x.StorageMapID);
                    table.ForeignKey(
                        name: "FK_StorageMap_Storage",
                        column: x => x.StorageID,
                        principalTable: "Storage",
                        principalColumn: "StorageID");
                    table.ForeignKey(
                        name: "FK_StorageMap_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageMap_StorageID",
                table: "StorageMap",
                column: "StorageID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageMap_UserID",
                table: "StorageMap",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__08638DF8072995FA",
                table: "Users",
                column: "UserEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageMap");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
