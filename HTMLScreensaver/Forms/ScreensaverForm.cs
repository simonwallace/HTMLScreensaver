using System;
using System.Windows.Forms;

namespace HTMLScreensaver.Forms
{
    /// <summary>
    /// A form that can be used as a screensaver.
    /// </summary>
    public class ScreensaverForm : Form
    {
        /// <summary>Records whether the form has loaded or not.</summary>
        private bool? loaded;

        /// <summary>
        /// Creates a new screensaver form on the supplied screen number.
        /// </summary>
        /// <param name="screenNumber">The screen to create the screensaver form on.</param>
        /// <param name="url">The URL to show.</param>
        public ScreensaverForm(int screenNumber, string url)
        {
            Initialise(screenNumber, url);
        }

        /// <summary>
        /// Initialises the screensaver form.
        /// </summary>
        /// <param name="screenNumber">The screen to initialise the screensaver form on.</param>
        /// <param name="url">The URL to show.</param>
        private void Initialise(int screenNumber, string url)
        {
            //Remove all borders and match the screen bounds so the form fills the screen
            FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.AllScreens[screenNumber].Bounds;

            //Add the content to the form
            AddControls(url);
            AddEventHandlers();
        }

        /// <summary>
        /// Adds the controls to the screensaver form.
        /// </summary>
        /// <param name="url">The URL to show</param>
        private void AddControls(string url)
        {
            //Clear any existing controls
            Controls.Clear();

            //Create a new fixed web browser
            var browser = new FixedWebBrowser()
            {
                Bounds = Bounds,
                Url = new Uri(url)
            };

            browser.DocumentCompleted += HandleBrowserLoadEvent;
            browser.PreviewKeyDown += HandleBrowserKeyEvent;

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
        private void HandleBrowserKeyEvent(object sender, PreviewKeyDownEventArgs e)
        {
            //Close the form
            Close();
        }

        /// <summary>
        /// Handles load events on the web browser.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleBrowserLoadEvent(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //If the sender is a web browser
            if (sender is WebBrowser)
            {
                var webBrowser = sender as WebBrowser;

                //Add the mouse event handlers
                webBrowser.Document.MouseDown += HandleBrowserMouseDownEvent;
                webBrowser.Document.MouseMove += HandleBrowserMouseMoveEvent;
            }
        }

        /// <summary>
        /// Handles mouse down events on the web browser.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleBrowserMouseDownEvent(object sender, HtmlElementEventArgs e)
        {
            //Close the form
            Close();
        }

        /// <summary>
        /// Handles mouse move events on the web browser.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleBrowserMouseMoveEvent(object sender, HtmlElementEventArgs e)
        {
            //Ignore the first trigger which occurs when the browser has loaded
            if (!loaded.HasValue)
            {
                loaded = false;
            }
            //Ignore the second trigger which occurs when the cursor first appears
            else if (!loaded.Value)
            {
                loaded = true;
            }
            //Close the form the next time the cursor moves
            else
            {
                Close();
            }
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