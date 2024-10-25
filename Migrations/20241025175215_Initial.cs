using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_key",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    token = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_key", x => x.Uuid);
                });

            migrationBuilder.InsertData(
                table: "api_key",
                columns: new[] { "Uuid", "created_at", "deleted_at", "description", "name", "token", "updated_at" },
                values: new object[] { new Guid("5b2a367c-55c1-4b65-b1c8-e9f54c3b57d1"), new DateTime(2024, 10, 25, 17, 52, 15, 358, DateTimeKind.Utc).AddTicks(1695), null, "first apikey", "master", "W8aKo6fhzZoWBECW86Gt7osp5i2UAp5Rs3JAbpFMRLSaQPgu9Hc4hHVpEepkm5MW", new DateTime(2024, 10, 25, 17, 52, 15, 358, DateTimeKind.Utc).AddTicks(1695) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_key");
        }
    }
}
