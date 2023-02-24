using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class NewData : Migration
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
                    direccion_registro = table.Column<byte>(type: "smallint", nullable: false),
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
                    dato_trama_enviada = table.Column<double>(type: "double precision", nullable: true),
                    dato_trama_recibida = table.Column<double>(type: "double precision", nullable: true),
                    trama_enviada = table.Column<string>(type: "text", nullable: true),
                    trama_recibida = table.Column<string>(type: "text", nullable: true),
                    estado_internet = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_creacion_dispositivo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TipoDeComunicacionId = table.Column<int>(type: "integer", nullable: true),
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
                        name: "FK_detalle_laboratorio_protocolo_com_TipoDeComunicacionId",
                        column: x => x.TipoDeComunicacionId,
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
                    { new Guid("f7af0026-c029-4b24-b465-8e9f889ac9b8"), new DateTime(2023, 2, 23, 20, 39, 30, 512, DateTimeKind.Local).AddTicks(3162), "N/A", "ACTUADORES", "Anonimo", "Local" },
                    { new Guid("d67960bc-152c-4a9d-adac-7dd0ec51b9dc"), new DateTime(2023, 2, 23, 20, 39, 30, 512, DateTimeKind.Local).AddTicks(5850), "N/A", "SET_POINT/CONTROL", "Anonimo", "Local" },
                    { new Guid("a0891542-c300-486e-b6e8-7f544804ba1b"), new DateTime(2023, 2, 23, 20, 39, 30, 512, DateTimeKind.Local).AddTicks(5830), "N/A", "SENSORES", "Anonimo", "Local" }
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
                    { new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(513), "N/A", "control de agua", "Anonimo", "Local" },
                    { new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(567), "N/A", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82"), (short)5, "queda ubicada en facatativá", new DateTime(2023, 2, 23, 20, 39, 30, 559, DateTimeKind.Local).AddTicks(1840), "N/A", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("7f73daea-1775-4a22-a58e-d050482f9dd7"), (short)2, "queda ubicada en la universidad cundinamarca extensión faca", new DateTime(2023, 2, 23, 20, 39, 30, 559, DateTimeKind.Local).AddTicks(1919), "N/A", "ucundinamarca", "Anonimo", 1, "Local" }
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
                columns: new[] { "id", "actividad_id", "direccion_registro", "fecha_creacion_server", "ip", "descripcion_estado", "modulo_componente_id", "nombre_componente", "session", "tipo_de_aplicacion", "upa_id" },
                values: new object[,]
                {
                    { new Guid("003bbddd-22fa-468e-b0c8-b66ac1586cd4"), new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), (byte)0, new DateTime(2023, 2, 23, 20, 39, 30, 700, DateTimeKind.Local).AddTicks(1799), "N/A", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("f7af0026-c029-4b24-b465-8e9f889ac9b8"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { new Guid("b7f743f6-6bfd-4aba-bfdf-e64bc5575ee9"), new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), (byte)1, new DateTime(2023, 2, 23, 20, 39, 30, 700, DateTimeKind.Local).AddTicks(2238), "N/A", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("f7af0026-c029-4b24-b465-8e9f889ac9b8"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { new Guid("05bc358d-92ef-46a8-9588-918275f3a5ba"), new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), (byte)3, new DateTime(2023, 2, 23, 20, 39, 30, 701, DateTimeKind.Local).AddTicks(4121), "N/A", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("f7af0026-c029-4b24-b465-8e9f889ac9b8"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { new Guid("100b0b78-1454-4034-b595-fc3099029e15"), new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), (byte)2, new DateTime(2023, 2, 23, 20, 39, 30, 701, DateTimeKind.Local).AddTicks(4147), "N/A", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteHexaFuncion\":\"0F\",\"ByteDecimalFuncion\":15}", new Guid("a0891542-c300-486e-b6e8-7f544804ba1b"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { new Guid("2fb34f39-a4d5-446d-8dc8-f4fd71587ad8"), new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), (byte)0, new DateTime(2023, 2, 23, 20, 39, 30, 701, DateTimeKind.Local).AddTicks(4157), "N/A", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteHexaFuncion\":\"0F\",\"ByteDecimalFuncion\":15}", new Guid("a0891542-c300-486e-b6e8-7f544804ba1b"), "PH", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { new Guid("a1564c77-c91c-44f9-82b9-ea17530781c2"), new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), (byte)1, new DateTime(2023, 2, 23, 20, 39, 30, 701, DateTimeKind.Local).AddTicks(4196), "N/A", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteHexaFuncion\":\"0F\",\"ByteDecimalFuncion\":15}", new Guid("a0891542-c300-486e-b6e8-7f544804ba1b"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { new Guid("982114e1-3233-47a4-9587-f1c062078227"), new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), (byte)7, new DateTime(2023, 2, 23, 20, 39, 30, 701, DateTimeKind.Local).AddTicks(4205), "N/A", "{\"Id\":\"c781773b-7d7c-47f7-b5d0-34a4943ba907\",\"TipoEstado\":\"AJUSTE\",\"ByteHexaFuncion\":\"F0\",\"ByteDecimalFuncion\":240}", new Guid("d67960bc-152c-4a9d-adac-7dd0ec51b9dc"), "SP_TEMPERATURA H2O", "Anonimo", "Local", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "protocolo_com",
                columns: new[] { "id", "nombre", "primer_byte_trama", "sigla", "upa_id" },
                values: new object[,]
                {
                    { 1, "Peer to Peer", (byte)73, "PP", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { 2, "Broad Cast", (byte)111, "BS", new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82") },
                    { 3, "Peer to Peer", (byte)73, "PP", new Guid("7f73daea-1775-4a22-a58e-d050482f9dd7") },
                    { 4, "Broad Cast", (byte)111, "BS", new Guid("7f73daea-1775-4a22-a58e-d050482f9dd7") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 2, "Lestoma-APP", "/+bAZfRo19ISeopIZznRcYFZiKKdQge0bTCgO+nYPTs=", null, "diegop177@hotmail.com", 2, new DateTime(2023, 2, 23, 20, 39, 30, 541, DateTimeKind.Local).AddTicks(4109), null, "N/A", "Diego-Super", 1, "EHjW1Ad4fzFth3q8KDXD9A==", "Anonimo", "Local" },
                    { 1, "Movil", "/+bAZfRo19ISeopIZznRcYFZiKKdQge0bTCgO+nYPTs=", null, "lestomaudecmovil@gmail.com", 2, new DateTime(2023, 2, 23, 20, 39, 30, 540, DateTimeKind.Local).AddTicks(9758), null, "N/A", "Lestoma-APP", 1, "EHjW1Ad4fzFth3q8KDXD9A==", "Anonimo", "Local" },
                    { 3, "Lestoma", "dvGgJ9o5nMlPSToK69ygyqhLbqpOW2Gfz+YDla3HHJI=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2023, 2, 23, 20, 39, 30, 549, DateTimeKind.Local).AddTicks(4396), null, "N/A", "Administrador", 2, "/Xiw7gavBof1n369KxJEbw==", "Anonimo", "Local" },
                    { 4, "Lestoma", "wtZMpvYqfF9xBzkhjiiQ/An2PGtDwnQQzvqZK7Sg3p4=", null, "programadoresuc@outlook.com", 2, new DateTime(2023, 2, 23, 20, 39, 30, 557, DateTimeKind.Local).AddTicks(4040), null, "N/A", "Auxiliar 1", 3, "+TqRSWfwd/Garx9tKWcypw==", "Anonimo", "Local" },
                    { 5, "Lestoma", "wtZMpvYqfF9xBzkhjiiQ/An2PGtDwnQQzvqZK7Sg3p4=", null, "tudec2020@gmail.com", 2, new DateTime(2023, 2, 23, 20, 39, 30, 557, DateTimeKind.Local).AddTicks(4091), null, "N/A", "Auxiliar 2", 3, "+TqRSWfwd/Garx9tKWcypw==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "TipoDeComunicacionId", "trama_enviada", "trama_recibida", "dato_trama_enviada", "dato_trama_recibida" },
                values: new object[,]
                {
                    { new Guid("65bf6fc3-e7f4-4fe0-b523-a15c67e487dd"), new Guid("003bbddd-22fa-468e-b0c8-b66ac1586cd4"), true, new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(979), new DateTime(2023, 2, 23, 20, 39, 30, 701, DateTimeKind.Local).AddTicks(9853), "N/A", "Anonimo", "Local", null, "6FDAF029000000009834", "49803CE33F8000008FC8", null, 1.0 },
                    { new Guid("837cca96-ca85-4fa0-8491-79b0732e9bcd"), new Guid("b7f743f6-6bfd-4aba-bfdf-e64bc5575ee9"), true, new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(1849), new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(1839), "N/A", "Anonimo", "Local", null, "495DF08E000000007B74", "496D3C083F80000096D1", null, 1.0 },
                    { new Guid("3048fe79-ef3f-401a-b252-f74e7abaa9bd"), new Guid("2fb34f39-a4d5-446d-8dc8-f4fd71587ad8"), true, new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(1856), new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(1854), "N/A", "Anonimo", "Local", null, "493E0FA6000000007453", "6FB2F0DC410E66663E8F", null, 8.9000000000000004 },
                    { new Guid("0538cc1c-59a7-4f25-89ed-3b977d32b34a"), new Guid("2fb34f39-a4d5-446d-8dc8-f4fd71587ad8"), true, new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(1860), new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(1859), "N/A", "Anonimo", "Local", null, "493E0FA6000000007453", "6FEFF08440D66666F1A3", null, 6.7000000000000002 },
                    { new Guid("e61c3ed2-c32d-43dc-a329-ea5f50b10e0b"), new Guid("982114e1-3233-47a4-9587-f1c062078227"), true, new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(2330), new DateTime(2023, 2, 23, 20, 39, 30, 702, DateTimeKind.Local).AddTicks(2322), "N/A", "Anonimo", "Local", null, "49F2F04541C00000A19A", "6FEEF0D8434800001CA9", 24.0, 200.0 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82"), 3, new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(5705), "N/A", "Anonimo", "Local" },
                    { new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82"), 3, new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(5726), "N/A", "Anonimo", "Local" },
                    { new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82"), 4, new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(5732), "N/A", "Anonimo", "Local" },
                    { new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), new Guid("2cd48cd9-d3a3-4910-a430-474b42458b82"), 4, new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(5735), "N/A", "Anonimo", "Local" },
                    { new Guid("a457d4bd-3921-4a22-8d77-6d895de743c3"), new Guid("7f73daea-1775-4a22-a58e-d050482f9dd7"), 5, new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(5739), "N/A", "Anonimo", "Local" },
                    { new Guid("9d229436-a9c7-4849-a840-055af0c2b5a9"), new Guid("7f73daea-1775-4a22-a58e-d050482f9dd7"), 5, new DateTime(2023, 2, 23, 20, 39, 30, 565, DateTimeKind.Local).AddTicks(5743), "N/A", "Anonimo", "Local" }
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
                name: "IX_detalle_laboratorio_TipoDeComunicacionId",
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                column: "TipoDeComunicacionId");

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
