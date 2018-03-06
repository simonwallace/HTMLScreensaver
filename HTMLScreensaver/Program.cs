using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HTMLScreensaver
{
    /// <summary>
    /// The HTML Screensaver program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            //Fix blur on high resolution screens
            SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Start the application on all available screens
            for (int i = Screen.AllScreens.GetLowerBound(0); i <= Screen.AllScreens.GetUpperBound(0); i++)
            {
                Application.Run(new ScreensaverForm(i));
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}