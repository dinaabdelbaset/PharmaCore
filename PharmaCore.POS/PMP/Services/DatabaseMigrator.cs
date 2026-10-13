using System;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    internal static class DatabaseMigrator
    {
        public static async Task RunAsync(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) return;
            try
            {
                await BranchAuthDb.EnsureSchemaAsync(connectionString);
                // Future: add other EnsureSchema calls here
            }
            catch (Exception ex)
            {
                // Log locally; in real scenario send to audit endpoint
                System.IO.File.AppendAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pharmacore_migration.log"), DateTime.UtcNow+" | MIGRATION ERROR: "+ex+Environment.NewLine);
            }
        }
    }
}