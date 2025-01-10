using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NReferenceManager.Cli
{
    public class ProjectFileFinder
    {
        public static List<string> GetCsprojFiles(string directoryPath)
        {
            List<string> csprojFiles = new List<string>();
            FindCsprojFiles(directoryPath, csprojFiles); return csprojFiles;
        }

        private static void FindCsprojFiles(string directoryPath, List<string> csprojFiles)
        {
            try
            {
                foreach (string file in Directory.GetFiles(directoryPath, "*.csproj"))
                {
                    csprojFiles.Add(file);
                }

                foreach (string directory in Directory.GetDirectories(directoryPath))
                {
                    FindCsprojFiles(directory, csprojFiles);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "Unexpected error during Search of csproj files");
            }
        }

    }
}