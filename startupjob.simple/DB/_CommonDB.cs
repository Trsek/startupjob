using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace startupjob.DB
{
    public class _CommonDB
    {
        const string SQL_conn = @"Data Source=EMPY.db";

        public string table;
        public Dictionary<string, SqliteType> columns = new Dictionary<string, SqliteType>();
        public Dictionary<string, object> values = new Dictionary<string, object>();

        public string ToString()
        {
            string line = string.Empty;

            foreach (var value in values)
                line += value.Key + "=" + value.Value + ", ";

            if (values.Count == 0)
                foreach (var value in columns)
                    line += value.Key + "[" + value.Value + "], ";

            return line;
        }

        public bool Update(string name, string value)
        {
            if (!columns.ContainsKey(name))
                return false;

            // check for intenger
            if (columns[name] == SqliteType.Integer)
            {
                int result = 0;
                if (!int.TryParse(value, out result))
                    return false;
            }

            // check for double
            if (columns[name] == SqliteType.Real)
            {
                double result = 0;
                if (!double.TryParse(value, out result))
                    return false;
            }

            // store
            values[name] = value;
            return true;
        }

        public bool Save()
        {
            List<string> sql_column = new List<string>();
            List<string> sql_value = new List<string>();
            int result = 0;

            if (values.Count > 0)
            {
                using (var connection = new SqliteConnection(SQL_conn))
                {
                    connection.Open();

                    var command = connection.CreateCommand();

                    foreach (var value in values)
                    {
                        sql_column.Add(value.Key);
                        sql_value.Add("$" + value.Key);
                    }

                    command.CommandText = @"INSERT INTO " + table
                            + " (" + string.Join(',', sql_column) + ")"
                            + " VALUES(" + string.Join(',', sql_value) + ")";

                    foreach (var value in values)
                        command.Parameters.AddWithValue("$" + value.Key, value.Value);

                    result = command.ExecuteNonQuery();
                }
            }
            
            return (result == 1);
        }

        public void Load(int id)
        {
            using (var connection = new SqliteConnection(SQL_conn))
            {
                connection.Open();
                values.Clear();

                var command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM " + table;

                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreach (var column in columns)
                        {
                            values.Add(column.Key, reader[column.Key]);
                        }
                    }
                }
            }
        }
    }
}
