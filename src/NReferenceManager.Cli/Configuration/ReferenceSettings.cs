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
    public class ReferenceSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ReferenceDto> References { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ReferenceSettings()
        {
            this.References = [];
        }
    }
}