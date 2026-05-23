using IMBaby.Helpers;
using System.Windows.Forms;

namespace IMBaby
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Initialize / ensure database tables exist
            try
            {
                DatabaseHelper.EnsureDatabase();
            }
            catch (Exception ex)
            {
                // Allow user to configure connection string if DB not found
                MessageBox.Show(
                    $"Could not connect to SQL Server.\n\n{ex.Message}\n\n" +
                    "Default connection: localhost (IMBabyDB)\n" +
                    "You can still run the app; edit DatabaseHelper.cs to update the connection string.",
                    "Database Setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.Run(new Form1());
        }
    }
}
