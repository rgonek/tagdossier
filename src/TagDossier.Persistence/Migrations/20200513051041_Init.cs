using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TagDossier.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connector",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_On = table.Column<DateTime>(nullable: true),
                    Created_ById = table.Column<Guid>(nullable: true),
                    LastModified_On = table.Column<DateTime>(nullable: true),
                    LastModified_ById = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: false),
                    TextColor = table.Column<string>(type: "varchar(6)", nullable: true),
                    BackgroundColor = table.Column<string>(type: "varchar(6)", nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Tag_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_AspNetUsers_Created_ById",
                        column: x => x.Created_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_AspNetUsers_LastModified_ById",
                        column: x => x.LastModified_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_On = table.Column<DateTime>(nullable: true),
                    Created_ById = table.Column<Guid>(nullable: true),
                    LastModified_On = table.Column<DateTime>(nullable: true),
                    LastModified_ById = table.Column<Guid>(nullable: true),
                    ConnectorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Source_Connector_ConnectorId",
                        column: x => x.ConnectorId,
                        principalTable: "Connector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Source_AspNetUsers_Created_ById",
                        column: x => x.Created_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Source_AspNetUsers_LastModified_ById",
                        column: x => x.LastModified_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_On = table.Column<DateTime>(nullable: true),
                    Created_ById = table.Column<Guid>(nullable: true),
                    LastModified_On = table.Column<DateTime>(nullable: true),
                    LastModified_ById = table.Column<Guid>(nullable: true),
                    SourceId = table.Column<int>(nullable: false),
                    ExternalId = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resource_AspNetUsers_Created_ById",
                        column: x => x.Created_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resource_AspNetUsers_LastModified_ById",
                        column: x => x.LastModified_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dossier",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_On = table.Column<DateTime>(nullable: true),
                    Created_ById = table.Column<Guid>(nullable: true),
                    LastModified_On = table.Column<DateTime>(nullable: true),
                    LastModified_ById = table.Column<Guid>(nullable: true),
                    ResourceId = table.Column<long>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dossier_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dossier_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dossier_AspNetUsers_Created_ById",
                        column: x => x.Created_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dossier_AspNetUsers_LastModified_ById",
                        column: x => x.LastModified_ById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dossier_ResourceId",
                table: "Dossier",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossier_TagId",
                table: "Dossier",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossier_Created_ById",
                table: "Dossier",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Dossier_LastModified_ById",
                table: "Dossier",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_SourceId",
                table: "Resource",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_Created_ById",
                table: "Resource",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_LastModified_ById",
                table: "Resource",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Source_ConnectorId",
                table: "Source",
                column: "ConnectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Source_Created_ById",
                table: "Source",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Source_LastModified_ById",
                table: "Source",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ParentId",
                table: "Tag",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Created_ById",
                table: "Tag",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LastModified_ById",
                table: "Tag",
                column: "LastModified_ById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dossier");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "Connector");
        }
    }
}
