using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace cs_app.Migrations
{
    public partial class MyMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    sec_name = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    doc = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hotel_name = table.Column<string>(type: "text", nullable: false),
                    room = table.Column<int>(type: "integer", nullable: false),
                    car_name = table.Column<string>(type: "text", nullable: false),
                    guide_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hotels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    dest = table.Column<string>(type: "text", nullable: false),
                    place = table.Column<string>(type: "text", nullable: false),
                    num_pass = table.Column<int>(type: "integer", nullable: false),
                    flight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tickets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_hotel",
                columns: table => new
                {
                    customers_id = table.Column<long>(type: "bigint", nullable: false),
                    hotels_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_hotel", x => new { x.customers_id, x.hotels_id });
                    table.ForeignKey(
                        name: "fk_customer_hotel_customers_customers_id",
                        column: x => x.customers_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customer_hotel_hotels_hotels_id",
                        column: x => x.hotels_id,
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_ticket",
                columns: table => new
                {
                    customers_id = table.Column<long>(type: "bigint", nullable: false),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_ticket", x => new { x.customers_id, x.ticket_id });
                    table.ForeignKey(
                        name: "fk_customer_ticket_customers_customers_id",
                        column: x => x.customers_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customer_ticket_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotel_ticket",
                columns: table => new
                {
                    hotels_id = table.Column<long>(type: "bigint", nullable: false),
                    tickets_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hotel_ticket", x => new { x.hotels_id, x.tickets_id });
                    table.ForeignKey(
                        name: "fk_hotel_ticket_hotels_hotels_id",
                        column: x => x.hotels_id,
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_hotel_ticket_tickets_tickets_id",
                        column: x => x.tickets_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_hotel_hotels_id",
                table: "customer_hotel",
                column: "hotels_id");

            migrationBuilder.CreateIndex(
                name: "ix_customer_ticket_ticket_id",
                table: "customer_ticket",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_hotel_ticket_tickets_id",
                table: "hotel_ticket",
                column: "tickets_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_hotel");

            migrationBuilder.DropTable(
                name: "customer_ticket");

            migrationBuilder.DropTable(
                name: "hotel_ticket");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "tickets");
        }
    }
}
