﻿using Npgsql;

namespace TP2_REST_Scholz_Veronica.Helpers {

    public class ConnectionHelpers {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            databaseUrl = "postgres://db_restaurant_uvry_user:cPx0CE9uj18eD0jAyOCMYtIYkXY3CQMX@dpg-cltjkd5a73kc73bhcb3g-a.oregon-postgres.render.com:5432/db_restaurant_uvry";

            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        //build the connection string from the environment. i.e. Heroku
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            return builder.ToString();

        }

    }
}