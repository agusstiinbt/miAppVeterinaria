using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miAppVeterinaria.Migrations
{
    public partial class _1erMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consultorios",
                columns: table => new
                {
                    IdConsultorio = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consultorios", x => x.IdConsultorio);
                });

            migrationBuilder.CreateTable(
                name: "diasLaboralesVet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Lunes = table.Column<bool>(type: "bit", nullable: false),
                    Martes = table.Column<bool>(type: "bit", nullable: false),
                    Miercoles = table.Column<bool>(type: "bit", nullable: false),
                    Jueves = table.Column<bool>(type: "bit", nullable: false),
                    Viernes = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaslaborales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "secretariasVeterinarios",
                columns: table => new
                {
                    IdSecretaria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consultorio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secretarias", x => x.IdSecretaria);
                });

            migrationBuilder.CreateTable(
                name: "secretariaVeterinarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VeterinarioId = table.Column<int>(type: "int", nullable: false),
                    SecretariaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secretariaVeterinarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "veterinarios",
                columns: table => new
                {
                    IdVeterinario = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    consultorio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veterinarios", x => x.IdVeterinario);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    consultorioIdConsultorio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clientes_consultorios_consultorioIdConsultorio",
                        column: x => x.consultorioIdConsultorio,
                        principalTable: "consultorios",
                        principalColumn: "IdConsultorio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_consultorioIdConsultorio",
                table: "clientes",
                column: "consultorioIdConsultorio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "diasLaboralesVet");

            migrationBuilder.DropTable(
                name: "secretariasVeterinarios");

            migrationBuilder.DropTable(
                name: "secretariaVeterinarios");

            migrationBuilder.DropTable(
                name: "veterinarios");

            migrationBuilder.DropTable(
                name: "consultorios");
        }
    }
}
