using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class DataInitial : Migration
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
                    { new Guid("b2ba13fa-d96f-495b-b1a3-f42eb7b32796"), new DateTime(2022, 11, 17, 16, 12, 4, 547, DateTimeKind.Local).AddTicks(4582), "10.201.240.243", "SENSORES", "Anonimo", "Local" },
                    { new Guid("33d6e193-2fbe-4118-882f-c002183d4aa2"), new DateTime(2022, 11, 17, 16, 12, 4, 547, DateTimeKind.Local).AddTicks(4593), "10.201.240.243", "SET_POINT/CONTROL", "Anonimo", "Local" },
                    { new Guid("d3e33310-61ce-4ac2-90c1-c4286631a0f7"), new DateTime(2022, 11, 17, 16, 12, 4, 547, DateTimeKind.Local).AddTicks(3042), "10.201.240.243", "ACTUADORES", "Anonimo", "Local" }
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
                    { new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(5225), "10.201.240.243", "control de agua", "Anonimo", "Local" },
                    { new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(5245), "10.201.240.243", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(3359), "10.201.240.243", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("e33757f8-ab9a-4143-82c5-ff3ed987e681"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(3382), "10.201.240.243", "ucundinamarca", "Anonimo", 1, "Local" }
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
                    { new Guid("c5b4d156-ffa1-4bc1-840e-12dcebf0c674"), new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(1968), "10.201.240.243", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("d3e33310-61ce-4ac2-90c1-c4286631a0f7"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") },
                    { new Guid("12eb8005-3487-451c-9edb-4bc90a946f32"), new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(2108), "10.201.240.243", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("d3e33310-61ce-4ac2-90c1-c4286631a0f7"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") },
                    { new Guid("38f5aa9e-e090-45fa-b910-4168ba1d1357"), new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(2115), "10.201.240.243", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("d3e33310-61ce-4ac2-90c1-c4286631a0f7"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") },
                    { new Guid("8f27b34e-a81f-4c10-b502-ebefc9a88e5c"), new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(2120), "10.201.240.243", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("b2ba13fa-d96f-495b-b1a3-f42eb7b32796"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") },
                    { new Guid("c3138870-41b0-4f5e-a791-5dcc710e935a"), new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(2123), "10.201.240.243", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("b2ba13fa-d96f-495b-b1a3-f42eb7b32796"), "PH", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") },
                    { new Guid("97101379-3f44-4eb0-913f-2c262a4fe745"), new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(2126), "10.201.240.243", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("b2ba13fa-d96f-495b-b1a3-f42eb7b32796"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") },
                    { new Guid("4144685c-7349-4bac-876a-d8d94e49ed69"), new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(2130), "10.201.240.243", "{\"Id\":\"c781773b-7d7c-47f7-b5d0-34a4943ba907\",\"TipoEstado\":\"AJUSTE\",\"ByteFuncion\":\"F0\"}", new Guid("33d6e193-2fbe-4118-882f-c002183d4aa2"), "SP_TEMPERATURA H2O", "Anonimo", "Local", new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "FMTdH4GAJiDffypi7wCq840EYtGeuw3/CT0EOZz7FHw=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 11, 17, 16, 12, 4, 575, DateTimeKind.Local).AddTicks(2979), null, "10.201.240.243", "Super Admin", 1, "Ka6/7yd7UYQ/dv+da2OL2Q==", "Anonimo", "Local" },
                    { 2, "Lestoma", "iZfYdY0T7CeB/M4kZ3VuoG4qc37iX16x/Cqm8lzNTAU=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 11, 17, 16, 12, 4, 582, DateTimeKind.Local).AddTicks(8656), null, "10.201.240.243", "Administrador", 2, "fpW+3+hEHJykTMx0T3KEOw==", "Anonimo", "Local" },
                    { 3, "Lestoma", "ljEhaEXbLX2VGgzr2PdHiCQmO0/iPzWYJGMfHcZSqmI=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 11, 17, 16, 12, 4, 589, DateTimeKind.Local).AddTicks(6737), null, "10.201.240.243", "Auxiliar 1", 3, "1giJOaKKcfoYyXBo0Fo5Gg==", "Anonimo", "Local" },
                    { 4, "Lestoma", "ljEhaEXbLX2VGgzr2PdHiCQmO0/iPzWYJGMfHcZSqmI=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 11, 17, 16, 12, 4, 589, DateTimeKind.Local).AddTicks(6746), null, "10.201.240.243", "Auxiliar 2", 3, "1giJOaKKcfoYyXBo0Fo5Gg==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada", "trama_recibida", "dato_trama_enviada", "dato_trama_recibida" },
                values: new object[,]
                {
                    { new Guid("8b3dd5a2-73e2-4004-9191-2959fe41435f"), new Guid("c5b4d156-ffa1-4bc1-840e-12dcebf0c674"), true, new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(7140), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(5949), "10.201.240.243", "Anonimo", "Local", 1, "6FDAF029000000009834", "49803CE33F8000008FC8", null, 1.0 },
                    { new Guid("3fa00e14-22ce-4cb0-bd70-a260c2f94c00"), new Guid("12eb8005-3487-451c-9edb-4bc90a946f32"), true, new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(8902), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(8892), "10.201.240.243", "Anonimo", "Local", 2, "495DF08E000000007B74", "496D3C083F80000096D1", null, 1.0 },
                    { new Guid("4ccf1912-580e-4e31-8173-6a71417f31eb"), new Guid("c3138870-41b0-4f5e-a791-5dcc710e935a"), true, new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(8910), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(8909), "10.201.240.243", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FB2F0DC410E66663E8F", null, 8.9000000000000004 },
                    { new Guid("42677cc4-d78a-4c3a-a049-d574f52c4da4"), new Guid("c3138870-41b0-4f5e-a791-5dcc710e935a"), true, new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(8915), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(8913), "10.201.240.243", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FEFF08440D66666F1A3", null, 6.7000000000000002 },
                    { new Guid("fe8b1d01-5e88-4974-86db-9e4475d64e61"), new Guid("4144685c-7349-4bac-876a-d8d94e49ed69"), true, new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(9358), new DateTime(2022, 11, 17, 16, 12, 4, 705, DateTimeKind.Local).AddTicks(9349), "10.201.240.243", "Anonimo", "Local", 1, "49F2F04541C00000A19A", "6FEEF0D8434800001CA9", 24.0, 200.0 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0"), 2, new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(8736), "10.201.240.243", "Anonimo", "Local" },
                    { new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0"), 2, new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(8869), "10.201.240.243", "Anonimo", "Local" },
                    { new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0"), 3, new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(8875), "10.201.240.243", "Anonimo", "Local" },
                    { new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new Guid("a0dc4b17-ff25-4fc5-b687-1b23cef12ce0"), 3, new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(8877), "10.201.240.243", "Anonimo", "Local" },
                    { new Guid("767f6153-1f32-4bff-bc91-78ee96666691"), new Guid("e33757f8-ab9a-4143-82c5-ff3ed987e681"), 4, new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(8880), "10.201.240.243", "Anonimo", "Local" },
                    { new Guid("06b110dc-51d7-474c-85c0-e0e03a237ef7"), new Guid("e33757f8-ab9a-4143-82c5-ff3ed987e681"), 4, new DateTime(2022, 11, 17, 16, 12, 4, 590, DateTimeKind.Local).AddTicks(8882), "10.201.240.243", "Anonimo", "Local" }
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
