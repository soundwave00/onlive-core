using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace onlive_core.Db
{
    public class IDatabase
    {
        public EntityDatabaseConfig DbConfig { get; set; }
        private MySqlConnection connection;

		public IDatabase()
        {
			DatabaseConfig databaseConfig = new DatabaseConfig();
            DbConfig = databaseConfig.GetSqlConnConfig();
        }

        public IDatabase(EntityDatabaseConfig _DbConfig)
        {
            DbConfig = _DbConfig;
        }

        public MySqlConnection Open()
        {
            if (connection == null)
				connection = new MySqlConnection(this.DbConfig.ConnectionString);
            
			if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        public void Close()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
		
        public int ExecuteNonQuery(MySqlCommand command, MySqlTransaction transaction = null)
        {
            int returnValue = -1;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

			/*
            using (SqlConnection connection = this.Open())
            {
                if (!connection.State.Equals(ConnectionState.Open))
                {
                    throw new Exception(String.Format("ExecuteNonQuery SqlCommand: ", command.CommandText));
                }
                command.Connection = connection;

                returnValue = command.ExecuteNonQuery();
                
				connection.Close();
            }
			*/

			if (!connection.State.Equals(ConnectionState.Open))
			{
				throw new Exception(String.Format("ExecuteNonQuery SqlCommand: ", command.CommandText));
			}
			command.Connection = connection;
			returnValue = command.ExecuteNonQuery();

            return returnValue;
        }

        public MySqlDataReader ExecuteReader(MySqlCommand command, MySqlTransaction transaction = null)
        {
            MySqlDataReader dr = null;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.Connection = connection;
            if (!command.Connection.State.Equals(ConnectionState.Open))
            {
                throw new Exception(String.Format("ExecuteReader SqlCommand: ", command.CommandText));
            }
            dr = command.ExecuteReader();

            return dr;
        }
    }
}
