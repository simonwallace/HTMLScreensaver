using System.Configuration;

namespace HTMLScreensaver.Settings
{
    /// <summary>
    /// The screen element collection configuration.
    /// </summary>
    public class ScreenElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Returns the screen section at the supplied index.
        /// </summary>
        /// <param name="index">The array index.</param>
        /// <returns>The screen section element.</returns>
        public ScreenSection this[int index]
        {
            get
            {
                return BaseGet(index) as ScreenSection;
            }
        }

        /// <summary>
        /// Creates a new screen section element.
        /// </summary>
        /// <returns>The new screen section element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ScreenSection();
        }

        /// <summary>
        /// Gets the key of the supplied screen section element.
        /// </summary>
        /// <param name="element">The screen section element.</param>
        /// <returns>The key of the screen section element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ScreenSection).Number;
        }
    }
}