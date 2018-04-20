using HTMLScreensaver.Configuration;
using HTMLScreensaver.Forms;
using HTMLScreensaver.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //The mode is the first argument
            var modeArg = args.FirstOrDefault()?.Trim().ToLower();
            Mode mode = Mode.UNDEFINED;
            
            //If the argument is the minimum length
            if (modeArg?.Length >= 2)
            {
                //Extract the mode from the argument
                mode = (Mode)modeArg[1];
            }

            //If the supplied mode is invalid
            if (mode != Mode.SHOW)
            {
                //Exit the application
                Environment.Exit(Convert.ToInt32(ExitCodes.INVALID_ARGUMENT));
            }

            try
            {
                //Fix blur on high resolution screens
                SetProcessDPIAware();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Get the configuration for the application
                var config = ScreensaverSection.GetConfig();

                //Build a list of all the forms to display
                var forms = new List<ScreensaverForm>();

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
                        //Add the screensaver to the list of forms
                        forms.Add(new ScreensaverForm(number, url));
                    }
                }

                //Hide the cursor
                Cursor.Hide();

                //Start the screensaver forms
                Application.Run(new MultiFormContext(forms.ToArray()));

                //Exit the application
                Environment.Exit(Convert.ToInt32(ExitCodes.SUCCESS));
            }
            catch (Exception e)
            {
                //Build the log file path
                var path = Path.GetTempPath()
                    + nameof(HTMLScreensaver)
                    + $".{DateTime.Now.ToString("yyyyMMddTHHmmss")}.log";

                //Write the error to the log
                File.WriteAllText(path, e.ToString());

                //Build the error message
                var errorCaption = Resources.ErrorCaption;
                var errorText = Resources.ErrorText + Environment.NewLine + path;

                //Show the cursor again
                Cursor.Show();

                //Show the error message
                MessageBox.Show(errorText, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Exit the application
                Environment.Exit(Convert.ToInt32(ExitCodes.ERROR));
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}