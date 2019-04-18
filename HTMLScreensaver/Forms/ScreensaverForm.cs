using Microsoft.Toolkit.Forms.UI.Controls;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HTMLScreensaver.Forms
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
            // Remove all borders and match the screen bounds so the form fills the screen
            FormBorderStyle = FormBorderStyle.None;

            // The form should appear above all others
            TopMost = true;

            SetBounds(screenNumber);

            AddControls(url);
        }

        /// <summary>
        /// Adds the controls to the screensaver form.
        /// </summary>
        /// <param name="url">The URL to show</param>
        private void AddControls(string url)
        {
            PreviewKeyDown += HandleCloseEvent;

            //Create a new fixed web browser
            var browser = new WebView()
            {
                Bounds = Bounds,
                Dock = DockStyle.Fill
            };

            ISupportInitialize initalisableBrowser = browser;

            initalisableBrowser.BeginInit();

            browser.IsScriptNotifyAllowed = true;

            browser.Click += HandleCloseEvent;
            browser.PreviewKeyDown += HandleCloseEvent;

            browser.DOMContentLoaded += HandleBrowserLoadEvent;
            browser.ScriptNotify += ScriptNotify;
            
            initalisableBrowser.EndInit();

            browser.Navigate(new Uri(url));

            //Add the controls to the form
            Controls.Add(browser);
        }

        private void ScriptNotify(object sender, WebViewControlScriptNotifyEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles close events on the web browser.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleCloseEvent(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Once the web page is loaded, event handlers are added to the page which will close the form.
        /// These will trigger when the user presses a key, moves the mouse, or clicks the pointer device.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private async void HandleBrowserLoadEvent(object sender, EventArgs e)
        {
            if (sender is WebView webView)
            {
                await webView.InvokeScriptAsync("eval", new string[]
                {
                    "document.addEventListener('keydown', function() { window.external.notify(''); });"
                    + "document.addEventListener('mouseover', function() { window.external.notify(''); });"
                    + "document.addEventListener('click', function() { window.external.notify(''); });"
                });
            }
        }

        /// <summary>
        /// Sets the bounds for the supplied screen number.
        /// </summary>
        /// <param name="screenNumber">The screen number.</param>
        private void SetBounds(int screenNumber)
        {
            var bounds = Screen.AllScreens[screenNumber].Bounds;

            using (var graphics = CreateGraphics())
            {
                bounds.Width = Convert.ToInt32(bounds.Width * graphics.DpiX / 96);
                bounds.Height = Convert.ToInt32(bounds.Height * graphics.DpiY / 96);
            }

            Bounds = bounds;
        }
    }
}