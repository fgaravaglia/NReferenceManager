using NReferenceManager.Cli.Configuration;

namespace NReferenceManager.Cli
{
    /// <summary>
    /// 
    /// </summary>
    public class ReferenceManager
    {
        #region Fields
        bool _IsInitialized;
        readonly Settings _Settings;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized => this._IsInitialized;

        #endregion
        public ReferenceManager()
        {
            this._IsInitialized = false;
            this._Settings = new Settings();
        }
        /// <summary>
        /// Parses the arguments
        /// </summary>
        /// <param name="arguments"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ParseArguments(IEnumerable<string> arguments)
        {
            try
            {
                this._IsInitialized = false;
                if (arguments.Count() != 1)
                    throw new ArgumentException("No input provided", nameof(arguments));

                foreach (var argument in arguments)
                {
                    var parts = argument.Split('=');
                    switch (parts[0].ToUpper())
                    {
                        case "-R":
                            this._Settings.RepositoryRootPath = parts[1].Trim();
                            break;
                        default:
                            throw new NotImplementedException($"Option '{parts[0]}' unknown");
                    }
                }
                if (String.IsNullOrEmpty(this._Settings.RepositoryRootPath))
                    throw new ArgumentException("Root Repository Folder cannot be null", nameof(arguments));
                this._IsInitialized = true;
            }
            catch (ArgumentException argEx)
            {
                LogError(argEx, "Invalid Argument");
                this._IsInitialized = false;
            }
            catch (Exception ex)
            {
                LogError(ex, "Unexpected error during argument parsing");
                this._IsInitialized = false;
            }
        }

        public int Run()
        {
            try
            {
                if (!this._IsInitialized)
                    return -2;

                // read settings
                var referenceSettings = new JsonReferenceSettingsProvider(Path.Combine(this._Settings.RepositoryRootPath, "packages.centralized.json")).Get();
                if (referenceSettings.References.Count == 0)
                {
                    LogWarning("No Reference found to be forced");
                    return 0;
                }

                // getting csProj
                var projFiles = GetCsProjList().ToList();
                LogInformation($"Found {projFiles.Count()} to be forced");

                // Updating
                foreach (var proj in projFiles)
                {
                    var counter = projFiles.IndexOf(proj) + 1;
                    LogInformation($"Start processing {proj.FullName} [{counter}/{projFiles.Count}]");
                    foreach (var reference in referenceSettings.References)
                    {
                        LogInformation($" > Setting {reference.PackageName} to Version {reference.PackageVersion}...");
                        ProjectFileUpdater.UpdatePackageReferenceVersion(reference, proj);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogError(ex, "Unexpected error during execution");
                return -1;
            }

        }

        #region Private Methods

        IEnumerable<FileInfo> GetCsProjList()
        {
            var files = ProjectFileFinder.GetCsprojFiles(this._Settings.RepositoryRootPath);
            return files.Select(x => new FileInfo(x));
        }

        void LogWarning(string message)
        {
            Console.WriteLine($"[WARN] {message}");
        }
        void LogInformation(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        void LogError(Exception ex, string message)
        {
            ArgumentNullException.ThrowIfNull(ex);
            ArgumentNullException.ThrowIfNullOrEmpty(message);

            Console.WriteLine("********************************************************");
            Console.WriteLine(message);
            Console.WriteLine(" Message: " + ex.Message);
            Console.WriteLine(" Stack Trace:");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine("********************************************************");
        }
        #endregion
    }
}