using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace lestoma.Data.Migrations
{
    public partial class FirstData : Migration
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    modulo_componente_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    { new Guid("68d80433-a30f-46a8-9751-fa66a4df652f"), new DateTime(2022, 8, 6, 22, 15, 21, 562, DateTimeKind.Local).AddTicks(2401), "172.25.224.1", "ACTUADORES", "Anonimo", "Local" },
                    { new Guid("78049990-d5ba-481f-aa21-d23b444ad918"), new DateTime(2022, 8, 6, 22, 15, 21, 564, DateTimeKind.Local).AddTicks(7896), "172.25.224.1", "SENSORES", "Anonimo", "Local" }
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
                    { new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new DateTime(2022, 8, 6, 22, 15, 21, 601, DateTimeKind.Local).AddTicks(8022), "172.25.224.1", "control de agua", "Anonimo", "Local" },
                    { new Guid("be63bec7-8971-48b7-8385-7aab75c8b01d"), new DateTime(2022, 8, 6, 22, 15, 21, 601, DateTimeKind.Local).AddTicks(8047), "172.25.224.1", "alimentacion de peces", "Anonimo", "Local" }
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
                    { new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac"), (short)5, "queda ubicada en facatativá", new DateTime(2022, 8, 6, 22, 15, 21, 601, DateTimeKind.Local).AddTicks(5732), "172.25.224.1", "finca el vergel", "Anonimo", 1, "Local" },
                    { new Guid("efb96939-d8d1-455d-9efd-bf345ba43732"), (short)2, "queda ubicada en la universidad cundinamarca extensión nfaca", new DateTime(2022, 8, 6, 22, 15, 21, 601, DateTimeKind.Local).AddTicks(5758), "172.25.224.1", "ucundinamarca", "Anonimo", 1, "Local" }
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
                    { new Guid("49b17244-255e-4411-a2e4-1dc7211d922d"), new Guid("be63bec7-8971-48b7-8385-7aab75c8b01d"), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(928), "172.25.224.1", "{\"Id\":\"da39353a-e5c4-408a-a8f6-240bc15790f5\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("68d80433-a30f-46a8-9751-fa66a4df652f"), "BOMBA DE OXIGENO", "Anonimo", "Local", new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac") },
                    { new Guid("751ef5ec-cc5c-47d0-9fdc-058ce088e71d"), new Guid("be63bec7-8971-48b7-8385-7aab75c8b01d"), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(967), "172.25.224.1", "{\"Id\":\"da39353a-e5c4-408a-a8f6-240bc15790f5\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("68d80433-a30f-46a8-9751-fa66a4df652f"), "LUZ ESTANQUE", "Anonimo", "Local", new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac") },
                    { new Guid("6aa9f3fe-be72-48df-b327-a18c2c68e7c8"), new Guid("be63bec7-8971-48b7-8385-7aab75c8b01d"), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(993), "172.25.224.1", "{\"Id\":\"da39353a-e5c4-408a-a8f6-240bc15790f5\",\"TipoEstado\":\"ON-OFF\",\"TercerByteTrama\":\"F0\",\"CuartoByteTrama\":\"00\"}", new Guid("68d80433-a30f-46a8-9751-fa66a4df652f"), "DOSIFICADOR DE ALIMENTO", "Anonimo", "Local", new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac") },
                    { new Guid("cba16d2d-259b-407a-96a2-6a969e4a3532"), new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(998), "172.25.224.1", "{\"Id\":\"f7c21379-5977-4210-9651-fd5de7af4a5b\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("78049990-d5ba-481f-aa21-d23b444ad918"), "TEMPERATURA H2O", "Anonimo", "Local", new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac") },
                    { new Guid("7a2b6285-f5dd-4c16-be40-4dbabd4246e3"), new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(1001), "172.25.224.1", "{\"Id\":\"f7c21379-5977-4210-9651-fd5de7af4a5b\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("78049990-d5ba-481f-aa21-d23b444ad918"), "PH", "Anonimo", "Local", new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac") },
                    { new Guid("d62d15c6-f5a7-4b80-9054-9f2b035a48e9"), new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(1006), "172.25.224.1", "{\"Id\":\"f7c21379-5977-4210-9651-fd5de7af4a5b\",\"TipoEstado\":\"LECTURA\",\"TercerByteTrama\":\"0F\",\"CuartoByteTrama\":\"00\"}", new Guid("78049990-d5ba-481f-aa21-d23b444ad918"), "NIVEL TANQUE", "Anonimo", "Local", new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac") }
                });

            migrationBuilder.InsertData(
                schema: "usuarios",
                table: "usuario",
                columns: new[] { "id", "apellido", "clave", "codigo_recuperacion", "email", "estado_id", "fecha_creacion_server", "vencimiento_codigo_recuperacion", "ip", "nombre", "rol_id", "semilla", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { 1, "Lestoma", "3RVoU8PlKLRj1bgpxI1ippv6ZfGvfrUwVhLTLkCt/Xw=", null, "diegop177@hotmail.com", 2, new DateTime(2022, 8, 6, 22, 15, 21, 584, DateTimeKind.Local).AddTicks(6693), null, "172.25.224.1", "Super Admin", 1, "SfhvDub9d7Pm/3skIvAtiQ==", "Anonimo", "Local" },
                    { 2, "Lestoma", "v4bDfOL0aR0xJZ98MwaDFBZo7ebyEbL33d9mykAMz1g=", null, "diegoarturo1598@hotmail.com", 2, new DateTime(2022, 8, 6, 22, 15, 21, 592, DateTimeKind.Local).AddTicks(7510), null, "172.25.224.1", "Administrador", 2, "bA6SLT8feXPi23ZCL3ygBA==", "Anonimo", "Local" },
                    { 3, "Lestoma", "PNx6FbHdF+e8d/7j17B6Q0mhvKgNxJxPCOGCveXwO2k=", null, "programadoresuc@outlook.com", 2, new DateTime(2022, 8, 6, 22, 15, 21, 600, DateTimeKind.Local).AddTicks(7975), null, "172.25.224.1", "Auxiliar 1", 3, "DfxeXsg5YFHzTxmRTXGtzA==", "Anonimo", "Local" },
                    { 4, "Lestoma", "PNx6FbHdF+e8d/7j17B6Q0mhvKgNxJxPCOGCveXwO2k=", null, "auxiliar2@gmail.com", 2, new DateTime(2022, 8, 6, 22, 15, 21, 600, DateTimeKind.Local).AddTicks(8002), null, "172.25.224.1", "Auxiliar 2", 3, "DfxeXsg5YFHzTxmRTXGtzA==", "Anonimo", "Local" }
                });

            migrationBuilder.InsertData(
                schema: "laboratorio_lestoma",
                table: "detalle_laboratorio",
                columns: new[] { "id", "componente_laboratorio_id", "estado_internet", "fecha_creacion_dispositivo", "fecha_creacion_server", "ip", "session", "set_point", "tipo_de_aplicacion", "tipo_com_id", "trama_enviada" },
                values: new object[,]
                {
                    { new Guid("94a97b5f-996c-41d5-b172-538b3efe9e32"), new Guid("49b17244-255e-4411-a2e4-1dc7211d922d"), true, new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(4252), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(4239), "172.25.224.1", "Anonimo", null, "Local", 1, "4901F000000000006180" },
                    { new Guid("0d2ce4e4-5ebb-4601-8972-b5fe6a4f5004"), new Guid("751ef5ec-cc5c-47d0-9fdc-058ce088e71d"), true, new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(5967), new DateTime(2022, 8, 6, 22, 15, 21, 630, DateTimeKind.Local).AddTicks(5956), "172.25.224.1", "Anonimo", null, "Local", 2, "6F01F000000000005302" }
                });

            migrationBuilder.InsertData(
                schema: "superadmin",
                table: "upa_actividad",
                columns: new[] { "actividad_id", "upa_id", "usuario_id", "fecha_creacion_server", "ip", "session", "tipo_de_aplicacion" },
                values: new object[,]
                {
                    { new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac"), 2, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2066), "172.25.224.1", "Anonimo", "Local" },
                    { new Guid("be63bec7-8971-48b7-8385-7aab75c8b01d"), new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac"), 2, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2082), "172.25.224.1", "Anonimo", "Local" },
                    { new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new Guid("efb96939-d8d1-455d-9efd-bf345ba43732"), 2, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2091), "172.25.224.1", "Anonimo", "Local" },
                    { new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac"), 3, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2086), "172.25.224.1", "Anonimo", "Local" },
                    { new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new Guid("efb96939-d8d1-455d-9efd-bf345ba43732"), 3, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2095), "172.25.224.1", "Anonimo", "Local" },
                    { new Guid("be63bec7-8971-48b7-8385-7aab75c8b01d"), new Guid("6e1cb560-1047-4f98-8e2f-e2e57c93f8ac"), 4, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2088), "172.25.224.1", "Anonimo", "Local" },
                    { new Guid("bf984295-6d23-42f9-a790-d567b540ccaf"), new Guid("efb96939-d8d1-455d-9efd-bf345ba43732"), 4, new DateTime(2022, 8, 6, 22, 15, 21, 602, DateTimeKind.Local).AddTicks(2098), "172.25.224.1", "Anonimo", "Local" }
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
