using System.Configuration;

namespace HTMLScreensaver.Configuration
{
    /// <summary>
    /// The screen section configuration.
    /// </summary>
    public class ScreenSection : ConfigurationSection
    {
        /// <summary>
        /// Returns the screen number. This is the key field and is required.
        /// </summary>
        [ConfigurationProperty("number", IsKey = true, IsRequired = true)]
        public int? Number
        {
            get
            {
                return this["number"] as int?;
            }
        }

        /// <summary>
        /// Returns the screen URL. This is required.
        /// </summary>
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return this["url"] as string;
            }
        }
    }
}