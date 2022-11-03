using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class InitialData : Migration
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
                    { new Guid("d9633ddd-1804-48f1-a74d-3f3d6aefe40e"), new DateTime(2022, 11, 2, 19, 50, 19, 978, DateTimeKind.Local).AddTicks(545), "192.168.1.15", "SENSORES", "Anonimo", "Local" },
                    { new Guid("7e675b69-cfe9-456d-8216-e9852466170a"), new DateTime(2022, 11, 2, 19, 50, 19, 978, DateTimeKind.Local).AddTicks(556), "192.168.1.15", "SET_POINT/CONTROL", "Anonimo", "Local" },
                    { new Guid("15c562d3-d21b-40e6-a47f-728e7ad07996"), new DateTime(2022, 11, 2, 19, 50, 19, 977, DateTimeKind.Local).AddTicks(8973), "192.168.1.15", "ACTUADORES", "Anonimo", "Local" }
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
                    { new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new DateTime(2022, 11, 2, 19, 50, 20, 24, DateTimeKind.Local).AddTicks(6358), "192.168.1.15", "control de agua", "Anonimo", "Local" },
                    { new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new DateTime(2022, 11, 2, 19, 50, 20, 24, DateTimeKind.Local).AddTicks(6378), "192.168.1.15", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 11, 2, 19, 50, 20, 24, DateTimeKind.Local).AddTicks(4395), "192.168.1.15", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("dbe94b8c-a8e7-4646-aa08-642a420e5bd4"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 11, 2, 19, 50, 20, 24, DateTimeKind.Local).AddTicks(4419), "192.168.1.15", "ucundinamarca", "Anonimo", 1, "Local" }
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
                    { new Guid("d858035b-cf16-4e90-8683-f4054be881a0"), new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9314), "192.168.1.15", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("15c562d3-d21b-40e6-a47f-728e7ad07996"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") },
                    { new Guid("2afa5d36-b849-4305-b496-4a434e955ad6"), new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9356), "192.168.1.15", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("15c562d3-d21b-40e6-a47f-728e7ad07996"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") },
                    { new Guid("707de268-5307-4017-97fa-12a760e44433"), new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9363), "192.168.1.15", "{\"Id\":\"98a74bd5-7390-4244-8b2e-255d3707071d\",\"TipoEstado\":\"ON-OFF\",\"ByteFuncion\":\"F0\"}", new Guid("15c562d3-d21b-40e6-a47f-728e7ad07996"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") },
                    { new Guid("846388be-1468-4fc5-b682-81897191eb72"), new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9366), "192.168.1.15", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("d9633ddd-1804-48f1-a74d-3f3d6aefe40e"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") },
                    { new Guid("8bafe268-55c2-4474-96aa-7547683b129a"), new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9369), "192.168.1.15", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("d9633ddd-1804-48f1-a74d-3f3d6aefe40e"), "PH", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") },
                    { new Guid("332b239f-e232-4b96-aad3-ef48ae70d111"), new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9372), "192.168.1.15", "{\"Id\":\"f5f738c7-0dba-48ee-afea-b22530160653\",\"TipoEstado\":\"LECTURA\",\"ByteFuncion\":\"0F\"}", new Guid("d9633ddd-1804-48f1-a74d-3f3d6aefe40e"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") },
                    { new Guid("557282b0-7848-4a2b-944a-66a112432fa8"), new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new DateTime(2022, 11, 2, 19, 50, 20, 171, DateTimeKind.Local).AddTicks(9396), "192.168.1.15", "{\"Id\":\"c781773b-7d7c-47f7-b5d0-34a4943ba907\",\"TipoEstado\":\"AJUSTE\",\"ByteFuncion\":\"F0\"}", new Guid("7e675b69-cfe9-456d-8216-e9852466170a"), "SP_TEMPERATURA H2O", "Anonimo", "Local", new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "3rVGoKyuGEG4Huy/b1ejkeDlWB0hqduF1If1HGXEbFE=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 11, 2, 19, 50, 20, 9, DateTimeKind.Local).AddTicks(3406), null, "192.168.1.15", "Super Admin", 1, "vP0+jz9BB8UdZC2vhETwJQ==", "Anonimo", "Local" },
                    { 2, "Lestoma", "3Vk4QTCzW2SK63My06UiPnOZj7tZCewwheJEsF2zac4=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 11, 2, 19, 50, 20, 16, DateTimeKind.Local).AddTicks(5910), null, "192.168.1.15", "Administrador", 2, "vTq3hNkpduCAeyO+TYCjig==", "Anonimo", "Local" },
                    { 3, "Lestoma", "fQApGgw+PwMLiG9DIcJlNb7iMxbXb85BOVTNKjKVuXE=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 11, 2, 19, 50, 20, 23, DateTimeKind.Local).AddTicks(6902), null, "192.168.1.15", "Auxiliar 1", 3, "lx5YfT0Y2PRwNCF2a7GyPg==", "Anonimo", "Local" },
                    { 4, "Lestoma", "fQApGgw+PwMLiG9DIcJlNb7iMxbXb85BOVTNKjKVuXE=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 11, 2, 19, 50, 20, 23, DateTimeKind.Local).AddTicks(6920), null, "192.168.1.15", "Auxiliar 2", 3, "lx5YfT0Y2PRwNCF2a7GyPg==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada", "trama_recibida", "dato_trama_enviada", "dato_trama_recibida" },
                values: new object[,]
                {
                    { new Guid("ae8be37f-2c5c-4bd5-9dfd-63bc217ddd6b"), new Guid("d858035b-cf16-4e90-8683-f4054be881a0"), true, new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(3690), new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(2833), "192.168.1.15", "Anonimo", "Local", 1, "6FDAF029000000009834", "49803CE33F8000008FC8", null, 1.0 },
                    { new Guid("dabf3513-1650-41fc-89f7-960566d26587"), new Guid("2afa5d36-b849-4305-b496-4a434e955ad6"), true, new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5405), new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5397), "192.168.1.15", "Anonimo", "Local", 2, "495DF08E000000007B74", "496D3C083F80000096D1", null, 1.0 },
                    { new Guid("0aa5ff65-7279-44bb-8b82-a9778ee3170f"), new Guid("8bafe268-55c2-4474-96aa-7547683b129a"), true, new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5411), new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5410), "192.168.1.15", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FB2F0DC410E66663E8F", null, 8.8999996185302734 },
                    { new Guid("a8fc134a-8521-4fe4-8887-7407ac199928"), new Guid("8bafe268-55c2-4474-96aa-7547683b129a"), true, new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5416), new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5415), "192.168.1.15", "Anonimo", "Local", 1, "493E0FA6000000007453", "6FEFF08440D66666F1A3", null, 6.6999998092651367 },
                    { new Guid("a737944d-1943-48e3-8e9a-2a3647912a58"), new Guid("557282b0-7848-4a2b-944a-66a112432fa8"), true, new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5843), new DateTime(2022, 11, 2, 19, 50, 20, 172, DateTimeKind.Local).AddTicks(5835), "192.168.1.15", "Anonimo", "Local", 1, "49F2F04541C00000A19A", "6FEEF0D8434800001CA9", 24.0, 200.0 }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e"), 2, new DateTime(2022, 11, 2, 19, 50, 20, 25, DateTimeKind.Local).AddTicks(140), "192.168.1.15", "Anonimo", "Local" },
                    { new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e"), 2, new DateTime(2022, 11, 2, 19, 50, 20, 25, DateTimeKind.Local).AddTicks(154), "192.168.1.15", "Anonimo", "Local" },
                    { new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e"), 3, new DateTime(2022, 11, 2, 19, 50, 20, 25, DateTimeKind.Local).AddTicks(157), "192.168.1.15", "Anonimo", "Local" },
                    { new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new Guid("f545f65d-c1e8-42e6-87a3-ffb8dc1d556e"), 3, new DateTime(2022, 11, 2, 19, 50, 20, 25, DateTimeKind.Local).AddTicks(160), "192.168.1.15", "Anonimo", "Local" },
                    { new Guid("6a34de57-da72-4747-bc2b-89c77ae30407"), new Guid("dbe94b8c-a8e7-4646-aa08-642a420e5bd4"), 4, new DateTime(2022, 11, 2, 19, 50, 20, 25, DateTimeKind.Local).AddTicks(162), "192.168.1.15", "Anonimo", "Local" },
                    { new Guid("cc710f98-7619-4f29-9419-2004c501104f"), new Guid("dbe94b8c-a8e7-4646-aa08-642a420e5bd4"), 4, new DateTime(2022, 11, 2, 19, 50, 20, 25, DateTimeKind.Local).AddTicks(164), "192.168.1.15", "Anonimo", "Local" }
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
