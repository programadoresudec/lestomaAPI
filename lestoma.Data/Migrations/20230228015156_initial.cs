using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "superadmin");

            migrationBuilder.EnsureSchema(
                name: "seguridad");

            migrationBuilder.EnsureSchema(
                name: "reportes");

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
                    nombre_actividad = table.Column<string>(type: "text", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_actualizacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actividad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aplicacion",
                schema: "seguridad",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
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
                    descripcion = table.Column<string>(type: "jsonb", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buzon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estado_usuario",
                schema: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
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
                    nombre_modulo = table.Column<string>(type: "text", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_actualizacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulo_componente", x => x.id);
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
                    nombre_upa = table.Column<string>(type: "text", nullable: false),
                    superadmin_id = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    cantidad_actividades = table.Column<short>(type: "smallint", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_actualizacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    clave = table.Column<string>(type: "text", nullable: false),
                    codigo_recuperacion = table.Column<string>(type: "text", nullable: true),
                    vencimiento_codigo_recuperacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    rol_id = table.Column<int>(type: "integer", nullable: false),
                    estado_id = table.Column<int>(type: "integer", nullable: false),
                    semilla = table.Column<string>(type: "text", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_actualizacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuario_rol_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "usuarios",
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    nombre_componente = table.Column<string>(type: "text", nullable: false),
                    descripcion_estado = table.Column<string>(type: "jsonb", nullable: false),
                    direccion_registro = table.Column<byte>(type: "smallint", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_actualizacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_componente_laboratorio_modulo_componente_modulo_componente_~",
                        column: x => x.modulo_componente_id,
                        principalSchema: "laboratorio_lestoma",
                        principalTable: "modulo_componente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_componente_laboratorio_upa_upa_id",
                        column: x => x.upa_id,
                        principalSchema: "superadmin",
                        principalTable: "upa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "protocolo_com",
                schema: "laboratorio_lestoma",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    upa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    primer_byte_trama = table.Column<byte>(type: "smallint", nullable: false),
                    sigla = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_protocolo_com", x => x.id);
                    table.ForeignKey(
                        name: "FK_protocolo_com_upa_upa_id",
                        column: x => x.upa_id,
                        principalSchema: "superadmin",
                        principalTable: "upa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_actualizacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_upa_actividad_upa_upa_id",
                        column: x => x.upa_id,
                        principalSchema: "superadmin",
                        principalTable: "upa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_upa_actividad_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "usuarios",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "detalle_laboratorio",
                schema: "laboratorio_lestoma",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    componente_laboratorio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dato_trama_enviada = table.Column<double>(type: "double precision", nullable: true),
                    dato_trama_recibida = table.Column<double>(type: "double precision", nullable: true),
                    trama_enviada = table.Column<string>(type: "text", nullable: true),
                    trama_recibida = table.Column<string>(type: "text", nullable: true),
                    estado_internet = table.Column<bool>(type: "boolean", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion_server = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_creacion_dispositivo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "modulo_componente",
                columns: new[] { "id", "fecha_actualizacion_server", "fecha_creacion_server", "ip", "nombre_modulo", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("7387b7ae-26af-434f-a83c-e29135629f97"), null, new DateTime(2023, 2, 27, 20, 51, 54, 992, DateTimeKind.Local).AddTicks(1392), "N/A", "ACTUADORES", "Anonimo", "Local" },
                    { new Guid("4edb4a8e-167b-4019-8e37-dd486051f4b3"), null, new DateTime(2023, 2, 27, 20, 51, 54, 992, DateTimeKind.Local).AddTicks(3533), "N/A", "SET_POINT/CONTROL", "Anonimo", "Local" },
                    { new Guid("05dc2a4b-7add-4d2a-94cd-f5be14472739"), null, new DateTime(2023, 2, 27, 20, 51, 54, 992, DateTimeKind.Local).AddTicks(3520), "N/A", "SENSORES", "Anonimo", "Local" }
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
                columns: new[] { "id", "fecha_actualizacion_server", "fecha_creacion_server", "ip", "nombre_actividad", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), null, new DateTime(2023, 2, 27, 20, 51, 55, 37, DateTimeKind.Local).AddTicks(9846), "N/A", "control de agua", "Anonimo", "Local" },
                    { new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), null, new DateTime(2023, 2, 27, 20, 51, 55, 37, DateTimeKind.Local).AddTicks(9881), "N/A", "alimentacion de peces", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "super_administrador",
                columns: new[] { "id", "usuario_id" },
                values: new object[,]
                {
                    { (short)1, (short)1 },
                    { (short)2, (short)2 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa",
                columns: new[] { "id", "cantidad_actividades", "descripcion", "fecha_actualizacion_server", "fecha_creacion_server", "ip", "nombre_upa", "session", "superadmin_id", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720"), (short)5, "queda ubicada en facatativá", null, new DateTime(2023, 2, 27, 20, 51, 55, 33, DateTimeKind.Local).AddTicks(6056), "N/A", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("9d935e26-2353-447e-97ba-5c3eccab9059"), (short)2, "queda ubicada en la universidad cundinamarca extensión faca", null, new DateTime(2023, 2, 27, 20, 51, 55, 33, DateTimeKind.Local).AddTicks(6093), "N/A", "ucundinamarca", "Anonimo", 1, "Local" }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "estado_usuario",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { 3, "Inactivo" },
                    { 4, "Bloqueado" },
                    { 1, "verificar cuenta" },
                    { 2, "Activado" }
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
                columns: new[] { "id", "actividad_id", "direccion_registro", "fecha_actualizacion_server", "fecha_creacion_server", "ip", "descripcion_estado", "modulo_componente_id", "nombre_componente", "session", "tipo_de_aplicacion", "upa_id" },
                values: new object[,]
                {
                    { new Guid("e95f08ab-6b4e-4d1f-aea7-85f0dc6848bf"), new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), (byte)0, null, new DateTime(2023, 2, 27, 20, 51, 55, 169, DateTimeKind.Local).AddTicks(9043), "N/A", "{\r\n  \"Id\": \"98a74bd5-7390-4244-8b2e-255d3707071d\",\r\n  \"TipoEstado\": \"ON-OFF\",\r\n  \"ByteHexaFuncion\": \"F0\",\r\n  \"ByteDecimalFuncion\": 240\r\n}", new Guid("7387b7ae-26af-434f-a83c-e29135629f97"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { new Guid("853929b3-bee1-4601-9cf0-8704d43fb771"), new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), (byte)1, null, new DateTime(2023, 2, 27, 20, 51, 55, 169, DateTimeKind.Local).AddTicks(9479), "N/A", "{\r\n  \"Id\": \"98a74bd5-7390-4244-8b2e-255d3707071d\",\r\n  \"TipoEstado\": \"ON-OFF\",\r\n  \"ByteHexaFuncion\": \"F0\",\r\n  \"ByteDecimalFuncion\": 240\r\n}", new Guid("7387b7ae-26af-434f-a83c-e29135629f97"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { new Guid("4a8e6897-7a48-4f18-92c0-ad07b24a1aaf"), new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), (byte)3, null, new DateTime(2023, 2, 27, 20, 51, 55, 170, DateTimeKind.Local).AddTicks(9670), "N/A", "{\r\n  \"Id\": \"98a74bd5-7390-4244-8b2e-255d3707071d\",\r\n  \"TipoEstado\": \"ON-OFF\",\r\n  \"ByteHexaFuncion\": \"F0\",\r\n  \"ByteDecimalFuncion\": 240\r\n}", new Guid("7387b7ae-26af-434f-a83c-e29135629f97"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { new Guid("629a5dcc-c2ec-4282-a619-3d52b0d5c5d2"), new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), (byte)2, null, new DateTime(2023, 2, 27, 20, 51, 55, 170, DateTimeKind.Local).AddTicks(9687), "N/A", "{\r\n  \"Id\": \"f5f738c7-0dba-48ee-afea-b22530160653\",\r\n  \"TipoEstado\": \"LECTURA\",\r\n  \"ByteHexaFuncion\": \"0F\",\r\n  \"ByteDecimalFuncion\": 15\r\n}", new Guid("05dc2a4b-7add-4d2a-94cd-f5be14472739"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { new Guid("556a8400-6ca7-4986-b1a8-b5d9982e1ffd"), new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), (byte)0, null, new DateTime(2023, 2, 27, 20, 51, 55, 170, DateTimeKind.Local).AddTicks(9714), "N/A", "{\r\n  \"Id\": \"f5f738c7-0dba-48ee-afea-b22530160653\",\r\n  \"TipoEstado\": \"LECTURA\",\r\n  \"ByteHexaFuncion\": \"0F\",\r\n  \"ByteDecimalFuncion\": 15\r\n}", new Guid("05dc2a4b-7add-4d2a-94cd-f5be14472739"), "PH", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { new Guid("8d0f4ea7-c228-4a2a-b804-5612bf31d997"), new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), (byte)1, null, new DateTime(2023, 2, 27, 20, 51, 55, 170, DateTimeKind.Local).AddTicks(9719), "N/A", "{\r\n  \"Id\": \"f5f738c7-0dba-48ee-afea-b22530160653\",\r\n  \"TipoEstado\": \"LECTURA\",\r\n  \"ByteHexaFuncion\": \"0F\",\r\n  \"ByteDecimalFuncion\": 15\r\n}", new Guid("05dc2a4b-7add-4d2a-94cd-f5be14472739"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { new Guid("6b693418-b841-4284-9315-6659b5d255e9"), new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), (byte)7, null, new DateTime(2023, 2, 27, 20, 51, 55, 170, DateTimeKind.Local).AddTicks(9725), "N/A", "{\r\n  \"Id\": \"c781773b-7d7c-47f7-b5d0-34a4943ba907\",\r\n  \"TipoEstado\": \"AJUSTE\",\r\n  \"ByteHexaFuncion\": \"F0\",\r\n  \"ByteDecimalFuncion\": 240\r\n}", new Guid("4edb4a8e-167b-4019-8e37-dd486051f4b3"), "SP_TEMPERATURA H2O", "Anonimo", "Local", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "protocolo_com",
                columns: new[] { "id", "nombre", "primer_byte_trama", "sigla", "upa_id" },
                values: new object[,]
                {
                    { 1, "Peer to Peer", (byte)73, "PP", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { 2, "Broad Cast", (byte)111, "BS", new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720") },
                    { 3, "Peer to Peer", (byte)73, "PP", new Guid("9d935e26-2353-447e-97ba-5c3eccab9059") },
                    { 4, "Broad Cast", (byte)111, "BS", new Guid("9d935e26-2353-447e-97ba-5c3eccab9059") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_actualizacion_server", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 2, "Lestoma-APP", "SqBCPAc/Va0ra6oJGBfhYVaZFSyixtuF72y7mSXxbuY=", null, "diegop177@hotmail.com", 2, null, new DateTime(2023, 2, 27, 20, 51, 55, 16, DateTimeKind.Local).AddTicks(9068), null, "N/A", "Diego-Super", 1, "omvm2+Y4TJcdRZcKVDyUQA==", "Anonimo", "Local" },
                    { 1, "Movil", "SqBCPAc/Va0ra6oJGBfhYVaZFSyixtuF72y7mSXxbuY=", null, "lestomaudecmovil@gmail.com", 2, null, new DateTime(2023, 2, 27, 20, 51, 55, 16, DateTimeKind.Local).AddTicks(5506), null, "N/A", "Lestoma-APP", 1, "omvm2+Y4TJcdRZcKVDyUQA==", "Anonimo", "Local" },
                    { 3, "Lestoma", "25AA4SF9hb7eCuxBNmWHE0galR+GwmprI41LT5GjkGo=", null, "diegoarturo1598@hotmail.com", 2, null, new DateTime(2023, 2, 27, 20, 51, 55, 24, DateTimeKind.Local).AddTicks(7658), null, "N/A", "Administrador", 2, "zt9vX3AnKFO9ZMYM5OSPZA==", "Anonimo", "Local" },
                    { 4, "Lestoma", "2lSzmKUBtbnWulyFDr45UumQ7Pg7X96PPlobrXjECFM=", null, "programadoresuc@outlook.com", 2, null, new DateTime(2023, 2, 27, 20, 51, 55, 32, DateTimeKind.Local).AddTicks(5692), null, "N/A", "Auxiliar 1", 3, "qWXTQ7izTdauqGVF/Oawxg==", "Anonimo", "Local" },
                    { 5, "Lestoma", "2lSzmKUBtbnWulyFDr45UumQ7Pg7X96PPlobrXjECFM=", null, "tudec2020@gmail.com", 2, null, new DateTime(2023, 2, 27, 20, 51, 55, 32, DateTimeKind.Local).AddTicks(5735), null, "N/A", "Auxiliar 2", 3, "qWXTQ7izTdauqGVF/Oawxg==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "trama_enviada", "trama_recibida", "dato_trama_enviada", "dato_trama_recibida" },
                values: new object[,]
                {
                    { new Guid("352d30b6-44c2-4da7-aace-8156dcff8589"), new Guid("e95f08ab-6b4e-4d1f-aea7-85f0dc6848bf"), true, new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(7767), new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(3764), "N/A", "Anonimo", "Local", "6FDAF029000000009834", "49803CE33F8000008FC8", null, 1.0 },
                    { new Guid("13324fde-11cf-4756-9a30-88afe097d678"), new Guid("853929b3-bee1-4601-9cf0-8704d43fb771"), true, new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(9054), new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(9040), "N/A", "Anonimo", "Local", "495DF08E000000007B74", "496D3C083F80000096D1", null, 1.0 },
                    { new Guid("f67d3f0c-3957-4a8a-9502-8f5ca567b86b"), new Guid("556a8400-6ca7-4986-b1a8-b5d9982e1ffd"), true, new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(9065), new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(9063), "N/A", "Anonimo", "Local", "493E0FA6000000007453", "6FB2F0DC410E66663E8F", null, 8.9000000000000004 },
                    { new Guid("d99db055-ef37-4e23-a63c-e910903ce182"), new Guid("556a8400-6ca7-4986-b1a8-b5d9982e1ffd"), true, new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(9072), new DateTime(2023, 2, 27, 20, 51, 55, 171, DateTimeKind.Local).AddTicks(9070), "N/A", "Anonimo", "Local", "493E0FA6000000007453", "6FEFF08440D66666F1A3", null, 6.7000000000000002 },
                    { new Guid("d4215f60-0b93-4a16-b951-08f75a43e03b"), new Guid("6b693418-b841-4284-9315-6659b5d255e9"), true, new DateTime(2023, 2, 27, 20, 51, 55, 172, DateTimeKind.Local).AddTicks(817), new DateTime(2023, 2, 27, 20, 51, 55, 172, DateTimeKind.Local).AddTicks(797), "N/A", "Anonimo", "Local", "49F2F04541C00000A19A", "6FEEF0D8434800001CA9", 24.0, 200.0 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_actualizacion_server", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720"), 3, null, new DateTime(2023, 2, 27, 20, 51, 55, 38, DateTimeKind.Local).AddTicks(2873), "N/A", "Anonimo", "Local" },
                    { new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720"), 3, null, new DateTime(2023, 2, 27, 20, 51, 55, 38, DateTimeKind.Local).AddTicks(2889), "N/A", "Anonimo", "Local" },
                    { new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720"), 4, null, new DateTime(2023, 2, 27, 20, 51, 55, 38, DateTimeKind.Local).AddTicks(2892), "N/A", "Anonimo", "Local" },
                    { new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), new Guid("d9e6e4cf-043f-4b0c-b960-6489f71e7720"), 4, null, new DateTime(2023, 2, 27, 20, 51, 55, 38, DateTimeKind.Local).AddTicks(2895), "N/A", "Anonimo", "Local" },
                    { new Guid("5679927b-6e94-445a-9c9f-74c08178c262"), new Guid("9d935e26-2353-447e-97ba-5c3eccab9059"), 5, null, new DateTime(2023, 2, 27, 20, 51, 55, 38, DateTimeKind.Local).AddTicks(2898), "N/A", "Anonimo", "Local" },
                    { new Guid("f93a4811-fdb2-4234-959c-4c4ebb627ccb"), new Guid("9d935e26-2353-447e-97ba-5c3eccab9059"), 5, null, new DateTime(2023, 2, 27, 20, 51, 55, 38, DateTimeKind.Local).AddTicks(2900), "N/A", "Anonimo", "Local" }
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
                name: "IX_protocolo_com_upa_id",
                schema: "laboratorio_lestoma",
                table: "protocolo_com",
                column: "upa_id");

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
                name: "aplicacion",
                schema: "seguridad");

            migrationBuilder.DropTable(
                name: "auditoria",
                schema: "seguridad");

            migrationBuilder.DropTable(
                name: "buzon",
                schema: "reportes");

            migrationBuilder.DropTable(
                name: "detalle_laboratorio",
                schema: "laboratorio_lestoma");

            migrationBuilder.DropTable(
                name: "protocolo_com",
                schema: "laboratorio_lestoma");

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
