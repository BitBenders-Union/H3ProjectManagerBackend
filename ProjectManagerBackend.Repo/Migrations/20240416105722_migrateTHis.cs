using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagerBackend.Repo.Migrations
{
    /// <inheritdoc />
    public partial class migrateTHis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "UserDetails",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "UserDetails",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserDetails");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserDetailId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_UserDetails_UserDetailId",
                        column: x => x.UserDetailId,
                        principalTable: "UserDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserDetailId",
                table: "UserLogins",
                column: "UserDetailId");
        }
    }
}
