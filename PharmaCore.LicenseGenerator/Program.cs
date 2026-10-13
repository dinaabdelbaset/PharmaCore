using PharmaCore.ActivationManager;

namespace PharmaCore.LicenseGenerator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // First-time setup: ensure DB connection is configured
            if (!ControlDbConnection.IsConfigured)
            {
                Home.ReconfigureDbConnection();
                if (!ControlDbConnection.IsConfigured)
                {
                    MessageBox.Show("لا يمكن بدء البرنامج بدون ضبط اتصال قاعدة البيانات.", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Application.Run(new Home());
        }
    }
}