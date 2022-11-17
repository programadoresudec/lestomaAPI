using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class Initial : Migration
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
                    { new Guid("93165803-b20a-4a8a-bb3c-928525fb1801"), new DateTime(2022, 11, 16, 19, 23, 1, 814, DateTimeKind.Local).AddTicks(1133), "172.18.0.1", "SENSORES", "Anonimo", "Local" },
                    { new Guid("f8dfd283-9187-4e0e-8019-630d90acb895"), new DateTime(2022, 11, 16, 19, 23, 1, 814, DateTimeKind.Local).AddTicks(1145), "172.18.0.1", "SET_POINT/CONTROL", "Anonimo", "Local" },
                    { new Guid("f69b24c8-ea88-4803-bd09-9c4c95f2daa2"), new DateTime(2022, 11, 16, 19, 23, 1, 813, DateTimeKind.Local).AddTicks(9568), "172.18.0.1", "ACTUADORES", "Anonimo", "Local" }
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
                    { new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(595), "172.18.0.1", "control de agua", "Anonimo", "Local" },
                    { new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(615), "172.18.0.1", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 11, 16, 19, 23, 1, 857, DateTimeKind.Local).AddTicks(8216), "172.18.0.1", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("7a8154b2-f30f-4dd8-8c35-a75443aa1b67"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 11, 16, 19, 23, 1, 857, DateTimeKind.Local).AddTicks(8259), "172.18.0.1", "ucundinamarca", "Anonimo", 1, "Local" }
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
                    { new Guid("9f389376-ca20-4978-a2da-477a14406a93"), new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5311), "172.18.0.1", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("f69b24c8-ea88-4803-bd09-9c4c95f2daa2"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") },
                    { new Guid("bf359693-0cbe-4bda-9521-46248941ba4b"), new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5362), "172.18.0.1", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("f69b24c8-ea88-4803-bd09-9c4c95f2daa2"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") },
                    { new Guid("bb242179-8da2-4761-a799-7e8bb6ecb595"), new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5367), "172.18.0.1", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("f69b24c8-ea88-4803-bd09-9c4c95f2daa2"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") },
                    { new Guid("778f4903-9473-4153-8c52-68f4a018628d"), new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5372), "172.18.0.1", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("93165803-b20a-4a8a-bb3c-928525fb1801"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") },
                    { new Guid("765e827d-cfe2-48f5-914c-b06c0fa497fd"), new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5375), "172.18.0.1", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("93165803-b20a-4a8a-bb3c-928525fb1801"), "PH", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") },
                    { new Guid("0c01d1a8-48d7-437c-9795-47c1696c1c8d"), new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5378), "172.18.0.1", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("93165803-b20a-4a8a-bb3c-928525fb1801"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") },
                    { new Guid("b5c895af-090c-4ba5-b61d-e34c4d570191"), new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(5381), "172.18.0.1", "{\"Id\":\"c781773b-7d7c-47f7-b5d0-34a4943ba907\",\"TipoEstado\":\"AJUSTE\",\"ByteFuncion\":\"F0\"}", new Guid("f8dfd283-9187-4e0e-8019-630d90acb895"), "SP_TEMPERATURA H2O", "Anonimo", "Local", new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "ZO7v1+ITfCsJsaxvBXRaVgYBrzYywVtLzvSJwBWMJVw=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 11, 16, 19, 23, 1, 842, DateTimeKind.Local).AddTicks(1230), null, "172.18.0.1", "Super Admin", 1, "YeS4/xxFu7Lq2LhsH0N5uA==", "Anonimo", "Local" },
                    { 2, "Lestoma", "sQ2G39VfFggaZmvbkTzngR2sZ0yJqe1/dcTgF/Q0PeY=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 11, 16, 19, 23, 1, 849, DateTimeKind.Local).AddTicks(9498), null, "172.18.0.1", "Administrador", 2, "A/m02tYox4EIXXx/E9n5xw==", "Anonimo", "Local" },
                    { 3, "Lestoma", "mqWWrTh7k9JcOluESRWDSvD3A+e9Vu+00iZTbyFEW6I=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 11, 16, 19, 23, 1, 857, DateTimeKind.Local).AddTicks(637), null, "172.18.0.1", "Auxiliar 1", 3, "bHZGYmlMD6KTKah4kS+nnA==", "Anonimo", "Local" },
                    { 4, "Lestoma", "mqWWrTh7k9JcOluESRWDSvD3A+e9Vu+00iZTbyFEW6I=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 11, 16, 19, 23, 1, 857, DateTimeKind.Local).AddTicks(651), null, "172.18.0.1", "Auxiliar 2", 3, "bHZGYmlMD6KTKah4kS+nnA==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada", "trama_recibida", "dato_trama_enviada", "dato_trama_recibida" },
                values: new object[,]
                {
                    { new Guid("f66fb0b6-9617-4e10-a345-77d5dade5e3c"), new Guid("9f389376-ca20-4978-a2da-477a14406a93"), true, new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(571), new DateTime(2022, 11, 16, 19, 23, 1, 977, DateTimeKind.Local).AddTicks(9662), "172.18.0.1", "Anonimo", "Local", 1, "6FDAF029000000009834", "49803CE33F8000008FC8", null, 1.0 },
                    { new Guid("4f1bf122-eedc-4d29-9727-ed3e845a7067"), new Guid("bf359693-0cbe-4bda-9521-46248941ba4b"), true, new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2072), new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2063), "172.18.0.1", "Anonimo", "Local", 2, "495DF08E000000007B74", "496D3C083F80000096D1", null, 1.0 },
                    { new Guid("046687e1-38bc-4d5b-bae6-bc7eb44dd327"), new Guid("765e827d-cfe2-48f5-914c-b06c0fa497fd"), true, new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2079), new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2077), "172.18.0.1", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FB2F0DC410E66663E8F", null, 8.9000000000000004 },
                    { new Guid("64fa90bb-d98c-44cd-9150-996fdddc742c"), new Guid("765e827d-cfe2-48f5-914c-b06c0fa497fd"), true, new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2083), new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2082), "172.18.0.1", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FEFF08440D66666F1A3", null, 6.7000000000000002 },
                    { new Guid("cf5df11f-0cc8-4fc7-a54f-9e35e51bde20"), new Guid("b5c895af-090c-4ba5-b61d-e34c4d570191"), true, new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2524), new DateTime(2022, 11, 16, 19, 23, 1, 978, DateTimeKind.Local).AddTicks(2514), "172.18.0.1", "Anonimo", "Local", 1, "49F2F04541C00000A19A", "6FEEF0D8434800001CA9", 24.0, 200.0 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f"), 2, new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(4449), "172.18.0.1", "Anonimo", "Local" },
                    { new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f"), 2, new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(4466), "172.18.0.1", "Anonimo", "Local" },
                    { new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f"), 3, new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(4652), "172.18.0.1", "Anonimo", "Local" },
                    { new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new Guid("dd6c683d-cc2e-434b-99ad-6f19bec9516f"), 3, new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(4656), "172.18.0.1", "Anonimo", "Local" },
                    { new Guid("94c28895-b38b-41ae-bc68-75842154df28"), new Guid("7a8154b2-f30f-4dd8-8c35-a75443aa1b67"), 4, new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(4659), "172.18.0.1", "Anonimo", "Local" },
                    { new Guid("79e309de-1945-4873-9352-1ece9c165bbc"), new Guid("7a8154b2-f30f-4dd8-8c35-a75443aa1b67"), 4, new DateTime(2022, 11, 16, 19, 23, 1, 858, DateTimeKind.Local).AddTicks(4661), "172.18.0.1", "Anonimo", "Local" }
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
