using System;
using static System.Console;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
            string user = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
            string pwd = Environment.GetEnvironmentVariable("DB_PWD") ?? "P@ssw0rd!123";
            string _sql =
                Environment.GetEnvironmentVariable("DB_QUERY")
                ?? @"
                      select
                          json_query((
                              select
                                  *
                              from (
                                  values
                                      (session_user, user_name(), suser_name())
                                  ) T
                                      ([session_user], [user_name], [suser_name])
                              for json auto
                              )) as userData
                      from
                          (values(1)) t(c) for json auto, without_array_wrapper
                  ";

            SqlConnection conn = new SqlConnection(
                $"Server={server}; UID={user}; PWD={pwd}; trusted_connection=false;Persist Security Info=False;Encrypt=False"
            );
            conn.Open();
            Console.WriteLine(
                $"Connected to SQL Server v{conn.ServerVersion} from {Environment.OSVersion.VersionString}"
            );
            SqlCommand cmd = new SqlCommand(_sql, conn);
            var qr = cmd.ExecuteScalar();
            Console.WriteLine(JsonDocument.Parse(qr.ToString()!).RootElement);
            conn.Close();
        }
    }
}
