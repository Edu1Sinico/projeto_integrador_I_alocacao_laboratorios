using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLocLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    IdDisciplina = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeDisciplina = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    QtdeAlunos = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.IdDisciplina);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorios",
                columns: table => new
                {
                    IDLaboratorio = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroLaboratorio = table.Column<int>(type: "integer", nullable: false),
                    qtdeComputador = table.Column<int>(type: "integer", nullable: false),
                    capacidadeMaxAluno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorios", x => x.IDLaboratorio);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    IdSoftware = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeSoftware = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Versao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.IdSoftware);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    RE = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaSoftware",
                columns: table => new
                {
                    DisciplinasIdDisciplina = table.Column<Guid>(type: "uuid", nullable: false),
                    SoftwaresIdSoftware = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaSoftware", x => new { x.DisciplinasIdDisciplina, x.SoftwaresIdSoftware });
                    table.ForeignKey(
                        name: "FK_DisciplinaSoftware_Disciplinas_DisciplinasIdDisciplina",
                        column: x => x.DisciplinasIdDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaSoftware_Softwares_SoftwaresIdSoftware",
                        column: x => x.SoftwaresIdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoriosSoftware",
                columns: table => new
                {
                    LaboratoriosIDLaboratorio = table.Column<Guid>(type: "uuid", nullable: false),
                    SoftwaresIdSoftware = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoriosSoftware", x => new { x.LaboratoriosIDLaboratorio, x.SoftwaresIdSoftware });
                    table.ForeignKey(
                        name: "FK_LaboratoriosSoftware_Laboratorios_LaboratoriosIDLaboratorio",
                        column: x => x.LaboratoriosIDLaboratorio,
                        principalTable: "Laboratorios",
                        principalColumn: "IDLaboratorio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoriosSoftware_Softwares_SoftwaresIdSoftware",
                        column: x => x.SoftwaresIdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alocacoes",
                columns: table => new
                {
                    IdAlocacao = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LaboratorioId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alocacoes", x => x.IdAlocacao);
                    table.ForeignKey(
                        name: "FK_Alocacoes_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alocacoes_Laboratorios_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorios",
                        principalColumn: "IDLaboratorio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alocacoes_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alocacoes_DisciplinaId",
                table: "Alocacoes",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alocacoes_LaboratorioId",
                table: "Alocacoes",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Alocacoes_UsuarioId",
                table: "Alocacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaSoftware_SoftwaresIdSoftware",
                table: "DisciplinaSoftware",
                column: "SoftwaresIdSoftware");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoriosSoftware_SoftwaresIdSoftware",
                table: "LaboratoriosSoftware",
                column: "SoftwaresIdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alocacoes");

            migrationBuilder.DropTable(
                name: "DisciplinaSoftware");

            migrationBuilder.DropTable(
                name: "LaboratoriosSoftware");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Laboratorios");

            migrationBuilder.DropTable(
                name: "Softwares");
        }
    }
}
