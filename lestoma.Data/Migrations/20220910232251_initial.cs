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
                    dato_calculado_por_trama = table.Column<double>(type: "double precision", nullable: true),
                    trama_enviada = table.Column<string>(type: "text", nullable: true),
                    trama_recibida = table.Column<string>(type: "text", nullable: true),
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
                    { new Guid("8fb3844a-dca8-4b0f-92de-f2a4f405349f"), new DateTime(2022, 9, 10, 18, 22, 51, 56, DateTimeKind.Local).AddTicks(2324), "192.168.1.6", "ACTUADORES", "Anonimo", "Local" },
                    { new Guid("8cf6db19-2070-4439-bb01-a2d41fbd650a"), new DateTime(2022, 9, 10, 18, 22, 51, 58, DateTimeKind.Local).AddTicks(6244), "192.168.1.6", "SENSORES", "Anonimo", "Local" }
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
                    { new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(2660), "192.168.1.6", "control de agua", "Anonimo", "Local" },
                    { new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(2678), "192.168.1.6", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(754), "192.168.1.6", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("199ba499-09d6-4b1b-9c52-9199da02ef95"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(774), "192.168.1.6", "ucundinamarca", "Anonimo", 1, "Local" }
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
                    { new Guid("6b145453-c8c0-4f27-9610-ffd7cbf1b498"), new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(4567), "192.168.1.6", "{\"Id\":\"6fc218e0-324b-49e1-901b-1285ff02b9c7\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("8fb3844a-dca8-4b0f-92de-f2a4f405349f"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2") },
                    { new Guid("16a30b72-c2f5-4d34-8614-0cd88e89c496"), new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(4609), "192.168.1.6", "{\"Id\":\"6fc218e0-324b-49e1-901b-1285ff02b9c7\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("8fb3844a-dca8-4b0f-92de-f2a4f405349f"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2") },
                    { new Guid("c0f87a7e-15b9-493b-b04c-6748615b18a0"), new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(4616), "192.168.1.6", "{\"Id\":\"6fc218e0-324b-49e1-901b-1285ff02b9c7\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("8fb3844a-dca8-4b0f-92de-f2a4f405349f"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2") },
                    { new Guid("f872024e-593f-4b15-8bc3-056547756c11"), new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(4620), "192.168.1.6", "{\"Id\":\"2b5f33c2-0407-424f-8269-889e889da77e\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("8cf6db19-2070-4439-bb01-a2d41fbd650a"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2") },
                    { new Guid("fe890a80-a627-4bc7-b494-677d73998618"), new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(4623), "192.168.1.6", "{\"Id\":\"2b5f33c2-0407-424f-8269-889e889da77e\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("8cf6db19-2070-4439-bb01-a2d41fbd650a"), "PH", "Anonimo", "Local", new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2") },
                    { new Guid("c6062b46-963e-42c7-aa37-f50466bf5253"), new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(4626), "192.168.1.6", "{\"Id\":\"2b5f33c2-0407-424f-8269-889e889da77e\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("8cf6db19-2070-4439-bb01-a2d41fbd650a"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "SxQO9ASKiHL2fO/SSxjgoP93YD+iNe5CGLFsSJAkzCQ=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 9, 10, 18, 22, 51, 77, DateTimeKind.Local).AddTicks(2254), null, "192.168.1.6", "Super Admin", 1, "5jAA+DNnyzMj6C3+k3fMVA==", "Anonimo", "Local" },
                    { 2, "Lestoma", "ypX4VBkKZw8lpkvMMM4T39LpcgxO0HsNQvwcbQV0iJ0=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 9, 10, 18, 22, 51, 84, DateTimeKind.Local).AddTicks(8137), null, "192.168.1.6", "Administrador", 2, "5VJ6rPl8KSLV5aJGMDc6rA==", "Anonimo", "Local" },
                    { 3, "Lestoma", "MWn8hP/M68NXmjyqPdnZZrmjHN7Vk625LPTSCLYsr+Y=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 9, 10, 18, 22, 51, 92, DateTimeKind.Local).AddTicks(3430), null, "192.168.1.6", "Auxiliar 1", 3, "xqeLal7OBsXXCym7DFy7JQ==", "Anonimo", "Local" },
                    { 4, "Lestoma", "MWn8hP/M68NXmjyqPdnZZrmjHN7Vk625LPTSCLYsr+Y=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 9, 10, 18, 22, 51, 92, DateTimeKind.Local).AddTicks(3451), null, "192.168.1.6", "Auxiliar 2", 3, "xqeLal7OBsXXCym7DFy7JQ==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada", "trama_recibida", "dato_calculado_por_trama" },
                values: new object[,]
                {
                    { new Guid("57251655-2068-4eeb-b77c-4a921b4548e8"), new Guid("6b145453-c8c0-4f27-9610-ffd7cbf1b498"), true, new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(7778), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(7767), "192.168.1.6", "Anonimo", "Local", 1, "4901F000000000006180", null, null },
                    { new Guid("172f0bc9-bad3-4072-9c06-e97341742953"), new Guid("16a30b72-c2f5-4d34-8614-0cd88e89c496"), true, new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(9605), new DateTime(2022, 9, 10, 18, 22, 51, 117, DateTimeKind.Local).AddTicks(9595), "192.168.1.6", "Anonimo", "Local", 2, "6F01F000000000005302", null, null }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2"), 2, new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(6435), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2"), 2, new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(6450), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2"), 3, new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(6453), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new Guid("a51a271b-a8f1-4e1a-9624-c408707dc5e2"), 3, new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(6455), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("4211b5e9-98c6-42e1-b117-2e45e584394d"), new Guid("199ba499-09d6-4b1b-9c52-9199da02ef95"), 4, new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(6458), "192.168.1.6", "Anonimo", "Local" },
                    { new Guid("2fa15f0c-dae7-4540-9549-cc4228c29a48"), new Guid("199ba499-09d6-4b1b-9c52-9199da02ef95"), 4, new DateTime(2022, 9, 10, 18, 22, 51, 93, DateTimeKind.Local).AddTicks(6460), "192.168.1.6", "Anonimo", "Local" }
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
