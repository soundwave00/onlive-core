using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

namespace onlive_core.Db
{
    public class EntityDatabaseConfig
    {
        public string ConnectionPrefix { get; set; }
        public string ConnectionString { get; set; }
    }

    public class DatabaseConfig
    {
		private IConfiguration Configuration { get; set; }

        public EntityDatabaseConfig GetSqlConnConfig()
        {
            EntityDatabaseConfig configuredDb = new EntityDatabaseConfig();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

			configuredDb.ConnectionString = Configuration.GetConnectionString("DBCONFIG");
			configuredDb.ConnectionPrefix = Configuration.GetConnectionString("DBPREFIX");

			return configuredDb;
		}

        public void AddParameterToCommand(MySqlCommand command, string name, MySqlDbType type, Object value)
        {
            if (command == null)
                throw new Exception("Il command non puo' essere null");

            if (String.IsNullOrEmpty(name))
                throw new Exception("Il valore di " + value.ToString() + " non puo' essere null o una stringa vuota");

            String parameterName = name.Trim();

            command.Parameters.Add(parameterName, type);
            command.Parameters[parameterName].Value = value;
		}
    }
}
