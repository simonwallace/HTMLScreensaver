using System.Configuration;

namespace HTMLScreensaver.Settings
{
    /// <summary>
    /// The root configuration class for the application.
    /// </summary>
    public class ScreensaverSection : ConfigurationSection
    {
        /// <summary>
        /// Returns the configuration setting from the app config file.
        /// </summary>
        /// <returns>The screensaver section.</returns>
        public static ScreensaverSection GetConfig()
        {
            return ConfigurationManager.GetSection("screensaver") as ScreensaverSection
                ?? new ScreensaverSection();
        }

        /// <summary>
        /// Returns the screens element collection from the screensaver section.
        /// </summary>
        [ConfigurationProperty("screens")]
        [ConfigurationCollection(typeof(ScreenElementCollection), AddItemName = "screen")]
        public ScreenElementCollection Screens
        {
            get
            {
                return this["screens"] as ScreenElementCollection;
            }
        }
    }
}