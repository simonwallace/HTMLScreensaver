using HTMLScreensaver.Settings;
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

            //Get the configuration for the application
            var config = ScreensaverSection.GetConfig();

            //Start the application on all available screens
            for (var i = 0; i < config.Screens.Count; i++)
            {
                //Get the screen configuration
                var screen = config.Screens[i];

                //The screen number array is zero-based
                var number = screen.Number.Value - 1;

                //The URL to display
                var url = screen.Url;

                //If the screen number is valid
                if (number >= Screen.AllScreens.GetLowerBound(0)
                    && number <= Screen.AllScreens.GetUpperBound(0))
                {
                    //Display the screensaver
                    Application.Run(new ScreensaverForm(number, url));
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}