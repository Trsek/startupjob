using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace startupjob.DB
{
    public class _CommonDB
    {
        const string SQL_conn = @"Data Source=EMPY.db";

        public string table;
        public Dictionary<string, SqliteType> columns = new Dictionary<string, SqliteType>();
        public Dictionary<string, object> values = new Dictionary<string, object>();

        public new string ToString()
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

        public async Task<Int64> Save()
        {
            List<string> sql_column = new List<string>();
            List<string> sql_value = new List<string>();
            Int64 result = -1;

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

                    // get id
                    if (result != -1)
                    {
                        command.CommandText = @"select last_insert_rowid()";    //TODO multithread?
                        result = (Int64)command.ExecuteScalar();
                    }
                }
            }

            return result;
        }

        public void Load(int id)
        {
            using (var connection = new SqliteConnection(SQL_conn))
            {
                connection.Open();
                values.Clear();

                var command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM " + table + " WHERE id = $id";

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
