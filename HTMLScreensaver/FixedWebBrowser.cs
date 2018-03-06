using System.Windows.Forms;

namespace HTMLScreensaver
{
    /// <summary>
    /// A web browser control that cannot be interacted with.
    /// </summary>
    public class FixedWebBrowser : WebBrowser
    {
        /// <summary>
        /// Creates a new fixed web browser.
        /// </summary>
        public FixedWebBrowser()
        {
            Initialise();
        }


        /// <summary>
        /// Initialises the fixed web browser.
        /// </summary>
        private void Initialise()
        {
            AllowNavigation = false;
            AllowWebBrowserDrop = false;
            IsWebBrowserContextMenuEnabled = false;
            ScriptErrorsSuppressed = true;
            ScrollBarsEnabled = false;
            WebBrowserShortcutsEnabled = false;
        }
    }
}