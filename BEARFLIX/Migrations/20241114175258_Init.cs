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
                name: "Banco",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Banco__3213E83F453A5249", x => x.id);
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
                    table.PrimaryKey("PK__Genero__3213E83F45240470", x => x.id);
                });

          

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
                    table.PrimaryKey("PK__Permiso__3213E83F17871AFC", x => x.id);
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
                    table.PrimaryKey("PK__Proveedo__3213E83FB22503CD", x => x.id);
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
                    table.PrimaryKey("PK__Reporte__3213E83F24E5CBEB", x => x.id);
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
                    table.PrimaryKey("PK__Rol__3213E83FF5DAB9A0", x => x.id);
                });

           

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
                    table.PrimaryKey("PK__TipoTarj__3213E83FA6CA5840", x => x.id);
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
                    table.PrimaryKey("PK__TipoVent__3213E83F0CA5A2F4", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    correo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    contrasena = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    foto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    fecha_baja = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3213E83F1BCC89CA", x => x.id);
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
                    estreno = table.Column<DateOnly>(type: "date", nullable: false),
                    video = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    precio_compra = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    precio_renta = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pelicula__3213E83FBFB2EB9D", x => x.id);
                    table.ForeignKey(
                        name: "FK__Pelicula__id_pro__66603565",
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
                    table.PrimaryKey("PK__PermisoR__889447C4CE063B3D", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK__PermisoRo__id_pe__797309D9",
                        column: x => x.id_permiso,
                        principalTable: "Permiso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PermisoRo__id_ro__787EE5A0",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id",
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
                    table.PrimaryKey("PK__ReporteP__3213E83FFF906C72", x => x.id);
                    table.ForeignKey(
                        name: "FK__ReportePr__id_pr__09A971A2",
                        column: x => x.id_proveedor,
                        principalTable: "Proveedor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__ReportePr__id_re__08B54D69",
                        column: x => x.id_reporte,
                        principalTable: "Reporte",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__ReportePr__id_ti__0A9D95DB",
                        column: x => x.id_tipo,
                        principalTable: "TipoVenta",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__Tarjeta__3213E83FA1E6AB09", x => x.id);
                    table.ForeignKey(
                        name: "FK__Tarjeta__id_banc__6FE99F9F",
                        column: x => x.id_banco,
                        principalTable: "Banco",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tarjeta__id_tipo__70DDC3D8",
                        column: x => x.id_tipo,
                        principalTable: "TipoTarjeta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tarjeta__id_usua__71D1E811",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
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
                    table.PrimaryKey("PK__UsuarioR__5895CFF39031F745", x => new { x.id_usuario, x.id_rol });
                    table.ForeignKey(
                        name: "FK__UsuarioRo__id_ro__75A278F5",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UsuarioRo__id_us__74AE54BC",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.PrimaryKey("PK__Pelicula__3C9BF10231928B9A", x => new { x.id_pelicula, x.id_genero });
                    table.ForeignKey(
                        name: "FK__PeliculaG__id_ge__7D439ABD",
                        column: x => x.id_genero,
                        principalTable: "Genero",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PeliculaG__id_pe__7C4F7684",
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
                    table.PrimaryKey("PK__Puntaje__856E135952663870", x => new { x.id_usuario, x.id_pelicula });
                    table.ForeignKey(
                        name: "FK__Puntaje__id_peli__02084FDA",
                        column: x => x.id_pelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Puntaje__id_usua__01142BA1",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
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
                    table.PrimaryKey("PK__Venta__3213E83FD2C93F27", x => x.id);
                    table.ForeignKey(
                        name: "FK__Venta__id_pelicu__6B24EA82",
                        column: x => x.id_pelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Venta__id_tipo__6C190EBB",
                        column: x => x.id_tipo,
                        principalTable: "TipoVenta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Venta__id_usuari__6A30C649",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Banco__298336B681B1EA29",
                table: "Banco",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Genero__298336B6F1ABFD5C",
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
                name: "UQ__Pelicula__38FA640F3196FD60",
                table: "Pelicula",
                column: "titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pelicula_genero",
                table: "PeliculaGenero",
                column: "id_genero");

            migrationBuilder.CreateIndex(
                name: "UQ__Permiso__298336B6A2280B6D",
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
                name: "UQ__Proveedo__298336B672F23E65",
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
                name: "UQ__ReporteP__97F8BA38FD28AF8C",
                table: "ReporteProveedor",
                columns: new[] { "id_reporte", "id_proveedor", "id_tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Rol__298336B62AAA1E95",
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
                name: "UQ__TipoTarj__298336B6FCC554AD",
                table: "TipoTarjeta",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__TipoVent__298336B67F4349E7",
                table: "TipoVenta",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuario_correo",
                table: "Usuario",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__2A586E0BF5ABB513",
                table: "Usuario",
                column: "correo",
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
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Proveedor");
        }
    }
}
