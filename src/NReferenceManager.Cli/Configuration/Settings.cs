using System.Diagnostics.CodeAnalysis;

namespace NReferenceManager.Cli.Configuration
{
    /// <summary>
    /// Settings of the application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Settings
    {
        /// <summary>
        /// ss
        /// </summary>
        public string RepositoryRootPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Settings()
        {
            this.RepositoryRootPath = "";
        }
    }
}