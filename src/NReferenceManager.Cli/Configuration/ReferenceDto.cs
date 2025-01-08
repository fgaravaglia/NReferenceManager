using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NReferenceManager.Cli.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ReferenceDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PackageVersion { get; set; }

        public ReferenceDto()
        {
            this.PackageName = "";
            this.PackageVersion = "";
        }
    }
}