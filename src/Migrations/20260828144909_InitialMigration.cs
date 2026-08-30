using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnquiryRouting.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EnquiryRoutingApi");

            migrationBuilder.CreateTable(
                name: "Agents",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MaxCapacity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPriority = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentLanguages",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentLanguages", x => new { x.AgentId, x.LanguageCode });
                    table.ForeignKey(
                        name: "FK_AgentLanguages_Agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enquiries",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateTimeCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateTimeClosed = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ClosedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enquiries_Agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AgentSkills",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentSkills", x => new { x.AgentId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_AgentSkills_Agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EnquiryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SenderType = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    DateTimeCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Enquiries_EnquiryId",
                        column: x => x.EnquiryId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnquirySkills",
                schema: "EnquiryRoutingApi",
                columns: table => new
                {
                    EnquiryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquirySkills", x => new { x.EnquiryId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_EnquirySkills_Enquiries_EnquiryId",
                        column: x => x.EnquiryId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnquirySkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "EnquiryRoutingApi",
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentLanguages_LanguageCode_AgentId",
                schema: "EnquiryRoutingApi",
                table: "AgentLanguages",
                columns: new[] { "LanguageCode", "AgentId" });

            migrationBuilder.CreateIndex(
                name: "IX_AgentSkills_SkillId_AgentId",
                schema: "EnquiryRoutingApi",
                table: "AgentSkills",
                columns: new[] { "SkillId", "AgentId" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_EnquiryId_DateTimeCreated",
                schema: "EnquiryRoutingApi",
                table: "ChatMessages",
                columns: new[] { "EnquiryId", "DateTimeCreated" });

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_AgentId",
                schema: "EnquiryRoutingApi",
                table: "Enquiries",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnquirySkills_SkillId_EnquiryId",
                schema: "EnquiryRoutingApi",
                table: "EnquirySkills",
                columns: new[] { "SkillId", "EnquiryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                schema: "EnquiryRoutingApi",
                table: "Skills",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentLanguages",
                schema: "EnquiryRoutingApi");

            migrationBuilder.DropTable(
                name: "AgentSkills",
                schema: "EnquiryRoutingApi");

            migrationBuilder.DropTable(
                name: "ChatMessages",
                schema: "EnquiryRoutingApi");

            migrationBuilder.DropTable(
                name: "EnquirySkills",
                schema: "EnquiryRoutingApi");

            migrationBuilder.DropTable(
                name: "Enquiries",
                schema: "EnquiryRoutingApi");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "EnquiryRoutingApi");

            migrationBuilder.DropTable(
                name: "Agents",
                schema: "EnquiryRoutingApi");
        }
    }
}
