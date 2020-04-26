using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DistributedExceptionHandling.Worker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributedException",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    StatusCode = table.Column<string>(nullable: false),
                    RequestedUri = table.Column<string>(nullable: false),
                    Origin = table.Column<string>(nullable: false),
                    OcurredExceptionDate = table.Column<DateTime>(nullable: false),
                    QueuePulledExceptionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributedException", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributedException");
        }
    }
}
