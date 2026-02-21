using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanctionsAndDisbursementService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disbursements",
                columns: table => new
                {
                    disbursementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dealId = table.Column<int>(type: "int", nullable: false),
                    branchId = table.Column<int>(type: "int", nullable: false),
                    disburseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    disburseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transactionReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disbursements", x => x.disbursementId);
                });

            migrationBuilder.CreateTable(
                name: "Sanctions",
                columns: table => new
                {
                    sanctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dealId = table.Column<int>(type: "int", nullable: false),
                    sanctionNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    loanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    interestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tenureMonths = table.Column<int>(type: "int", nullable: false),
                    emiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    repaymentSchedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerAccepted = table.Column<bool>(type: "bit", nullable: false),
                    acceptedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    pdfPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanctions", x => x.sanctionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disbursements");

            migrationBuilder.DropTable(
                name: "Sanctions");
        }
    }
}
