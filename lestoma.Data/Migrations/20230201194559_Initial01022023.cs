using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class Initial01022023 : Migration
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
                    nombre_actividad = table.Column<string>(type: "text", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
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
                    detalle = table.Column<string>(type: "jsonb", nullable: false),
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
                name: "control_de_agua",
                schema: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "jsonb", nullable: false),
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
                    detalle = table.Column<string>(type: "jsonb", nullable: false),
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
                    detalle = table.Column<string>(type: "jsonb", nullable: false),
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
                    detalle = table.Column<string>(type: "jsonb", nullable: false),
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
                    nombre = table.Column<string>(type: "text", nullable: false),
                    primer_byte_trama = table.Column<string>(type: "text", nullable: false),
                    sigla = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
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
                    detalle = table.Column<string>(type: "jsonb", nullable: false),
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
                    nombre_upa = table.Column<string>(type: "text", nullable: false),
                    superadmin_id = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    cantidad_actividades = table.Column<short>(type: "smallint", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
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
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
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
                    tipo_com_id = table.Column<int>(type: "integer", nullable: false),
                    dato_trama_enviada = table.Column<double>(type: "double precision", nullable: true),
                    dato_trama_recibida = table.Column<double>(type: "double precision", nullable: true),
                    trama_enviada = table.Column<string>(type: "text", nullable: true),
                    trama_recibida = table.Column<string>(type: "text", nullable: true),
                    estado_internet = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_creacion_dispositivo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    session = table.Column<string>(type: "text", nullable: false),
                    tipo_de_aplicacion = table.Column<string>(type: "text", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalle_laboratorio_protocolo_com_tipo_com_id",
                        column: x => x.tipo_com_id,
                        principalSchema: "laboratorio_lestoma",
                        principalTable: "protocolo_com",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "modulo_componente",
                columns: new[] { "id", "fecha_creacion_server", "ip", "nombre_modulo", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("c4425a21-86da-4cef-9623-28882135c403"), new DateTime(2023, 2, 1, 14, 45, 58, 599, DateTimeKind.Local).AddTicks(4894), "172.30.96.1", "SENSORES", "Anonimo", "Local" },
                    { new Guid("48cc18e5-59cc-4c95-91fc-853ec233b225"), new DateTime(2023, 2, 1, 14, 45, 58, 599, DateTimeKind.Local).AddTicks(4906), "172.30.96.1", "SET_POINT/CONTROL", "Anonimo", "Local" },
                    { new Guid("c1a18a89-4f8f-4d50-9279-93135483e329"), new DateTime(2023, 2, 1, 14, 45, 58, 599, DateTimeKind.Local).AddTicks(3488), "172.30.96.1", "ACTUADORES", "Anonimo", "Local" }
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
                    { new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(5169), "172.30.96.1", "control de agua", "Anonimo", "Local" },
                    { new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(5193), "172.30.96.1", "alimentacion de peces", "Anonimo", "Local" }
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
                columns: new[] { "id", "cantidad_actividades", "descripcion", "fecha_creacion_server", "ip", "nombre_upa", "session", "superadmin_id", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("91f86319-dea2-46d8-8c3d-2065b9297962"), (short)5, "queda ubicada en facatativá", new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(3151), "172.30.96.1", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("2ace840c-862b-4b32-b583-622587a8e06f"), (short)2, "queda ubicada en la universidad cundinamarca extensión faca", new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(3189), "172.30.96.1", "ucundinamarca", "Anonimo", 1, "Local" }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "estado_usuario",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { 3, "Inactivo" },
                    { 2, "Activado" },
                    { 1, "verificar cuenta" },
                    { 4, "Bloqueado" }
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
                    { new Guid("ad50972d-6421-4853-ac04-2e33c7f01540"), new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8556), "172.30.96.1", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("c1a18a89-4f8f-4d50-9279-93135483e329"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") },
                    { new Guid("9564116f-7904-4548-b99d-1e129c97441c"), new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8613), "172.30.96.1", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("c1a18a89-4f8f-4d50-9279-93135483e329"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") },
                    { new Guid("4b78e7fb-fa05-4de4-837b-0b6b1fd689f8"), new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8623), "172.30.96.1", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("c1a18a89-4f8f-4d50-9279-93135483e329"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") },
                    { new Guid("a17c1b85-ad09-4208-9954-eb5b4b08ea13"), new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8628), "172.30.96.1", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteHexaFuncion\":\"0F\",\"ByteDecimalFuncion\":15}", new Guid("c4425a21-86da-4cef-9623-28882135c403"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") },
                    { new Guid("dd162b75-cfac-47bf-8865-2eafabecc6a0"), new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8632), "172.30.96.1", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteHexaFuncion\":\"0F\",\"ByteDecimalFuncion\":15}", new Guid("c4425a21-86da-4cef-9623-28882135c403"), "PH", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") },
                    { new Guid("7e0fe3e1-4e48-4aae-a833-4c8a1987bb12"), new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8635), "172.30.96.1", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteHexaFuncion\":\"0F\",\"ByteDecimalFuncion\":15}", new Guid("c4425a21-86da-4cef-9623-28882135c403"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") },
                    { new Guid("cbaad725-cacc-4ab1-b54e-91cb9414b953"), new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new DateTime(2023, 2, 1, 14, 45, 58, 853, DateTimeKind.Local).AddTicks(8639), "172.30.96.1", "{\"Id\":\"c781773b-7d7c-47f7-b5d0-34a4943ba907\",\"TipoEstado\":\"AJUSTE\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("48cc18e5-59cc-4c95-91fc-853ec233b225"), "SP_TEMPERATURA H2O", "Anonimo", "Local", new Guid("91f86319-dea2-46d8-8c3d-2065b9297962") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 2, "Lestoma-APP", "vXbhVJqYXTKJhsUfgepmlREF3WZXVNRFSFVkDrIxQaI=", null, "diegop177@hotmail.com", 2, new DateTime(2023, 2, 1, 14, 45, 58, 644, DateTimeKind.Local).AddTicks(1054), null, "172.30.96.1", "Diego-Super", 1, "9NFnwgZRTJr61A3ymVq4sA==", "Anonimo", "Local" },
                    { 1, "Movil", "vXbhVJqYXTKJhsUfgepmlREF3WZXVNRFSFVkDrIxQaI=", null, "lestomaudecmovil@gmail.com", 2, new DateTime(2023, 2, 1, 14, 45, 58, 643, DateTimeKind.Local).AddTicks(7621), null, "172.30.96.1", "Lestoma-APP", 1, "9NFnwgZRTJr61A3ymVq4sA==", "Anonimo", "Local" },
                    { 3, "Lestoma", "A5HzwpwJIuYyDvaDL1RFes/ORX7PUmnU+LSGK8hChXc=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2023, 2, 1, 14, 45, 58, 651, DateTimeKind.Local).AddTicks(1435), null, "172.30.96.1", "Administrador", 2, "3CM+4ymQ6qe0KqrKndcRBw==", "Anonimo", "Local" },
                    { 4, "Lestoma", "Z2Ykx/Ilz7kLhisdZNSl3YVQIJLM2fP6QII/WrXPKhU=", null, "programadoresuc@outlook.com", 2, new DateTime(2023, 2, 1, 14, 45, 58, 658, DateTimeKind.Local).AddTicks(4479), null, "172.30.96.1", "Auxiliar 1", 3, "nl4cgYNV4yg9BRw070b4vw==", "Anonimo", "Local" },
                    { 5, "Lestoma", "Z2Ykx/Ilz7kLhisdZNSl3YVQIJLM2fP6QII/WrXPKhU=", null, "tudec2020@gmail.com", 2, new DateTime(2023, 2, 1, 14, 45, 58, 658, DateTimeKind.Local).AddTicks(4511), null, "172.30.96.1", "Auxiliar 2", 3, "nl4cgYNV4yg9BRw070b4vw==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada", "trama_recibida", "dato_trama_enviada", "dato_trama_recibida" },
                values: new object[,]
                {
                    { new Guid("c5738d6d-8dde-4b48-a0f0-2b2c2ffc54b9"), new Guid("ad50972d-6421-4853-ac04-2e33c7f01540"), true, new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(3115), new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(2255), "172.30.96.1", "Anonimo", "Local", 1, "6FDAF029000000009834", "49803CE33F8000008FC8", null, 1.0 },
                    { new Guid("d558b2e6-0187-4fa9-99eb-cff4e6b87c2c"), new Guid("9564116f-7904-4548-b99d-1e129c97441c"), true, new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(4561), new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(4552), "172.30.96.1", "Anonimo", "Local", 2, "495DF08E000000007B74", "496D3C083F80000096D1", null, 1.0 },
                    { new Guid("58cc3185-1867-41c3-99d8-d21f06a54b05"), new Guid("dd162b75-cfac-47bf-8865-2eafabecc6a0"), true, new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(4568), new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(4567), "172.30.96.1", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FB2F0DC410E66663E8F", null, 8.9000000000000004 },
                    { new Guid("0a87b909-c249-48c2-9541-f341114507bf"), new Guid("dd162b75-cfac-47bf-8865-2eafabecc6a0"), true, new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(4574), new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(4573), "172.30.96.1", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FEFF08440D66666F1A3", null, 6.7000000000000002 },
                    { new Guid("e4c74b36-b696-4164-91dd-40c50efe9001"), new Guid("cbaad725-cacc-4ab1-b54e-91cb9414b953"), true, new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(5011), new DateTime(2023, 2, 1, 14, 45, 58, 854, DateTimeKind.Local).AddTicks(5003), "172.30.96.1", "Anonimo", "Local", 1, "49F2F04541C00000A19A", "6FEEF0D8434800001CA9", 24.0, 200.0 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new Guid("91f86319-dea2-46d8-8c3d-2065b9297962"), 3, new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(8260), "172.30.96.1", "Anonimo", "Local" },
                    { new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new Guid("91f86319-dea2-46d8-8c3d-2065b9297962"), 3, new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(8278), "172.30.96.1", "Anonimo", "Local" },
                    { new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new Guid("91f86319-dea2-46d8-8c3d-2065b9297962"), 4, new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(8282), "172.30.96.1", "Anonimo", "Local" },
                    { new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new Guid("91f86319-dea2-46d8-8c3d-2065b9297962"), 4, new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(8284), "172.30.96.1", "Anonimo", "Local" },
                    { new Guid("c071a713-b2d3-4dcf-be15-e3ca1a4d436b"), new Guid("2ace840c-862b-4b32-b583-622587a8e06f"), 5, new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(8287), "172.30.96.1", "Anonimo", "Local" },
                    { new Guid("4cc1a161-776c-48f5-8081-05343c247469"), new Guid("2ace840c-862b-4b32-b583-622587a8e06f"), 5, new DateTime(2023, 2, 1, 14, 45, 58, 659, DateTimeKind.Local).AddTicks(8289), "172.30.96.1", "Anonimo", "Local" }
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
