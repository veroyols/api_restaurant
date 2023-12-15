using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntrega",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntrega", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "integer", nullable: false),
                    PrecioTotal = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormaEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntrega",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderia",
                columns: table => new
                {
                    MercaderiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<int>(type: "integer", nullable: false),
                    Ingredientes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Preparacion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Imagen = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderia", x => x.MercaderiaId);
                    table.ForeignKey(
                        name: "FK_Mercaderia_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderia",
                columns: table => new
                {
                    ComandaMercaderiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MercaderiaId = table.Column<int>(type: "integer", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderia", x => x.ComandaMercaderiaId);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Mercaderia_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderia",
                        principalColumn: "MercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormaEntrega",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Salon" },
                    { 2, "Delivery" },
                    { 3, "Pedidos Ya" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Minutas" },
                    { 3, "Pastas" },
                    { 4, "Parrilla" },
                    { 5, "Pizzas" },
                    { 6, "Sandwich" },
                    { 7, "Ensaladas" },
                    { 8, "Bebidas" },
                    { 9, "Cerveza Artesanal" },
                    { 10, "Postres" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderia",
                columns: new[] { "MercaderiaId", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 1, "https://ibb.co/9csGtkK", "Berenjenas, vinagre, aceite de oliva, ajo, laurel, orégano, pimentón, sal. ", "Berenjenas en Escabeche", 500, "Cortar las berenjenas y colocarlas en una olla con vinagre y agua. Hervir y colocar las rodajas en un frasco de vidrio esterilizado. Cubrirlas con ajo picado y agregar sal y aceite a gusto. Dejar reposar durante 24 horas antes de consumir.", 1 },
                    { 2, "https://ibb.co/NnHP6QS", "Porotos, cebolla, ají molido, vinagre, aceite, sal y pimienta. ", "Porotos a la vinagreta", 500, "Saltear la cebolla. Agregar ají molido, el vinagre y cocinar hasta que hierva. En un frasco de vidrio esterilizado, colocar los porotos y cubrir con la mezcla de cebolla. Dejar reposar durante 24 horas antes de consumir.", 1 },
                    { 3, "https://ibb.co/cDS7KRw", "Garbanzos cocidos, jugo de limón, aceite de oliva, ajo, sal y comino. ", "Humus de Garbanzo", 500, "Colocar los garbanzos, el jugo de limón, el ajo, el comino, la sal y procesar hasta obtener una pasta suave. Agregar aceite de oliva hasta obtener la consistencia deseada. Servir con tostaditas con ajo.", 1 },
                    { 4, "https://ibb.co/XSC5P3M", "Carne de vaca, pan rallado, huevo, salsa de tomate, jamón, queso y aceite para freír.", "Milanesa Napolitana", 1500, "Desgrasar la carne y dejar reposar en un recipiente con huevo batido, ajo, perejil, sal y pimienta. Pasar por pan rallado. Freír en aceite caliente hasta que esté dorado y crujiente. Cubrir con salsa de tomate, jamón y queso y hornear.", 2 },
                    { 5, "https://ibb.co/W2fWJ3b", "Papas, aceite y sal.", "Papas Fritas", 1000, "Pelar y cortar las papas en tiras delgadas. Freír en aceite caliente hasta que estén doradas y crujientes. Escurrir y salar al gusto.", 2 },
                    { 6, "https://ibb.co/LxLD6Dn", "Masa de empanada, carne picada, cebolla, huevo duro, ají molido, sal, cebolla de verdeo.", "Empanada", 400, "Cocinar la carne con cebolla, ají molido y sal. Agregar huevo duro y cebolla de verdero picada. Armar las empanadas y hornear.", 2 },
                    { 7, "https://ibb.co/nRkD8Td", "Huevos, queso fresco, orégano, sal y pimienta", "Omelette", 900, "Batir los huevos con sal y pimienta. Verter en la sartén antiadherente. Agregar queso y oregano. Enrollar y servir.", 2 },
                    { 8, "https://ibb.co/8dL8BVR", "Masa para tarta, jamón, queso, tomate, huevos, sal y pimienta.", "Tarta", 2500, "Estirar la masa para tarta en un molde. Cubrir con jamón, tomate y queso. Batir los huevos con sal y pimienta. Verter sobre la tarta. Cubrir con masa y hornear.", 2 },
                    { 9, "https://ibb.co/hW2Kx6Z", "Fideos, queso crema, queso parmesano, queso roquefort, queso rallado, leche, manteca.", "Fideos con salsa cuatro quesos", 2100, "Cocinar los fideos según las instrucciones del paquete. Mezclar en una sartén la leche con los quesos, a fuego bajo hasta que se derritan. Agregar la manteca y revolver. Servir la salsa sobre los fideos.", 3 },
                    { 10, "https://ibb.co/Lkz3QnZ", "Ñoquis, carne picada, cebolla, ajo, tomate, aceite, albahaca, sal y pimienta", "Ñoquis con Salsa Bolognesa", 2500, "Dorar el ajo y agregar cebolla en aceite hasta dorar. Agregar la carne. Agregar tomate picado, albahaca y dejar reducir. Cocinar a fuego bajo. Servir sobre los ñoquis.", 3 },
                    { 11, "https://ibb.co/Kjq1TbW", "Sorrentinos, tomate, cebolla, ajo, aceite, sal y pimienta.", "Sorrentinos con Salsa Filetto", 2500, "Cocinar los sorrentinos según las instrucciones del paquete. En una sartén, dorar cebolla y ajo picados en aceite. Agregar tomate picado y cocinar. Dejar reducir. Cocinar a fuego bajo. Servir sobre los sorrentinos.", 3 },
                    { 12, "https://ibb.co/Dw3s1yk", "Queso provolone, orégano, aceite de oliva.", "Provoleta", 1200, "Cortar el queso en rodajas y rociar con aceite de oliva. Espolvorear orégano. Cocinar a la parrilla o en sartén hasta que esté dorado.", 4 },
                    { 13, "https://ibb.co/28v9M20", "Asado, vacio, chorizo, morcilla, chinchulines, riñones, mollejas, sal y limón.", "Parrillada para 1", 2000, "Salar la carne y cocinar en la parrilla a fuego medio. Servir.", 4 },
                    { 14, "https://ibb.co/X8jxHqV", "Asado, vacio, chorizo, morcilla, chinchulines, riñones, mollejas, sal y limón.", "Parrillada para 2", 3000, "Salar la carne y cocinar en la parrilla a fuego medio. Servir.", 4 },
                    { 15, "https://ibb.co/XzTFN7V", "Harina, levadura, aceite y sal. Salsa de tomate, ajo, queso muzzarella, aceitunas. ", "Muzzarella", 2300, "Dejar levar la levadura con agua tibia. Mezclar con harina y sal, amasar y dejar reposar untada en aceite. Formar bollos y dejar reposar. Precocinar a la parrilla, agregar salsa de tomate, queso y hornear con bandeja encima. Agregar aceitunas y servir.", 5 },
                    { 16, "https://ibb.co/xCyjZ4L", "Harina, levadura, aceite y sal. Tomate, ajo, albahaca, queso muzzarella, aceitunas. ", "Napolitana", 2500, "Levar levadura con agua tibia. Añadir a recipiente con harina y sal. Amasar y dejar reposar untada en aceite. Formar bollos y reposar. Precocinar en parrilla. Agregar salsa, queso y hornear. Añadir tomate, ajo, albahaca, aceitunas. Servir.", 5 },
                    { 17, "https://ibb.co/0qMCVmW", "Harina, levadura, aceite y sal. Salsa de tomate, ajo, queso muzzarella, rucula, jamon crudo, tomate cherry, aceitunas negras. ", "Rucula y jamon crudo", 3000, "Levar la levadura, mezclar con harina y sal. Reposar la masa en aceite y tapar. Formar bollos y reposar. Precocinar la masa en parrilla, agregar salsa, queso, bandeja y hornear. Agregar jamón crudo, rúcula, tomate cherry, aceitunas y servir.", 5 },
                    { 18, "https://ibb.co/yYrBzDK", "Harina, levadura, aceite y sal. Queso muzzarella, jamon cocido, cebolla.", "Fugazetta Rellena", 3200, "Dejar levar la levadura con agua tibia. Agregar harina y sal.Amasar y dejar reposar la masa en aceite tapada.Formar bollos y dejar reposar.Extender masa en bandeja.Agregar jamon y queso.Tapar con masa y cebolla.Hornear y servir.", 5 },
                    { 19, "https://ibb.co/LvCv8xk", "Pan de hamburguesa, carne picada, lechuga, tomate, cebolla, queso cheddar.", "Hamburguesa", 1000, "Mezclar carne picada, ajo, perejil, sal, pimienta, huevo. Formar bolitas con pan rallado. Aplastar y cocinar en parrilla o sartén. Agregar cheddar y dar vuelta. Armar hamburguesa con carne, lechuga, tomate y cebolla en pan.", 6 },
                    { 20, "https://ibb.co/FKM7zpK", "Lechuga, cebolla, aceite de giralsol, vinagre de alcohol.", "Lechuga y Cebolla", 700, "Cortar la lechuga en trozos y la cebolla en juliana. Mezclar en un recipiente y agregar aceite, vinagre y sal.", 7 },
                    { 21, "https://ibb.co/q14Ndmn", "Agua.", "Agua", 500, "Servir en un vaso.", 8 },
                    { 22, "https://ibb.co/svz8cZh", "Agua con gas.", "Agua con gas", 500, "Servir en un vaso.", 8 },
                    { 23, "https://ibb.co/HBbZYmD", "CocaCola.", "CocaCola", 700, "Servir en un vaso.", 8 },
                    { 24, "https://ibb.co/ZWz4gc9", "Agua Tonica", "Agua Tonica", 700, "Servir en un vaso.", 8 },
                    { 25, "https://ibb.co/qxyZvdN", "Vino de la casa.", "Vino", 2000, "Servir en una copa.", 8 },
                    { 26, "https://ibb.co/kMYYCz5", "Maltas claras, lupulos suaves y agua.", "Blonde Ale", 700, "Se mezclan los ingredientes en agua caliente para producir mosto. Luego se agrega levadura para la fermentación, y después de algunos días, se empaqueta en barriles para la carbonatación. Finalmente, se enfría y se sirve.", 9 },
                    { 27, "https://ibb.co/KrBnctH", "Maltas oscuras, lupulos amargos, agua y miel.", "Honey", 700, "Se mezclan los ingredientes en agua caliente para producir mosto. Luego se agrega levadura para la fermentación, y después de algunos días, se empaqueta en barriles para la carbonatación. Finalmente, se enfría y se sirve.", 9 },
                    { 28, "https://ibb.co/ZL6j5wN", "Maltas palidas, lupulos fuertes y agua.", "Scottish", 700, "Se mezclan los ingredientes en agua caliente para producir mosto. Luego se agrega levadura para la fermentación, y después de algunos días, se empaqueta en barriles para la carbonatación. Finalmente, se enfría y se sirve.", 9 },
                    { 29, "https://ibb.co/ZYjVDWq", "Pan, leche, huevos, azúcar, pasas de uva, esencia de vainilla. ", "Budin de Pan", 800, "Remojar el pan en la mezcla de leche, huevos, azúcar y vainilla. Agregar pasas y hornear.", 10 },
                    { 30, "https://ibb.co/9W1bpvN", "Helado, salsa de chocolate y crema. ", "Copa Helada", 800, "Colocar el helado en una copa y cubrir con la salsa de chocolate. Agregar crema batida.", 10 },
                    { 31, "https://ibb.co/bzrV3FD", "Harina, leche, huevos, sal, dulce de leche. ", "Panqueques con dulce de leche", 800, "Batir leche, huevo y harina con una pizca de sal. Cocinar en sartén caliente. Untar con dulce de leche y enrollar.", 10 },
                    { 32, "https://ibb.co/hdSjB77", "Frutillas, crema batida, azúcar. ", "Frutillas con Crema", 800, "Cortar las frutillas, agregar azúcar y dejar reposar. Servir con crema batida.", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_ComandaId",
                table: "ComandaMercaderia",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_MercaderiaId",
                table: "ComandaMercaderia",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_TipoMercaderiaId",
                table: "Mercaderia",
                column: "TipoMercaderiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderia");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderia");

            migrationBuilder.DropTable(
                name: "FormaEntrega");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}
