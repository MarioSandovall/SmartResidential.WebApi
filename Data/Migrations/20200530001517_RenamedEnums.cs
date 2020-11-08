using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenamedEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Document");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Image");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Paid out");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Cancelled");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cash");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Credit Card");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Payment transfer");

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Opened");

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cancelled");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Approved");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Not Approved");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Resident", "Resident" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Vigilant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Documento");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Imagen");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pendiente");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Pagado");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Cancelado");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Efectivo");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Tarjeta de crédito");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Transferencia");

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Abierto");

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cancelado");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pendiente");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Aprobado");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "No Aprobado");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Administrador");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Inquilino (ChangeThis)", "Tenant" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Vigilante");
        }
    }
}
