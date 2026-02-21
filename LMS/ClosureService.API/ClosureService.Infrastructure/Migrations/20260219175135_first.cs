using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClosureService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foreclosures",
                columns: table => new
                {
                    ForeclosureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForeclosureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrincipalOutstanding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestOutstanding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PenaltyOutstanding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForeclosureCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RebateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForeclosureType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemainingTenure = table.Column<int>(type: "int", nullable: false),
                    SavingsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CertificateGenerated = table.Column<bool>(type: "bit", nullable: false),
                    CertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foreclosures", x => x.ForeclosureId);
                });

            migrationBuilder.CreateTable(
                name: "LoanClosures",
                columns: table => new
                {
                    ClosureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    ClosureType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosureReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalMaturityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualMaturityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalDisbursed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrincipalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalInterestPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPenaltyPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutstandingAtClosure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosureCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RebateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WriteOffAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SettlementAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NocIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NocIssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NocNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NocFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosureCertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosureApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosureStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanClosures", x => x.ClosureId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foreclosures");

            migrationBuilder.DropTable(
                name: "LoanClosures");
        }
    }
}
