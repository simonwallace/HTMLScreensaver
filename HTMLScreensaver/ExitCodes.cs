namespace HTMLScreensaver
{
    /// <summary>
    /// The available application exit codes.
    /// </summary>
    public enum ExitCodes
    {
        /// <summary>The supplied argument is invalid.</summary>
        INVALID_ARGUMENT = -1,

        /// <summary>The application exited as expected.</summary>
        SUCCESS = 0,

        /// <summary>The application exited due to an error.</summary>
        ERROR = 1
    }
}