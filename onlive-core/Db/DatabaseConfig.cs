using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.IO;
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

        public void AddParameter(MySqlCommand command, string name, MySqlDbType type, Object value)
        {
            if (command == null)
                throw new Exception("Command is null");

            if (String.IsNullOrEmpty(name))
                throw new Exception("Param name is null");

            if (value == null)
                throw new Exception("Param value is null");

            String parameterName = name.Trim();

            command.Parameters.Add(parameterName, type);
            command.Parameters[parameterName].Value = value;
		}

        public void AddArrayParameters<T>(MySqlCommand command, string name, MySqlDbType type, IEnumerable<T> values)
        {
            if (command == null)
                throw new Exception("Command is null");

            if (String.IsNullOrEmpty(name))
                throw new Exception("Param name is null");

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            List<string> parametersName = new List<string>();
            int count = 0;

			name = name.Trim();

            foreach (var value in values)
            {
                var parameterName = name + count;
                parametersName.Add(parameterName);

                MySqlParameter parameter = new MySqlParameter(parameterName, type);
            	parameter.Value = value;
                
                command.Parameters.Add(parameter);
                parameters.Add(parameter);

				count++;
            }

            command.CommandText = command.CommandText.Replace(name, string.Join(",", parametersName));
        }
    }
}
