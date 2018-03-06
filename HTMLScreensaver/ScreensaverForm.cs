using System;
using System.Windows.Forms;

namespace HTMLScreensaver
{
    /// <summary>
    /// A form that can be used as a screensaver.
    /// </summary>
    public class ScreensaverForm : Form
    {
        /// <summary>
        /// Creates a new screensaver form on the supplied screen number.
        /// </summary>
        /// <param name="screenNumber">The screen to create the screensaver form on.</param>
        public ScreensaverForm(int screenNumber)
        {
            Initialise(screenNumber);
        }

        /// <summary>
        /// Initialises the screensaver form.
        /// </summary>
        /// <param name="screenNumber">The screen to initialise the screensaver form on.</param>
        private void Initialise(int screenNumber)
        {
            //Remove all borders and match the screen bounds so the form fills the screen
            FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.AllScreens[screenNumber].Bounds;

            //Add the content to the form
            AddControls();
            AddEventHandlers();
        }

        /// <summary>
        /// Adds the controls to the screensaver form.
        /// </summary>
        private void AddControls()
        {
            //Clear any existing controls
            Controls.Clear();

            //Create a new fixed web browser
            var browser = new FixedWebBrowser()
            {
                Bounds = Bounds,
                Url = new Uri("https://www.google.co.uk")
            };

            browser.PreviewKeyDown += HandleKeyEvent;

            //Add the controls to the form
            Controls.Add(browser);
        }

        /// <summary>
        /// Adds the event handlers to the screensaver form.
        /// </summary>
        private void AddEventHandlers()
        {
            //Re-add the event handlers
            Load -= HandleLoadEvent;
            Load += HandleLoadEvent;
        }

        /// <summary>
        /// Handles key events on the web browser.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleKeyEvent(object sender, PreviewKeyDownEventArgs e)
        {
            //Close the form
            Close();
        }

        /// <summary>
        /// Handles load events on the screensaver form.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleLoadEvent(object sender, EventArgs e)
        {
            //Force the form to appear above all other forms
            TopMost = true;

            //Hide the cursor
            Cursor.Hide();
        }
    }
}