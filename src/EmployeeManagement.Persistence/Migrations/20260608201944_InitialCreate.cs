using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CurrentPositionId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_CurrentPositionId",
                        column: x => x.CurrentPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnassignedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionHistories_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Software development and infrastructure", "Engineering" },
                    { 2, "Commercial and business development", "Sales" },
                    { 3, "People management and recruitment", "Human Resources" },
                    { 4, "Financial management and accounting", "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Employee" },
                    { 2, "Manager" },
                    { 3, "Senior Manager" },
                    { 4, "Director" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "EmployeeId", "IsActive", "LastLoginAt", "PasswordHash", "Role" },
                values: new object[] { 1, new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), "admin@employeemanagement.local", null, true, null, "$2a$11$B8rPrvToa6xUe8HG1h8EPeM/OVCoJyobrHvbxDCix6jpE3AAF8C8i", "Admin" });

            migrationBuilder.Sql("""
                DECLARE @MigrationDate datetime2 = SYSUTCDATETIME();
                DECLARE @ProjectStartDate datetime2 = DATEADD(day, -15, @MigrationDate);
                DECLARE @ProjectEndDate datetime2 = DATEADD(day, 15, @MigrationDate);

                SET IDENTITY_INSERT [Employees] ON;

                INSERT INTO [Employees] ([Id], [IdentificationNumber], [Name], [Salary], [CurrentPositionId], [DepartmentId], [HireDate], [IsActive])
                VALUES
                    (1, N'100000001', N'Martin Sales Manager', 9500000.00, 2, 2, DATEADD(year, -5, @MigrationDate), CAST(1 AS bit)),
                    (2, N'100000002', N'Laura Engineering Manager', 9800000.00, 2, 1, DATEADD(year, -5, @MigrationDate), CAST(1 AS bit)),
                    (3, N'100000003', N'Andres Regular Engineer', 5200000.00, 1, 1, DATEADD(year, -2, @MigrationDate), CAST(1 AS bit)),
                    (4, N'100000004', N'Sofia Junior Engineer', 4200000.00, 1, 1, DATEADD(month, -2, @MigrationDate), CAST(1 AS bit));

                SET IDENTITY_INSERT [Employees] OFF;

                SET IDENTITY_INSERT [Projects] ON;

                INSERT INTO [Projects] ([Id], [Name], [Description], [StartDate], [EndDate])
                VALUES
                    (1, N'Clinical Scheduling Platform', N'Patient scheduling and operations workflow.', @ProjectStartDate, @ProjectEndDate),
                    (2, N'Mobile Care Portal', N'Mobile portal for care coordination.', @ProjectStartDate, @ProjectEndDate),
                    (3, N'Billing Automation', N'Automation for billing and reporting tasks.', @ProjectStartDate, @ProjectEndDate);

                SET IDENTITY_INSERT [Projects] OFF;

                SET IDENTITY_INSERT [PositionHistories] ON;

                INSERT INTO [PositionHistories] ([Id], [EmployeeId], [PositionId], [StartDate], [EndDate])
                VALUES
                    (1, 2, 1, DATEADD(year, -5, @MigrationDate), DATEADD(year, -2, @MigrationDate)),
                    (2, 2, 2, DATEADD(year, -2, @MigrationDate), NULL);

                SET IDENTITY_INSERT [PositionHistories] OFF;

                SET IDENTITY_INSERT [EmployeeProjects] ON;

                INSERT INTO [EmployeeProjects] ([Id], [EmployeeId], [ProjectId], [AssignedDate], [UnassignedDate])
                VALUES
                    (1, 3, 1, @ProjectStartDate, NULL),
                    (2, 3, 2, @ProjectStartDate, NULL),
                    (3, 4, 3, @ProjectStartDate, NULL);

                SET IDENTITY_INSERT [EmployeeProjects] OFF;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_AssignedDate",
                table: "EmployeeProjects",
                column: "AssignedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                table: "EmployeeProjects",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_EmployeeId_ProjectId",
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CurrentPositionId",
                table: "Employees",
                column: "CurrentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdentificationNumber",
                table: "Employees",
                column: "IdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsActive",
                table: "Employees",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Name",
                table: "Employees",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PositionHistories_EmployeeId",
                table: "PositionHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionHistories_PositionId",
                table: "PositionHistories",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionHistories_StartDate",
                table: "PositionHistories",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name",
                table: "Positions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EndDate",
                table: "Projects",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StartDate",
                table: "Projects",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "PositionHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
