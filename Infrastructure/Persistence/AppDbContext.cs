using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<TipoMercaderia> TipoMercaderiaDb { get; set; }
        public DbSet<FormaEntrega> FormaEntregaDb { get; set; }
        public DbSet<Comanda> ComandaDb { get; set; }
        public DbSet<Mercaderia> MercaderiaDb { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderiaDb { get; set; }

        //CONECTION STRING
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=TP2-REST-Scholz_Veronica;Trusted_Connection=True;TrustServerCertificate=True");
        }

        //MODELADO -> FluentApi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TIPOMERCADERIA
            modelBuilder.Entity<TipoMercaderia>(entity =>
            {
                entity.ToTable("TipoMercaderia");
                entity.HasKey(tm => tm.TipoMercaderiaId);
                entity.Property(tm => tm.Descripcion).HasColumnType("nvarchar(50)");
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 1, Descripcion = "Entrada" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 2, Descripcion = "Minutas" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 3, Descripcion = "Pastas" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 4, Descripcion = "Parrilla" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 5, Descripcion = "Pizzas" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 6, Descripcion = "Sandwich" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 7, Descripcion = "Ensaladas" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 8, Descripcion = "Bebidas" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 9, Descripcion = "Cerveza Artesanal" });
                entity.HasData(new TipoMercaderia { TipoMercaderiaId = 10, Descripcion = "Postres" });

                //RELACION: 0 o muchos con Mercaderia

            });

            //FORMAENTREGA
            modelBuilder.Entity<FormaEntrega>(entity =>
            {
                entity.ToTable("FormaEntrega");
                entity.HasKey(fe => fe.FormaEntregaId);
                entity.Property(fe => fe.Descripcion).HasColumnType("nvarchar(50)");
                entity.HasData(new FormaEntrega { FormaEntregaId = 1, Descripcion = "Salon" });
                entity.HasData(new FormaEntrega { FormaEntregaId = 2, Descripcion = "Delivery" });
                entity.HasData(new FormaEntrega { FormaEntregaId = 3, Descripcion = "Pedidos Ya" });

                //RELACION: 0 o muchos con Comanda

            });

            //MERCADERIA
            modelBuilder.Entity<Mercaderia>(entity =>
            {
            entity.ToTable("Mercaderia");
            entity.HasKey(m => m.MercaderiaId);
            entity.Property(m => m.Nombre).HasColumnType("nvarchar(50)");
            entity.Property(m => m.Ingredientes).HasColumnType("nvarchar(255)");
            entity.Property(m => m.Preparacion).HasColumnType("nvarchar(255)");
            entity.Property(m => m.Imagen).HasColumnType("nvarchar(255)");

                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 1,
                    Nombre = "Berenjenas en Escabeche",
                    TipoMercaderiaId = 1,
                    Precio = 500,
                    Ingredientes = "Berenjenas, vinagre, aceite de oliva, ajo, laurel, orégano, pimentón, sal. ",
                    Preparacion = "Cortar las berenjenas y colocarlas en una olla con vinagre y agua. Hervir y colocar las rodajas en un frasco de vidrio esterilizado. Cubrirlas con ajo picado y agregar sal y aceite a gusto. Dejar reposar durante 24 horas antes de consumir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=19RZ1fcdX8FaYZapFayXSKkvSeh_eD8sx"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 2,
                    Nombre = "Porotos a la vinagreta",
                    TipoMercaderiaId = 1,
                    Precio = 500,
                    Ingredientes = "Porotos, cebolla, ají molido, vinagre, aceite, sal y pimienta. ",
                    Preparacion = "Saltear la cebolla. Agregar ají molido, el vinagre y cocinar hasta que hierva. En un frasco de vidrio esterilizado, colocar los porotos y cubrir con la mezcla de cebolla. Dejar reposar durante 24 horas antes de consumir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1xdVsFI-bfmKKrxUOoRvLwNy_tpZi1tlL"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 3,
                    Nombre = "Humus de Garbanzo",
                    TipoMercaderiaId = 1,
                    Precio = 500,
                    Ingredientes = "Garbanzos cocidos, jugo de limón, aceite de oliva, ajo, sal y comino. ",
                    Preparacion = "Colocar los garbanzos, el jugo de limón, el ajo, el comino, la sal y procesar hasta obtener una pasta suave. Agregar aceite de oliva hasta obtener la consistencia deseada. Servir con tostaditas con ajo.",
                    Imagen = "http://drive.google.com/uc?export=view&id=18PT8STxFOdbqcWPo2qbEeX-dmHqrKL4U"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 4,
                    Nombre = "Milanesa Napolitana",
                    TipoMercaderiaId = 2,
                    Precio = 1500,
                    Ingredientes = "Carne de vaca, pan rallado, huevo, salsa de tomate, jamón, queso y aceite para freír.",
                    Preparacion = "Desgrasar la carne y dejar reposar en un recipiente con huevo batido, ajo, perejil, sal y pimienta. Pasar por pan rallado. Freír en aceite caliente hasta que esté dorado y crujiente. Cubrir con salsa de tomate, jamón y queso y hornear.",
                    Imagen = "http://drive.google.com/uc?export=view&id=10KYo-Sx37qfrCCJb2NDwjfBwc0x7k6ya"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 5,
                    Nombre = "Papas Fritas",
                    TipoMercaderiaId = 2,
                    Precio = 1000,
                    Ingredientes = "Papas, aceite y sal.",
                    Preparacion = "Pelar y cortar las papas en tiras delgadas. Freír en aceite caliente hasta que estén doradas y crujientes. Escurrir y salar al gusto.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1I1Q6EtlwXY8hCll7GSrXut1aMtjBWCZe"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 6,
                    Nombre = "Empanada",
                    TipoMercaderiaId = 2,
                    Precio = 400,
                    Ingredientes = "Masa de empanada, carne picada, cebolla, huevo duro, ají molido, sal, cebolla de verdeo.",
                    Preparacion = "Cocinar la carne con cebolla, ají molido y sal. Agregar huevo duro y cebolla de verdero picada. Armar las empanadas y hornear.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1f-ZxsKFvooSuCWt1KWnX3xn6DnAF4Z88"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 7,
                    Nombre = "Omelette",
                    TipoMercaderiaId = 2,
                    Precio = 900,
                    Ingredientes = "Huevos, queso fresco, orégano, sal y pimienta",
                    Preparacion = "Batir los huevos con sal y pimienta. Verter en la sartén antiadherente. Agregar queso y oregano. Enrollar y servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1o9q6P2my5mlVrtxmPE-vLHNDv62psRAq"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 8,
                    Nombre = "Tarta",
                    TipoMercaderiaId = 2,
                    Precio = 2500,
                    Ingredientes = "Masa para tarta, jamón, queso, tomate, huevos, sal y pimienta.",
                    Preparacion = "Estirar la masa para tarta en un molde. Cubrir con jamón, tomate y queso. Batir los huevos con sal y pimienta. Verter sobre la tarta. Cubrir con masa y hornear.",
                    Imagen = "http://drive.google.com/uc?export=view&id=11wwHdYe4Zi0eMRrhay3cRzpywlsBbr2a"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 9,
                    Nombre = "Fideos con salsa cuatro quesos",
                    TipoMercaderiaId = 3,
                    Precio = 2100,
                    Ingredientes = "Fideos, queso crema, queso parmesano, queso roquefort, queso rallado, leche, manteca.",
                    Preparacion = "Cocinar los fideos según las instrucciones del paquete. Mezclar en una sartén la leche con los quesos, a fuego bajo hasta que se derritan. Agregar la manteca y revolver. Servir la salsa sobre los fideos.",
                    Imagen = "http://drive.google.com/uc?export=view&id=15lJhT2aRy3StYLz76Ikt33Z-ViZaN8nS"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 10,
                    Nombre = "Ñoquis con Salsa Bolognesa",
                    TipoMercaderiaId = 3,
                    Precio = 2500,
                    Ingredientes = "Ñoquis, carne picada, cebolla, ajo, tomate, aceite, albahaca, sal y pimienta",
                    Preparacion = "Dorar el ajo y agregar cebolla en aceite hasta dorar. Agregar la carne. Agregar tomate picado, albahaca y dejar reducir. Cocinar a fuego bajo. Servir sobre los ñoquis.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1kYYKJPH865M4a4uoF9FrC0kOkONntdPn"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 11,
                    Nombre = "Sorrentinos con Salsa Filetto",
                    TipoMercaderiaId = 3,
                    Precio = 2500,
                    Ingredientes = "Sorrentinos, tomate, cebolla, ajo, aceite, sal y pimienta.",
                    Preparacion = "Cocinar los sorrentinos según las instrucciones del paquete. En una sartén, dorar cebolla y ajo picados en aceite. Agregar tomate picado y cocinar. Dejar reducir. Cocinar a fuego bajo. Servir sobre los sorrentinos.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1HujVQHj9rpi4NpW_Dteve04cGO760-Q6"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 12,
                    Nombre = "Provoleta",
                    TipoMercaderiaId = 4,
                    Precio = 1200,
                    Ingredientes = "Queso provolone, orégano, aceite de oliva.",
                    Preparacion = "Cortar el queso en rodajas y rociar con aceite de oliva. Espolvorear orégano. Cocinar a la parrilla o en sartén hasta que esté dorado.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1Zca5YZ3-d9-R9mpBcKJNPloodPNyO5mK"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 13,
                    Nombre = "Parrillada para 1",
                    TipoMercaderiaId = 4,
                    Precio = 2000,
                    Ingredientes = "Asado, vacio, chorizo, morcilla, chinchulines, riñones, mollejas, sal y limón.",
                    Preparacion = "Salar la carne y cocinar en la parrilla a fuego medio. Servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1KwW_hTxoX1OCaowkrUa4zlWiGwyJONkG"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 14,
                    Nombre = "Parrillada para 2",
                    TipoMercaderiaId = 4,
                    Precio = 3000,
                    Ingredientes = "Asado, vacio, chorizo, morcilla, chinchulines, riñones, mollejas, sal y limón.",
                    Preparacion = "Salar la carne y cocinar en la parrilla a fuego medio. Servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1tjBKJ7w5JIhhw3C3Fi5ZxLu798SSdbIL"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 15,
                    Nombre = "Muzzarella",
                    TipoMercaderiaId = 5,
                    Precio = 2300,
                    Ingredientes = "Harina, levadura, aceite y sal. Salsa de tomate, ajo, queso muzzarella, aceitunas. ",
                    Preparacion = "Dejar levar la levadura con agua tibia. Mezclar con harina y sal, amasar y dejar reposar untada en aceite. Formar bollos y dejar reposar. Precocinar a la parrilla, agregar salsa de tomate, queso y hornear con bandeja encima. Agregar aceitunas y servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1D05vfv2K1VokG5KlXzsxLBLWrqRteAjc"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 16,
                    Nombre = "Napolitana",
                    TipoMercaderiaId = 5,
                    Precio = 2500,
                    Ingredientes = "Harina, levadura, aceite y sal. Tomate, ajo, albahaca, queso muzzarella, aceitunas. ",
                    Preparacion = "Levar levadura con agua tibia. Añadir a recipiente con harina y sal. Amasar y dejar reposar untada en aceite. Formar bollos y reposar. Precocinar en parrilla. Agregar salsa, queso y hornear. Añadir tomate, ajo, albahaca, aceitunas. Servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1_FdIEzrM-QS5H78syVAoRX9htrZUCQ7T"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 17,
                    Nombre = "Rucula y jamon crudo",
                    TipoMercaderiaId = 5,
                    Precio = 3000,
                    Ingredientes = "Harina, levadura, aceite y sal. Salsa de tomate, ajo, queso muzzarella, rucula, jamon crudo, tomate cherry, aceitunas negras. ",
                    Preparacion = "Levar la levadura, mezclar con harina y sal. Reposar la masa en aceite y tapar. Formar bollos y reposar. Precocinar la masa en parrilla, agregar salsa, queso, bandeja y hornear. Agregar jamón crudo, rúcula, tomate cherry, aceitunas y servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1ekxSyUVbsuGzl00I5bIRYUt0Ntv2bmRC"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 18,
                    Nombre = "Fugazetta Rellena",
                    TipoMercaderiaId = 5,
                    Precio = 3200,
                    Ingredientes = "Harina, levadura, aceite y sal. Queso muzzarella, jamon cocido, cebolla.",
                    Preparacion = "Dejar levar la levadura con agua tibia. Agregar harina y sal.Amasar y dejar reposar la masa en aceite tapada.Formar bollos y dejar reposar.Extender masa en bandeja.Agregar jamon y queso.Tapar con masa y cebolla.Hornear y servir.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1U_FDLjY-DvNft-6vYpDSwCM50VBdkWmR"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 19,
                    Nombre = "Hamburguesa",
                    TipoMercaderiaId = 6,
                    Precio = 1000,
                    Ingredientes = "Pan de hamburguesa, carne picada, lechuga, tomate, cebolla, queso cheddar.",
                    Preparacion = "Mezclar carne picada, ajo, perejil, sal, pimienta, huevo. Formar bolitas con pan rallado. Aplastar y cocinar en parrilla o sartén. Agregar cheddar y dar vuelta. Armar hamburguesa con carne, lechuga, tomate y cebolla en pan.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1MvwhTPB-mIU2U7a_ZEjjgj3QhbMXOJ1B"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 20,
                    Nombre = "Lechuga y Cebolla",
                    TipoMercaderiaId = 7,
                    Precio = 700,
                    Ingredientes = "Lechuga, cebolla, aceite de giralsol, vinagre de alcohol.",
                    Preparacion = "Cortar la lechuga en trozos y la cebolla en juliana. Mezclar en un recipiente y agregar aceite, vinagre y sal.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1RCO7t5V9v5nMA7mUIMeSzpqDyVDVLWcR"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 21,
                    Nombre = "Agua",
                    TipoMercaderiaId = 8,
                    Precio = 500,
                    Ingredientes = "Agua.",
                    Preparacion = "Servir en un vaso.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1AT0FKcyPQ_YGsRma5Ngu6GX8ftf8Ywwd"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 22,
                    Nombre = "Agua con gas",
                    TipoMercaderiaId = 8,
                    Precio = 500,
                    Ingredientes = "Agua con gas.",
                    Preparacion = "Servir en un vaso.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1LgUFou-0XV4ZGXLvl0VdOudpB2xNowUQ"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 23,
                    Nombre = "CocaCola",
                    TipoMercaderiaId = 8,
                    Precio = 700,
                    Ingredientes = "CocaCola.",
                    Preparacion = "Servir en un vaso.",
                    Imagen = "http://drive.google.com/uc?export=view&id=19_2s9DOCYfjSeOM8oOMjfPLZQxrIbrax"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 24,
                    Nombre = "Agua Tonica",
                    TipoMercaderiaId = 8,
                    Precio = 700,
                    Ingredientes = "Agua Tonica",
                    Preparacion = "Servir en un vaso.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1GPpCuTHDb_YfZ5WgfBpwnh6gCZ4UzYh9"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 25,
                    Nombre = "Vino",
                    TipoMercaderiaId = 8,
                    Precio = 2000,
                    Ingredientes = "Vino de la casa.",
                    Preparacion = "Servir en una copa.",
                    Imagen = "http://drive.google.com/uc?export=view&id=17DJ0vDYpvAlRvFtfor2hVXmB3nb6p5KK"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 26,
                    Nombre = "Blonde Ale",
                    TipoMercaderiaId = 9,
                    Precio = 700,
                    Ingredientes = "Maltas claras, lupulos suaves y agua.",
                    Preparacion = "Se mezclan los ingredientes en agua caliente para producir mosto. Luego se agrega levadura para la fermentación, y después de algunos días, se empaqueta en barriles para la carbonatación. Finalmente, se enfría y se sirve.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1SdRfF1u3J6ejfsoYoMIiXLSaplHcNaDS"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 27,
                    Nombre = "Honey",
                    TipoMercaderiaId = 9,
                    Precio = 700,
                    Ingredientes = "Maltas oscuras, lupulos amargos, agua y miel.",
                    Preparacion = "Se mezclan los ingredientes en agua caliente para producir mosto. Luego se agrega levadura para la fermentación, y después de algunos días, se empaqueta en barriles para la carbonatación. Finalmente, se enfría y se sirve.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1uNGK3SZOYrbb4SkF9TZpEhw-qQ1i_6Su"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 28,
                    Nombre = "Scottish",
                    TipoMercaderiaId = 9,
                    Precio = 700,
                    Ingredientes = "Maltas palidas, lupulos fuertes y agua.",
                    Preparacion = "Se mezclan los ingredientes en agua caliente para producir mosto. Luego se agrega levadura para la fermentación, y después de algunos días, se empaqueta en barriles para la carbonatación. Finalmente, se enfría y se sirve.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1dupPwq4BpsOAfeloQt0zncUPgS5b8TSL"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 29,
                    Nombre = "Budin de Pan",
                    TipoMercaderiaId = 10,
                    Precio = 800,
                    Ingredientes = "Pan, leche, huevos, azúcar, pasas de uva, esencia de vainilla. ",
                    Preparacion = "Remojar el pan en la mezcla de leche, huevos, azúcar y vainilla. Agregar pasas y hornear.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1ElTRsSRJST0nTWKSsfQkR4XSeNhlI6Al"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 30,
                    Nombre = "Copa Helada",
                    TipoMercaderiaId = 10,
                    Precio = 800,
                    Ingredientes = "Helado, salsa de chocolate y crema. ",
                    Preparacion = "Colocar el helado en una copa y cubrir con la salsa de chocolate. Agregar crema batida.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1wq625i2tDiHzQRVGrU_i3hYJpzuPwryc"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 31,
                    Nombre = "Panqueques con dulce de leche",
                    TipoMercaderiaId = 10,
                    Precio = 800,
                    Ingredientes = "Harina, leche, huevos, sal, dulce de leche. ",
                    Preparacion = "Batir leche, huevo y harina con una pizca de sal. Cocinar en sartén caliente. Untar con dulce de leche y enrollar.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1Jmqg2GaZ0Whmq-yzxgtNOOBCi8k2dIwL"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 32,
                    Nombre = "Frutillas con Crema",
                    TipoMercaderiaId = 10,
                    Precio = 800,
                    Ingredientes = "Frutillas, crema batida, azúcar. ",
                    Preparacion = "Cortar las frutillas, agregar azúcar y dejar reposar. Servir con crema batida.",
                    Imagen = "http://drive.google.com/uc?export=view&id=1NX-GToSUk1pqMHpU6K0r453GlJOt3HH4"
                });

                //RELACION
                entity
                    .HasOne<TipoMercaderia>(mercaderia => mercaderia.TipoMercaderia)
                    .WithMany(tipoMercaderia => tipoMercaderia.Mercaderias)
                    .HasForeignKey(mercaderia => mercaderia.TipoMercaderiaId);
            });

            //COMANDA
            modelBuilder.Entity<Comanda>(entity =>
            {
                entity.ToTable("Comanda");
                entity.HasKey(c => c.ComandaId);
                entity.Property(c => c.Fecha).HasColumnType("date");

                //RELACION
                entity
                    .HasOne<FormaEntrega>(comanda => comanda.FormaEntrega)
                    .WithMany(formaEntrega => formaEntrega.Comandas)
                    .HasForeignKey(comanda => comanda.FormaEntregaId);
            });

            //COMANDAMERCADERIA
            modelBuilder.Entity<ComandaMercaderia>(entity =>
            {
                entity.ToTable("ComandaMercaderia");
                entity.HasKey(c => c.ComandaMercaderiaId);
                entity.Property(c => c.ComandaMercaderiaId).ValueGeneratedOnAdd();

                //RELACION
                entity
                    .HasOne<Comanda>(comandaMercaderia => comandaMercaderia.Comanda)
                    .WithMany(comanda => comanda.ComandaMercaderias)
                    .HasForeignKey(comandaMercaderia => comandaMercaderia.ComandaId);
                entity
                    .HasOne<Mercaderia>(comandaMercaderia => comandaMercaderia.Mercaderia)
                    .WithMany(comanda => comanda.ComandaMercaderias)
                    .HasForeignKey(comandaMercaderia => comandaMercaderia.MercaderiaId);
            });

        }
    }
}
