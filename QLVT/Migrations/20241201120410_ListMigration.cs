using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLVT.Migrations
{
    /// <inheritdoc />
    public partial class ListMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListExport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doc_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    export_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    export_price = table.Column<long>(type: "bigint", nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false),
                    promoter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    receiver = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListExport", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ListImport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doc_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    import_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    import_price = table.Column<long>(type: "bigint", nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false),
                    deliverer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    receiver = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListImport", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ListProduct",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    import_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    import_price = table.Column<long>(type: "bigint", nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListProduct", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListExport");

            migrationBuilder.DropTable(
                name: "ListImport");

            migrationBuilder.DropTable(
                name: "ListProduct");
        }
    }
}
