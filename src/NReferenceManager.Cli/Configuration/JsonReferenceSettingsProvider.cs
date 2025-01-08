using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NReferenceManager.Cli.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonReferenceSettingsProvider : IReferenceSettingsProvider
    {
        #region  Fields
        readonly string _FileFullPath;
        readonly object _Locker = new object();
        JsonSerializerOptions _Options;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        public JsonReferenceSettingsProvider(string fullName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(fullName);
            this._FileFullPath = fullName;
            this._Options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ReferenceSettings Get()
        {
            if (!File.Exists(this._FileFullPath))
                return new ReferenceSettings();

            var jsonContent = "";
            lock (this._Locker)
            {
                jsonContent = File.ReadAllText(this._FileFullPath);
            }
            return JsonSerializer.Deserialize<ReferenceSettings>(jsonContent, this._Options) ?? new ReferenceSettings();
        }
    }
}