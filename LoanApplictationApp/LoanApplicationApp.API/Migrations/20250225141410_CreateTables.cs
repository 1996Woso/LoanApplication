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
                name: "LoanProcessors",
                columns: table => new
                {
                    LoanProcessorNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    LoanProcessorNo = table.Column<long>(type: "bigint", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthlySalary = table.Column<double>(type: "float", nullable: false),
                    CreditScore = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCopyFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankStatementFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaySlipFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "LoanProcessors");
        }
    }
}
