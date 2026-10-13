using System;
using System.Data;
using Npgsql;

class Program {
    static void Main() {
        var connStr = "Host=localhost;Port=5432;Database=PharmaCoreDB;Username=postgres;Password=13579";
        try {
            using var conn = new NpgsqlConnection(connStr);
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Console.WriteLine(reader[0].ToString());
            }
        } catch (Exception ex) {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
