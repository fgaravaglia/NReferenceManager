namespace NReferenceManager.Cli.Configuration
{
    /// <summary>
    /// abstraction for component to get settings for references
    /// </summary>ab
    public interface IReferenceSettingsProvider
    {
        /// <summary>
        /// Gets the data
        /// </summary>
        /// <returns></returns>
        ReferenceSettings Get();
    }
}