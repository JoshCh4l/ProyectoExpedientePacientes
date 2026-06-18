using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoExpedientePacientes.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambiosModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicamentoPaciente_Medico_MedicoId",
                table: "MedicamentoPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaClinica_Medico_MedicoId",
                table: "NotaClinica");

            migrationBuilder.DropForeignKey(
                name: "FK_PadecimientoPaciente_Medico_MedicoId",
                table: "PadecimientoPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultadoExamenMedico_Medico_MedicoId",
                table: "ResultadoExamenMedico");

            migrationBuilder.DropForeignKey(
                name: "FK_TratamientoPaciente_Medico_MedicoId",
                table: "TratamientoPaciente");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "TratamientoPaciente",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_TratamientoPaciente_MedicoId",
                table: "TratamientoPaciente",
                newName: "IX_TratamientoPaciente_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "ResultadoExamenMedico",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultadoExamenMedico_MedicoId",
                table: "ResultadoExamenMedico",
                newName: "IX_ResultadoExamenMedico_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "PadecimientoPaciente",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_PadecimientoPaciente_MedicoId",
                table: "PadecimientoPaciente",
                newName: "IX_PadecimientoPaciente_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "NotaClinica",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_NotaClinica_MedicoId",
                table: "NotaClinica",
                newName: "IX_NotaClinica_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "MedicamentoPaciente",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicamentoPaciente_MedicoId",
                table: "MedicamentoPaciente",
                newName: "IX_MedicamentoPaciente_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicamentoPaciente_Usuario_UsuarioId",
                table: "MedicamentoPaciente",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotaClinica_Usuario_UsuarioId",
                table: "NotaClinica",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PadecimientoPaciente_Usuario_UsuarioId",
                table: "PadecimientoPaciente",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadoExamenMedico_Usuario_UsuarioId",
                table: "ResultadoExamenMedico",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TratamientoPaciente_Usuario_UsuarioId",
                table: "TratamientoPaciente",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicamentoPaciente_Usuario_UsuarioId",
                table: "MedicamentoPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaClinica_Usuario_UsuarioId",
                table: "NotaClinica");

            migrationBuilder.DropForeignKey(
                name: "FK_PadecimientoPaciente_Usuario_UsuarioId",
                table: "PadecimientoPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultadoExamenMedico_Usuario_UsuarioId",
                table: "ResultadoExamenMedico");

            migrationBuilder.DropForeignKey(
                name: "FK_TratamientoPaciente_Usuario_UsuarioId",
                table: "TratamientoPaciente");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "TratamientoPaciente",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_TratamientoPaciente_UsuarioId",
                table: "TratamientoPaciente",
                newName: "IX_TratamientoPaciente_MedicoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ResultadoExamenMedico",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultadoExamenMedico_UsuarioId",
                table: "ResultadoExamenMedico",
                newName: "IX_ResultadoExamenMedico_MedicoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "PadecimientoPaciente",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_PadecimientoPaciente_UsuarioId",
                table: "PadecimientoPaciente",
                newName: "IX_PadecimientoPaciente_MedicoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "NotaClinica",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_NotaClinica_UsuarioId",
                table: "NotaClinica",
                newName: "IX_NotaClinica_MedicoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "MedicamentoPaciente",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicamentoPaciente_UsuarioId",
                table: "MedicamentoPaciente",
                newName: "IX_MedicamentoPaciente_MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicamentoPaciente_Medico_MedicoId",
                table: "MedicamentoPaciente",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotaClinica_Medico_MedicoId",
                table: "NotaClinica",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PadecimientoPaciente_Medico_MedicoId",
                table: "PadecimientoPaciente",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadoExamenMedico_Medico_MedicoId",
                table: "ResultadoExamenMedico",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TratamientoPaciente_Medico_MedicoId",
                table: "TratamientoPaciente",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
