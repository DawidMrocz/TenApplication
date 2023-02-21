using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TenApplication.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CCtr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActTyp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TennecoStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Software = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engineer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ecm = table.Column<int>(type: "int", nullable: false),
                    Gpdm = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<int>(type: "int", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Received = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finished = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Raports",
                columns: table => new
                {
                    RaportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaportDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raports", x => x.RaportId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    CatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.CatId);
                    table.ForeignKey(
                        name: "FK_Cats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inboxs",
                columns: table => new
                {
                    InboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inboxs", x => x.InboxId);
                    table.ForeignKey(
                        name: "FK_Inboxs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InboxItems",
                columns: table => new
                {
                    InboxItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hours = table.Column<double>(type: "float(2)", precision: 2, nullable: false),
                    Components = table.Column<int>(type: "int", nullable: false),
                    DrawingsComponents = table.Column<int>(type: "int", nullable: false),
                    DrawingsAssembly = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    InboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxItems", x => x.InboxItemId);
                    table.ForeignKey(
                        name: "FK_InboxItems_Inboxs_InboxId",
                        column: x => x.InboxId,
                        principalTable: "Inboxs",
                        principalColumn: "InboxId");
                    table.ForeignKey(
                        name: "FK_InboxItems_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatRecords",
                columns: table => new
                {
                    CatRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InboxItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatRecords", x => x.CatRecordId);
                    table.ForeignKey(
                        name: "FK_CatRecords_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "CatId");
                    table.ForeignKey(
                        name: "FK_CatRecords_InboxItems_InboxItemId",
                        column: x => x.InboxItemId,
                        principalTable: "InboxItems",
                        principalColumn: "InboxItemId");
                });

            migrationBuilder.CreateTable(
                name: "RaportRecords",
                columns: table => new
                {
                    RaportRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaportRecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RaportRecordHours = table.Column<double>(type: "float(2)", precision: 2, nullable: false),
                    InboxItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaportRecords", x => x.RaportRecordId);
                    table.ForeignKey(
                        name: "FK_RaportRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RaportRecords_InboxItems_InboxItemId",
                        column: x => x.InboxItemId,
                        principalTable: "InboxItems",
                        principalColumn: "InboxItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaportRecords_Raports_RaportId",
                        column: x => x.RaportId,
                        principalTable: "Raports",
                        principalColumn: "RaportId");
                });

            migrationBuilder.CreateTable(
                name: "CatRecordCells",
                columns: table => new
                {
                    CatRecordCellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellHours = table.Column<double>(type: "float(2)", precision: 2, nullable: false),
                    CatRecordCellDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CatRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatRecordCells", x => x.CatRecordCellId);
                    table.ForeignKey(
                        name: "FK_CatRecordCells_CatRecords_CatRecordId",
                        column: x => x.CatRecordId,
                        principalTable: "CatRecords",
                        principalColumn: "CatRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "Client", "DueDate", "Ecm", "Engineer", "Finished", "Gpdm", "JobDescription", "Link", "ProjectName", "ProjectNumber", "Region", "Software", "Started", "Status", "TaskType" },
                values: new object[,]
                {
                    { 1, null, null, 32016495, null, null, 0, "Create muffler", null, null, null, "NA", "Catia", null, 0, "Models" },
                    { 2, null, null, 32016408, null, null, 0, "Create pipe", null, null, null, "NA", "NX", null, 10, "Models" },
                    { 3, null, null, 32016497, null, null, 0, "Create drawing", null, null, null, "NA", "Catia", null, 20, "Drawings" },
                    { 4, null, null, 32016485, null, null, 0, "Update drawing", null, null, null, "NA", "Catia", null, 0, "Drawings" },
                    { 5, null, null, 32016464, null, null, 0, "Partition holes", null, null, null, "CN", "Catia", null, 100, "Models" },
                    { 6, null, null, 32016435, null, null, 0, "Hot end proposal", null, null, null, "NA", "Catia", null, 45, "Both" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CatRecordCells_CatRecordId",
                table: "CatRecordCells",
                column: "CatRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CatRecords_CatId",
                table: "CatRecords",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_CatRecords_InboxItemId",
                table: "CatRecords",
                column: "InboxItemId",
                unique: true,
                filter: "[InboxItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_UserId",
                table: "Cats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_InboxId",
                table: "InboxItems",
                column: "InboxId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_JobId",
                table: "InboxItems",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Inboxs_UserId",
                table: "Inboxs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaportRecords_InboxItemId",
                table: "RaportRecords",
                column: "InboxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RaportRecords_RaportId",
                table: "RaportRecords",
                column: "RaportId");

            migrationBuilder.CreateIndex(
                name: "IX_RaportRecords_UserId",
                table: "RaportRecords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CatRecordCells");

            migrationBuilder.DropTable(
                name: "RaportRecords");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CatRecords");

            migrationBuilder.DropTable(
                name: "Raports");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "InboxItems");

            migrationBuilder.DropTable(
                name: "Inboxs");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
