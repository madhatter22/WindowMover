using System;
using System.ComponentModel;
using System.Windows.Forms;
using WindowMover.ViewModels;
using WindowMover.Views;

namespace WindowMover
{
    static class Program
    {
        private static MainView _view;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += ApplicationThreadException;
            _view = new MainView(new MainViewModel());

            Application.Run();
        }

        private static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string header = e.Exception is Win32Exception ? "Error Moving Window" : "Error";
            MessageBox.Show(e.Exception.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
