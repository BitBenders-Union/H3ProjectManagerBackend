using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagerBackend.Repo.Migrations
{
    /// <inheritdoc />
    public partial class noncascadingDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_Departments_DepartmentId",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_Locations_LocationId",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDepartments_Departments_DepartmentId",
                table: "ProjectDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDepartments_Projects_ProjectId",
                table: "ProjectDepartments");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_Departments_DepartmentId",
                table: "DepartmentLocations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_Locations_LocationId",
                table: "DepartmentLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDepartments_Departments_DepartmentId",
                table: "ProjectDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDepartments_Projects_ProjectId",
                table: "ProjectDepartments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_Departments_DepartmentId",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_Locations_LocationId",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDepartments_Departments_DepartmentId",
                table: "ProjectDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDepartments_Projects_ProjectId",
                table: "ProjectDepartments");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_Departments_DepartmentId",
                table: "DepartmentLocations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_Locations_LocationId",
                table: "DepartmentLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDepartments_Departments_DepartmentId",
                table: "ProjectDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDepartments_Projects_ProjectId",
                table: "ProjectDepartments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
