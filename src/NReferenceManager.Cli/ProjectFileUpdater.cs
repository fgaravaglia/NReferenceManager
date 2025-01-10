using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NReferenceManager.Cli.Configuration;

namespace NReferenceManager.Cli
{
    /// <summary>
    /// Component responsible for updateing CSPROJ
    /// </summary>
    internal class ProjectFileUpdater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="csProjFile"></param>
        public static void UpdatePackageReferenceVersion(ReferenceDto reference, FileInfo csProjFile)
        {
            try
            {
                XDocument csprojDocument = XDocument.Load(csProjFile.FullName);
                var packageReferences = csprojDocument.Descendants("PackageReference");
                foreach (var packageReference in packageReferences)
                {
                    var versionAttribute = packageReference.Attribute("Version");
                    var packageName = packageReference.Attribute("Include")?.Value ?? "";
                    if (packageName.Equals(reference.PackageName, StringComparison.InvariantCultureIgnoreCase) && versionAttribute != null)
                        versionAttribute.Value = reference.PackageVersion;
                }
                csprojDocument.Save(csProjFile.FullName);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "Unexpected error during update of " + csProjFile?.Name);
            }
        }
    }
}