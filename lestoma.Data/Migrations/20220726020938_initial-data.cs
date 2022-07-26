using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class initialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "superadmin");

            migrationBuilder.EnsureSchema(
                name: "reportes");

            migrationBuilder.EnsureSchema(
                name: "seguridad");

            migrationBuilder.EnsureSchema(
                name: "laboratorio_lestoma");

            migrationBuilder.EnsureSchema(
                name: "usuarios");

            migrationBuilder.CreateTable(
                name: "actividad",
                schema: "superadmin",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_actividad = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actividad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alimentar_peces",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: true),
                    detalle_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior_registro_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alimentar_peces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aplicacion",
                schema: "seguridad",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    tiempo_expiracion_token = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aplicacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auditoria",
                schema: "seguridad",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    schema = table.Column<string>(type: "text", nullable: true),
                    tabla = table.Column<string>(type: "text", nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    accion = table.Column<string>(type: "text", nullable: true),
                    user_bd = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<string>(type: "jsonb", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    pk = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "buzon",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "jsonb", nullable: true),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buzon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "control_de_agua",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: true),
                    detalle_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior_registro_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_de_agua", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "control_de_entorno",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: true),
                    detalle_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior_registro_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_de_entorno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "control_electrico",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: true),
                    detalle_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior_registro_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_electrico", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "control_hidroponico",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: true),
                    detalle_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior_registro_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_hidroponico", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estado_usuario",
                schema: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modulo_componente",
                schema: "laboratorio_lestoma",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_modulo = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulo_componente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "protocolo_com",
                schema: "laboratorio_lestoma",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    primer_byte_trama = table.Column<string>(type: "text", nullable: true),
                    sigla = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_protocolo_com", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recirculacion_de_agua",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: true),
                    detalle_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior_registro_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recirculacion_de_agua", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                schema: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "super_administrador",
                schema: "superadmin",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_super_administrador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "upa",
                schema: "superadmin",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_upa = table.Column<string>(type: "text", nullable: true),
                    superadmin_id = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    cantidad_actividades = table.Column<short>(type: "smallint", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                schema: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    apellido = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    clave = table.Column<string>(type: "text", nullable: true),
                    codigo_recuperacion = table.Column<string>(type: "text", nullable: true),
                    vencimiento_codigo_recuperacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    rol_id = table.Column<int>(type: "integer", nullable: false),
                    estado_id = table.Column<int>(type: "integer", nullable: false),
                    semilla = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuario_estado_usuario_estado_id",
                        column: x => x.estado_id,
                        principalSchema: "usuarios",
                        principalTable: "estado_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_rol_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "usuarios",
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "componente_laboratorio",
                schema: "laboratorio_lestoma",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modulo_componente_id = table.Column<int>(type: "integer", nullable: false),
                    actividad_id = table.Column<Guid>(type: "uuid", nullable: false),
                    upa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_componente = table.Column<string>(type: "text", nullable: true),
                    descripcion_estado = table.Column<string>(type: "jsonb", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_componente_laboratorio", x => x.id);
                    table.ForeignKey(
                        name: "FK_componente_laboratorio_modulo_componente_modulo_componente_~",
                        column: x => x.modulo_componente_id,
                        principalSchema: "laboratorio_lestoma",
                        principalTable: "modulo_componente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_componente_laboratorio_upa_upa_id",
                        column: x => x.upa_id,
                        principalSchema: "superadmin",
                        principalTable: "upa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens_usuario_por_aplicacion",
                schema: "seguridad",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: true),
                    expiracion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creado_por_ip = table.Column<string>(type: "text", nullable: true),
                    fecha_revocacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    revocado_por_ip = table.Column<string>(type: "text", nullable: true),
                    reeemplazado_por_token = table.Column<string>(type: "text", nullable: true),
                    aplicacion_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens_usuario_por_aplicacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_tokens_usuario_por_aplicacion_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "usuarios",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "upa_actividad",
                schema: "superadmin",
                columns: table => new
                {
                    upa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actividad_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upa_actividad", x => new { x.upa_id, x.actividad_id, x.usuario_id });
                    table.ForeignKey(
                        name: "FK_upa_actividad_actividad_actividad_id",
                        column: x => x.actividad_id,
                        principalSchema: "superadmin",
                        principalTable: "actividad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_upa_actividad_upa_upa_id",
                        column: x => x.upa_id,
                        principalSchema: "superadmin",
                        principalTable: "upa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_upa_actividad_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "usuarios",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_laboratorio",
                schema: "laboratorio_lestoma",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    componente_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_com_id = table.Column<int>(type: "integer", nullable: false),
                    set_point = table.Column<double>(type: "double precision", nullable: true),
                    trama_enviada = table.Column<string>(type: "text", nullable: true),
                    estado_internet = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_creacion_dispositivo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_laboratorio", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_laboratorio_componente_laboratorio_componente_labor~",
                        column: x => x.componente_laboratorio_id,
                        principalSchema: "laboratorio_lestoma",
                        principalTable: "componente_laboratorio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_laboratorio_protocolo_com_tipo_com_id",
                        column: x => x.tipo_com_id,
                        principalSchema: "laboratorio_lestoma",
                        principalTable: "protocolo_com",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "modulo_componente",
                columns: new[] { "id", "fecha_creacion_server", "ip", "nombre_modulo", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 25, 21, 9, 37, 541, DateTimeKind.Local).AddTicks(3901), "10.212.135.7", "ACTUADORES", "Anonimo", "Local" },
                    { 2, new DateTime(2022, 7, 25, 21, 9, 37, 543, DateTimeKind.Local).AddTicks(743), "10.212.135.7", "SENSORES", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "protocolo_com",
                columns: new[] { "id", "nombre", "primer_byte_trama", "sigla" },
                values: new object[,]
                {
                    { 2, "Broad Cast", "6F", "BS" },
                    { 1, "Peer to Peer", "49", "PP" }
                });

            migrationBuilder.InsertData(
                schema: "seguridad",
                table: "aplicacion",
                columns: new[] { "id", "nombre", "tiempo_expiracion_token" },
                values: new object[,]
                {
                    { 1, "App Movil", (short)31 },
                    { 2, "Web", (short)45 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "actividad",
                columns: new[] { "id", "fecha_creacion_server", "ip", "nombre_actividad", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(4155), "10.212.135.7", "control de agua", "Anonimo", "Local" },
                    { new Guid("c04ef552-5ac2-4df9-8d7b-98897d7e6295"), new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(4180), "10.212.135.7", "alimentacion de peces", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "super_administrador",
                columns: new[] { "id", "usuario_id" },
                values: new object[] { (short)1, (short)1 });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa",
                columns: new[] { "id", "cantidad_actividades", "descripcion", "fecha_creacion_server", "ip", "nombre_upa", "session", "superadmin_id", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(1718), "10.212.135.7", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("4b489e27-ece0-41db-a6a3-a86d659f1de4"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(1752), "10.212.135.7", "ucundinamarca", "Anonimo", 1, "Local" }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "estado_usuario",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { 4, "Bloqueado" },
                    { 2, "Activado" },
                    { 1, "verificar cuenta" },
                    { 3, "Inactivo" }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "rol",
                columns: new[] { "id", "nombre_rol" },
                values: new object[,]
                {
                    { 1, "Super Administrador" },
                    { 2, "Administrador" },
                    { 3, "Auxiliar" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "componente_laboratorio",
                columns: new[] { "id", "actividad_id", "fecha_creacion_server", "ip", "descripcion_estado", "modulo_componente_id", "nombre_componente", "session", "tipo_de_aplicacion", "upa_id" },
                values: new object[,]
                {
                    { new Guid("a933497c-b8e5-4aac-9b1f-5b10414c082f"), new Guid("c04ef552-5ac2-4df9-8d7b-98897d7e6295"), new DateTime(2022, 7, 25, 21, 9, 37, 591, DateTimeKind.Local).AddTicks(9840), "10.212.135.7", "{\"Id\":\"97509b12-d877-4bac-bdde-a2c81dc7fcef\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", 1, "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a") },
                    { new Guid("bf9a5b1b-fe68-41e5-bd6e-38592e337fbf"), new Guid("c04ef552-5ac2-4df9-8d7b-98897d7e6295"), new DateTime(2022, 7, 25, 21, 9, 37, 591, DateTimeKind.Local).AddTicks(9879), "10.212.135.7", "{\"Id\":\"97509b12-d877-4bac-bdde-a2c81dc7fcef\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", 1, "LUZ ESTANQUE", "Anonimo", "Local", new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a") },
                    { new Guid("65777bf9-6e21-4f44-bef3-afed0e2860d8"), new Guid("c04ef552-5ac2-4df9-8d7b-98897d7e6295"), new DateTime(2022, 7, 25, 21, 9, 37, 591, DateTimeKind.Local).AddTicks(9884), "10.212.135.7", "{\"Id\":\"97509b12-d877-4bac-bdde-a2c81dc7fcef\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", 1, "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a") },
                    { new Guid("a0affd81-5be5-4a76-8e66-2007214f14b1"), new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new DateTime(2022, 7, 25, 21, 9, 37, 591, DateTimeKind.Local).AddTicks(9889), "10.212.135.7", "{\"Id\":\"e0ac82fe-5c64-48d6-8d43-6158df589b26\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", 2, "TEMPERATURA H2O", "Anonimo", "Local", new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a") },
                    { new Guid("c61b76d1-636a-458e-899d-1c62deac672a"), new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new DateTime(2022, 7, 25, 21, 9, 37, 591, DateTimeKind.Local).AddTicks(9893), "10.212.135.7", "{\"Id\":\"e0ac82fe-5c64-48d6-8d43-6158df589b26\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", 2, "PH", "Anonimo", "Local", new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a") },
                    { new Guid("0618f64b-cf4c-4569-b8be-b0306eea76ac"), new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new DateTime(2022, 7, 25, 21, 9, 37, 591, DateTimeKind.Local).AddTicks(9921), "10.212.135.7", "{\"Id\":\"e0ac82fe-5c64-48d6-8d43-6158df589b26\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", 2, "NIVEL TANQUE", "Anonimo", "Local", new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "oXDvGpj1612barpNvxwfbv36HIlRvqeZrRnHSUa0AzM=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 7, 25, 21, 9, 37, 555, DateTimeKind.Local).AddTicks(5603), null, "10.212.135.7", "Super Admin", 1, "M57TAJBuPu9lO00CWSMRYQ==", "Anonimo", "Local" },
                    { 2, "Lestoma", "wKt45yvYJWmjB5+LoI6QWRx+MIQt5fCppGvfbeYpUY0=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 7, 25, 21, 9, 37, 563, DateTimeKind.Local).AddTicks(6124), null, "10.212.135.7", "Administrador", 2, "zZYq+Chup+FSDLV4Zv+IfA==", "Anonimo", "Local" },
                    { 3, "Lestoma", "gxs6YWr5hsiOXWDbQhQbUut+nehqfQvItlx6e2R8db8=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 7, 25, 21, 9, 37, 571, DateTimeKind.Local).AddTicks(3510), null, "10.212.135.7", "Auxiliar 1", 3, "zYszfTg4A0Kw/vODue5eHw==", "Anonimo", "Local" },
                    { 4, "Lestoma", "gxs6YWr5hsiOXWDbQhQbUut+nehqfQvItlx6e2R8db8=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 7, 25, 21, 9, 37, 571, DateTimeKind.Local).AddTicks(3525), null, "10.212.135.7", "Auxiliar 2", 3, "zYszfTg4A0Kw/vODue5eHw==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "set_point", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada" },
                values: new object[,]
                {
                    { new Guid("96d219da-7751-428b-a80f-53b3e552183e"), new Guid("a933497c-b8e5-4aac-9b1f-5b10414c082f"), true, new DateTime(2022, 7, 25, 21, 9, 37, 592, DateTimeKind.Local).AddTicks(3375), new DateTime(2022, 7, 25, 21, 9, 37, 592, DateTimeKind.Local).AddTicks(3360), "10.212.135.7", "Anonimo", null, "Local", 1, "4901F000000000006180" },
                    { new Guid("57a115f0-0604-4d57-b69e-e63b44ebd600"), new Guid("bf9a5b1b-fe68-41e5-bd6e-38592e337fbf"), true, new DateTime(2022, 7, 25, 21, 9, 37, 592, DateTimeKind.Local).AddTicks(4994), new DateTime(2022, 7, 25, 21, 9, 37, 592, DateTimeKind.Local).AddTicks(4981), "10.212.135.7", "Anonimo", null, "Local", 2, "6F01F000000000005302" }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a"), 2, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8149), "10.212.135.7", "Anonimo", "Local" },
                    { new Guid("c04ef552-5ac2-4df9-8d7b-98897d7e6295"), new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a"), 2, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8167), "10.212.135.7", "Anonimo", "Local" },
                    { new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new Guid("4b489e27-ece0-41db-a6a3-a86d659f1de4"), 2, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8178), "10.212.135.7", "Anonimo", "Local" },
                    { new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a"), 3, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8172), "10.212.135.7", "Anonimo", "Local" },
                    { new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new Guid("4b489e27-ece0-41db-a6a3-a86d659f1de4"), 3, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8182), "10.212.135.7", "Anonimo", "Local" },
                    { new Guid("c04ef552-5ac2-4df9-8d7b-98897d7e6295"), new Guid("f0fd706b-6913-4cee-b433-b4a63e7ec51a"), 4, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8175), "10.212.135.7", "Anonimo", "Local" },
                    { new Guid("14938fd4-42dc-4e53-99bf-6f16be59b4d6"), new Guid("4b489e27-ece0-41db-a6a3-a86d659f1de4"), 4, new DateTime(2022, 7, 25, 21, 9, 37, 572, DateTimeKind.Local).AddTicks(8184), "10.212.135.7", "Anonimo", "Local" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_componente_laboratorio_modulo_componente_id",
                schema: "laboratorio_lestoma",
                table: "componente_laboratorio",
                column: "modulo_componente_id");

            migrationBuilder.CreateIndex(
                name: "IX_componente_laboratorio_upa_id",
                schema: "laboratorio_lestoma",
                table: "componente_laboratorio",
                column: "upa_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_laboratorio_componente_laboratorio_id",
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                column: "componente_laboratorio_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_laboratorio_tipo_com_id",
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                column: "tipo_com_id");

            migrationBuilder.CreateIndex(
                name: "IX_tokens_usuario_por_aplicacion_usuario_id",
                schema: "seguridad",
                table: "tokens_usuario_por_aplicacion",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_upa_actividad_actividad_id",
                schema: "superadmin",
                table: "upa_actividad",
                column: "actividad_id");

            migrationBuilder.CreateIndex(
                name: "IX_upa_actividad_usuario_id",
                schema: "superadmin",
                table: "upa_actividad",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_estado_id",
                schema: "usuarios",
                table: "usuario",
                column: "estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_rol_id",
                schema: "usuarios",
                table: "usuario",
                column: "rol_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alimentar_peces",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "aplicacion",
                schema: "seguridad");

            migrationBuilder.DropTable(
                name: "auditoria",
                schema: "seguridad");

            migrationBuilder.DropTable(
                name: "buzon",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "control_de_agua",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "control_de_entorno",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "control_electrico",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "control_hidroponico",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "detalle_laboratorio",
                schema: "laboratorio_lestoma");

            migrationBuilder.DropTable(
                name: "recirculacion_de_agua",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "super_administrador",
                schema: "superadmin");

            migrationBuilder.DropTable(
                name: "tokens_usuario_por_aplicacion",
                schema: "seguridad");

            migrationBuilder.DropTable(
                name: "upa_actividad",
                schema: "superadmin");

            migrationBuilder.DropTable(
                name: "componente_laboratorio",
                schema: "laboratorio_lestoma");

            migrationBuilder.DropTable(
                name: "protocolo_com",
                schema: "laboratorio_lestoma");

            migrationBuilder.DropTable(
                name: "actividad",
                schema: "superadmin");

            migrationBuilder.DropTable(
                name: "usuario",
                schema: "usuarios");

            migrationBuilder.DropTable(
                name: "modulo_componente",
                schema: "laboratorio_lestoma");

            migrationBuilder.DropTable(
                name: "upa",
                schema: "superadmin");

            migrationBuilder.DropTable(
                name: "estado_usuario",
                schema: "usuarios");

            migrationBuilder.DropTable(
                name: "rol",
                schema: "usuarios");
        }
    }
}
