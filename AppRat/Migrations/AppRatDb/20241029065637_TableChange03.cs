using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppRat.Migrations.AppRatDb
{
    /// <inheritdoc />
    public partial class TableChange03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AR_Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Franchise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    SalesPeople = table.Column<int>(type: "int", nullable: false),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Results_Id = table.Column<int>(type: "int", nullable: false),
                    Condition_Id = table.Column<int>(type: "int", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    Invoiced = table.Column<bool>(type: "bit", nullable: false),
                    Signed = table.Column<bool>(type: "bit", nullable: false),
                    Insurance_Id = table.Column<int>(type: "int", nullable: false),
                    TradeIn = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Spotter = table.Column<bool>(type: "bit", nullable: false),
                    ClientOutOfTown = table.Column<bool>(type: "bit", nullable: false),
                    Remarks_Id = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AR_Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_Feedback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AR_Target",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    New = table.Column<int>(type: "int", nullable: false),
                    Used = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_Target", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_Auths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_Auths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_Dealerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_Dealerships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_Insurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_Insurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_Remarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_Remarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARL_SalesPeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARL_SalesPeople", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARR_Auths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARR_Auths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ARR_DealerLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARR_DealerLink", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AR_Applications");

            migrationBuilder.DropTable(
                name: "AR_Feedback");

            migrationBuilder.DropTable(
                name: "AR_Target");

            migrationBuilder.DropTable(
                name: "ARL_Auths");

            migrationBuilder.DropTable(
                name: "ARL_Conditions");

            migrationBuilder.DropTable(
                name: "ARL_Dealerships");

            migrationBuilder.DropTable(
                name: "ARL_Insurances");

            migrationBuilder.DropTable(
                name: "ARL_Remarks");

            migrationBuilder.DropTable(
                name: "ARL_Results");

            migrationBuilder.DropTable(
                name: "ARL_SalesPeople");

            migrationBuilder.DropTable(
                name: "ARR_Auths");

            migrationBuilder.DropTable(
                name: "ARR_DealerLink");
        }
    }
}
