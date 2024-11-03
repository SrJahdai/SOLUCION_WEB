using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEARFLIX.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    foto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    fecha_baja = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Banco__3213E83F0CDF7A3E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genero__3213E83FE0FDFB64", x => x.id);
                });

            //migrationBuilder.CreateTable(
            //    name: "GeneroPelicula",
            //    columns: table => new
            //    {
            //        IdGenero = table.Column<int>(type: "int", nullable: false),
            //        IdPelicula = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GeneroPelicula", x => new { x.IdGenero, x.IdPelicula });
            //    });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permiso__3213E83F368A8851", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    porcentaje = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__3213E83F8ECC1BBF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rango_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    rango_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_generacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ganancias_totales = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ganancias_brutas = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reporte__3213E83F5DC71BCC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__3213E83F867DD8B6", x => x.id);
                });

            //migrationBuilder.CreateTable(
            //    name: "RolUsuario",
            //    columns: table => new
            //    {
            //        IdRol = table.Column<int>(type: "int", nullable: false),
            //        IdUsuario = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RolUsuario", x => new { x.IdRol, x.IdUsuario });
            //    });

            migrationBuilder.CreateTable(
                name: "TipoTarjeta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoTarj__3213E83F3CCAC959", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVenta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoVent__3213E83F0D45F75B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    duracion = table.Column<int>(type: "int", nullable: false),
                    portada = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    fondo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    titulo_imagen = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    video = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    precio_compra = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    precio_renta = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pelicula__3213E83F8744204E", x => x.id);
                    table.ForeignKey(
                        name: "FK__Pelicula__id_pro__5441852A",
                        column: x => x.id_proveedor,
                        principalTable: "Proveedor",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PermisoRol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PermisoR__889447C48067D7CB", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK__PermisoRo__id_pe__6754599E",
                        column: x => x.id_permiso,
                        principalTable: "Permiso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PermisoRo__id_ro__66603565",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UsuarioR__5895CFF38AC7C735", x => new { x.id_usuario, x.id_rol });
                    table.ForeignKey(
                        name: "FK__UsuarioRo__id_ro__6383C8BA",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UsuarioRo__id_us__628FA481",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    titular = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    id_banco = table.Column<int>(type: "int", nullable: false),
                    vencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    ultimos_cuatro = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    id_tipo = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tarjeta__3213E83F307CD1CA", x => x.id);
                    table.ForeignKey(
                        name: "FK__Tarjeta__id_banc__5DCAEF64",
                        column: x => x.id_banco,
                        principalTable: "Banco",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tarjeta__id_tipo__5EBF139D",
                        column: x => x.id_tipo,
                        principalTable: "TipoTarjeta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tarjeta__id_usua__5FB337D6",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteProveedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_reporte = table.Column<int>(type: "int", nullable: false),
                    id_proveedor = table.Column<int>(type: "int", nullable: false),
                    id_tipo = table.Column<int>(type: "int", nullable: false),
                    total_ganancias = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    monto_a_pagar = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReporteP__3213E83F9CF1B662", x => x.id);
                    table.ForeignKey(
                        name: "FK__ReportePr__id_pr__778AC167",
                        column: x => x.id_proveedor,
                        principalTable: "Proveedor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__ReportePr__id_re__76969D2E",
                        column: x => x.id_reporte,
                        principalTable: "Reporte",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__ReportePr__id_ti__787EE5A0",
                        column: x => x.id_tipo,
                        principalTable: "TipoVenta",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PeliculaGenero",
                columns: table => new
                {
                    id_pelicula = table.Column<int>(type: "int", nullable: false),
                    id_genero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pelicula__3C9BF10288C9D129", x => new { x.id_pelicula, x.id_genero });
                    table.ForeignKey(
                        name: "FK__PeliculaG__id_ge__6B24EA82",
                        column: x => x.id_genero,
                        principalTable: "Genero",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PeliculaG__id_pe__6A30C649",
                        column: x => x.id_pelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puntaje",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_pelicula = table.Column<int>(type: "int", nullable: false),
                    puntaje = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Puntaje__856E135946915967", x => new { x.id_usuario, x.id_pelicula });
                    table.ForeignKey(
                        name: "FK__Puntaje__id_peli__6FE99F9F",
                        column: x => x.id_pelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Puntaje__id_usua__6EF57B66",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_pelicula = table.Column<int>(type: "int", nullable: false),
                    fecha_venta = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    expiracion = table.Column<DateTime>(type: "datetime", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    id_tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venta__3213E83F20DB1106", x => x.id);
                    table.ForeignKey(
                        name: "FK__Venta__id_pelicu__59063A47",
                        column: x => x.id_pelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Venta__id_tipo__59FA5E80",
                        column: x => x.id_tipo,
                        principalTable: "TipoVenta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Venta__id_usuari__5812160E",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__2A586E0B4FD6CE3F",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Banco__298336B6A846EEF5",
                table: "Banco",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Genero__298336B6535391E6",
                table: "Genero",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pelicula_titulo",
                table: "Pelicula",
                column: "titulo");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_id_proveedor",
                table: "Pelicula",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "UQ__Pelicula__38FA640FCFB2E8C2",
                table: "Pelicula",
                column: "titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pelicula_genero",
                table: "PeliculaGenero",
                column: "id_genero");

            migrationBuilder.CreateIndex(
                name: "UQ__Permiso__298336B64EE0242F",
                table: "Permiso",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermisoRol_id_permiso",
                table: "PermisoRol",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "idx_proveedor_descripcion",
                table: "Proveedor",
                column: "descripcion");

            migrationBuilder.CreateIndex(
                name: "UQ__Proveedo__298336B608C3003C",
                table: "Proveedor",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_puntaje_usuario",
                table: "Puntaje",
                columns: new[] { "id_usuario", "puntaje" });

            migrationBuilder.CreateIndex(
                name: "IX_Puntaje_id_pelicula",
                table: "Puntaje",
                column: "id_pelicula");

            migrationBuilder.CreateIndex(
                name: "idx_reporte_proveedor_tipo",
                table: "ReporteProveedor",
                columns: new[] { "id_proveedor", "id_tipo" });

            migrationBuilder.CreateIndex(
                name: "IX_ReporteProveedor_id_tipo",
                table: "ReporteProveedor",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "UQ__ReporteP__97F8BA38AAB4AB51",
                table: "ReporteProveedor",
                columns: new[] { "id_reporte", "id_proveedor", "id_tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Rol__298336B6F6AF6237",
                table: "Rol",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_id_banco",
                table: "Tarjeta",
                column: "id_banco");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_id_tipo",
                table: "Tarjeta",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_id_usuario",
                table: "Tarjeta",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "UQ__TipoTarj__298336B67DC1FCCB",
                table: "TipoTarjeta",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__TipoVent__298336B652AE8540",
                table: "TipoVenta",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_id_rol",
                table: "UsuarioRol",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "idx_venta_fecha",
                table: "Venta",
                column: "fecha_venta");

            migrationBuilder.CreateIndex(
                name: "idx_venta_pelicula",
                table: "Venta",
                column: "id_pelicula");

            migrationBuilder.CreateIndex(
                name: "idx_venta_tipo",
                table: "Venta",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "idx_venta_usuario",
                table: "Venta",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "GeneroPelicula");

            migrationBuilder.DropTable(
                name: "PeliculaGenero");

            migrationBuilder.DropTable(
                name: "PermisoRol");

            migrationBuilder.DropTable(
                name: "Puntaje");

            migrationBuilder.DropTable(
                name: "ReporteProveedor");

            //migrationBuilder.DropTable(
            //    name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Tarjeta");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "TipoTarjeta");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "TipoVenta");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Proveedor");
        }
    }
}
