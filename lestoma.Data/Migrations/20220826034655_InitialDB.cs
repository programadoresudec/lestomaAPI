using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class InitialDB : Migration
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
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_modulo = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    modulo_componente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actividad_id = table.Column<Guid>(type: "uuid", nullable: false),
                    upa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_componente = table.Column<string>(type: "text", nullable: true),
                    descripcion_estado = table.Column<string>(type: "jsonb", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    session = table.Column<string>(type: "text", nullable: true),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_componente_laboratorio", x => x.id);
                    table.ForeignKey(
                        name: "FK_componente_laboratorio_actividad_actividad_id",
                        column: x => x.actividad_id,
                        principalSchema: "superadmin",
                        principalTable: "actividad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    { new Guid("0ae66223-7d06-4578-b740-352912786240"), new DateTime(2022, 8, 25, 22, 46, 54, 576, DateTimeKind.Local).AddTicks(6321), "192.168.1.6", "ACTUADORES", "Anonimo", "Local" },
                    { new Guid("a1a443dd-4d80-4171-a5b7-aed140787af7"), new DateTime(2022, 8, 25, 22, 46, 54, 578, DateTimeKind.Local).AddTicks(2722), "192.168.1.6", "SENSORES", "Anonimo", "Local" }
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
                    { new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(3977), "192.168.1.6", "control de agua", "Anonimo", "Local" },
                    { new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(4002), "192.168.1.6", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(1247), "192.168.1.6", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("18f736ab-ce68-4d88-a0d1-ef05dcbc140f"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(1282), "192.168.1.6", "ucundinamarca", "Anonimo", 1, "Local" }
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
                    { new Guid("5d371d35-f20d-4115-9efc-d549ede92ee0"), new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(4516), "192.168.1.6", "{\"Id\":\"c1d0a88e-3a5f-41e2-a1c2-4e1a7de34d42\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("0ae66223-7d06-4578-b740-352912786240"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f") },
                    { new Guid("6a89ec3e-61d7-4f61-a034-b89b43e457d5"), new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(4568), "192.168.1.6", "{\"Id\":\"c1d0a88e-3a5f-41e2-a1c2-4e1a7de34d42\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("0ae66223-7d06-4578-b740-352912786240"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f") },
                    { new Guid("7c381c30-80d5-4093-8c31-d9f71395aea6"), new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(4574), "192.168.1.6", "{\"Id\":\"c1d0a88e-3a5f-41e2-a1c2-4e1a7de34d42\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("0ae66223-7d06-4578-b740-352912786240"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f") },
                    { new Guid("b13a7f2b-969e-4d1a-b786-a1dc47cc1423"), new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(4577), "192.168.1.6", "{\"Id\":\"e5af5e30-5683-4332-8015-48978a17b799\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("a1a443dd-4d80-4171-a5b7-aed140787af7"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f") },
                    { new Guid("06326ac6-c21d-4189-9e4a-9681425b00d4"), new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(4581), "192.168.1.6", "{\"Id\":\"e5af5e30-5683-4332-8015-48978a17b799\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("a1a443dd-4d80-4171-a5b7-aed140787af7"), "PH", "Anonimo", "Local", new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f") },
                    { new Guid("2155080c-83ea-4e75-b645-8956e2982446"), new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(4584), "192.168.1.6", "{\"Id\":\"e5af5e30-5683-4332-8015-48978a17b799\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("a1a443dd-4d80-4171-a5b7-aed140787af7"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f") }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f"), 2, new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(7892), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f"), 2, new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(7908), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f"), 3, new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(7910), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new Guid("fe089e47-6780-4f0f-8bca-f608e0cf740f"), 3, new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(7912), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("240106f0-6882-4324-8fe1-f9e2e3bf739b"), new Guid("18f736ab-ce68-4d88-a0d1-ef05dcbc140f"), 4, new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(7915), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("eadfbf7a-9dcb-478d-88bd-26355878b8a7"), new Guid("18f736ab-ce68-4d88-a0d1-ef05dcbc140f"), 4, new DateTime(2022, 8, 25, 22, 46, 54, 606, DateTimeKind.Local).AddTicks(7917), "192.168.1.6", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "P8aY6NNrvS9qN4U3JUbXUjKMnT3xJqSNHkapHz0NSIs=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 8, 25, 22, 46, 54, 590, DateTimeKind.Local).AddTicks(2707), null, "192.168.1.6", "Super Admin", 1, "qHOuN/KuGBcjfqTdV93Oog==", "Anonimo", "Local" },
                    { 2, "Lestoma", "vPBqYJqiJSmToTIgC9i6LyAvwYcR9jGqU4Hy6PPhA3k=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 8, 25, 22, 46, 54, 598, DateTimeKind.Local).AddTicks(83), null, "192.168.1.6", "Administrador", 2, "QIwPzUlZ2tsLXYphpxC4rg==", "Anonimo", "Local" },
                    { 3, "Lestoma", "dRE/4IcCXKZwn4LVcRZKoPCs3z3gu8FglDYB9m8S7yE=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 8, 25, 22, 46, 54, 605, DateTimeKind.Local).AddTicks(2399), null, "192.168.1.6", "Auxiliar 1", 3, "UTqrlysx1CFGM2EE20ec3w==", "Anonimo", "Local" },
                    { 4, "Lestoma", "dRE/4IcCXKZwn4LVcRZKoPCs3z3gu8FglDYB9m8S7yE=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 8, 25, 22, 46, 54, 605, DateTimeKind.Local).AddTicks(2428), null, "192.168.1.6", "Auxiliar 2", 3, "UTqrlysx1CFGM2EE20ec3w==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "set_point", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada" },
                values: new object[,]
                {
                    { new Guid("3d338823-98a9-4dfe-b357-eadba3c07c17"), new Guid("5d371d35-f20d-4115-9efc-d549ede92ee0"), true, new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(8327), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(8309), "192.168.1.6", "Anonimo", null, "Local", 1, "4901F000000000006180" },
                    { new Guid("4c4432c3-ef57-40c1-8e0b-7bd7d3834ab4"), new Guid("6a89ec3e-61d7-4f61-a034-b89b43e457d5"), true, new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(9909), new DateTime(2022, 8, 25, 22, 46, 54, 625, DateTimeKind.Local).AddTicks(9898), "192.168.1.6", "Anonimo", null, "Local", 2, "6F01F000000000005302" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_componente_laboratorio_actividad_id",
                schema: "laboratorio_lestoma",
                table: "componente_laboratorio",
                column: "actividad_id");

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
                name: "usuario",
                schema: "usuarios");

            migrationBuilder.DropTable(
                name: "actividad",
                schema: "superadmin");

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
