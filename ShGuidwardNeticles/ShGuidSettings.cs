using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShGuidwardNeticles
{
    public class ShGuidSettings
    {
        #region Fields
        private char _replaceSlashWith;
        private char _replacePlusWith;
        private bool _trim;

        public char ReplaceSlashWith
        {
            get { return _replaceSlashWith; }
        }

        public char ReplacePlusWith
        {
            get { return _replacePlusWith; }
        }

        /// <summary>
        /// If true, the ShGuid is trimmed to 22 characters
        /// </summary>
        /// <remarks>
        /// We know that "==" will always be present at the end of the ShGuid. To save storage, we can trim these
        /// redundant characters
        /// <see href="https://stackoverflow.com/a/9279005"/>
        /// </remarks>
        /// 
        public bool Trim
        {
            get { return _trim; }
        }
        #endregion

        /// <summary>
        /// Constructs a settings object to replace '/' with '_', '-' with '-', and trims to 22 characters
        /// </summary>
        public ShGuidSettings()
        {
            _replaceSlashWith = '_';
            _replacePlusWith = '-';
            _trim = true;
        }

        public ShGuidSettings(char replaceSlashWith, char replacePlusWith, bool trim)
        {
            _replacePlusWith = replaceSlashWith;
            _replacePlusWith = replacePlusWith;
            _trim = trim;
        }

        public static ShGuidSettings CreateDefaultIfNull(ShGuidSettings settings)
        {
            if (settings == null)
                return new ShGuidSettings();

            return settings;
        }
    }
}
