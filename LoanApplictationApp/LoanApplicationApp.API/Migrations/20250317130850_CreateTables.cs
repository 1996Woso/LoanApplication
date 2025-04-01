using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApplicationApp.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanProcessors",
                columns: table => new
                {
                    LoanProcessorNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProcessors", x => x.LoanProcessorNo);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanProcessorNo = table.Column<long>(type: "bigint", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthlySalary = table.Column<double>(type: "float", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    CreditScore = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationNo);
                    table.ForeignKey(
                        name: "FK_Applications_LoanProcessors_LoanProcessorNo",
                        column: x => x.LoanProcessorNo,
                        principalTable: "LoanProcessors",
                        principalColumn: "LoanProcessorNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_LoanProcessorNo",
                table: "Applications",
                column: "LoanProcessorNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "LoanProcessors");
        }
    }
}
