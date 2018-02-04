using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShGuidwardNeticles
{
    /// <summary>
    /// Immutable Guid and it's base64 encoded counterpart
    /// </summary>
    /// <remarks>
    /// http://www.singular.co.nz/2007/12/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp/
    /// </remarks>
    public class ShGuid
    {
        #region Properties/Fields
        private readonly Guid _guid;
        private readonly string _shortGuid;

        public Guid Guid
        {
            get { return _guid; }
        }

        public string ShortGuid
        {
            get { return _shortGuid; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty ShGuid
        /// </summary>
        public ShGuid(ShGuidSettings settings = null)
        {
            _guid = Guid.Empty;
            _shortGuid = Encode(_guid, settings);
        }

        /// <summary>
        /// Creates a ShGuid
        /// </summary>
        /// <param name="guid">A guid to initialize ShGuid with</param>
        /// <param name="settings"><see cref="ShGuidSettings"/></param>
        public ShGuid(Guid guid, ShGuidSettings settings = null)
        {
            _guid = guid;
            _shortGuid = Encode(guid, settings);
        }

        /// <summary>
        /// Creates a ShGuid with a stringified guid
        /// </summary>
        /// <param name="stringifiedGuid">A stringified guid</param>
        /// <param name="settings"><see cref="ShGuidSettings"/> </param>
        public ShGuid(string stringifiedGuid, ShGuidSettings settings = null)
        {
            _guid = new Guid(stringifiedGuid);
            _shortGuid = Encode(_guid, settings);
        }
        #endregion

        #region Create
        public static ShGuid NewShGuid(ShGuidSettings settings = null)
        {
            var guid = Guid.NewGuid();
            return new ShGuid(guid, settings);
        }
        #endregion
        
        #region Parse
        /// <summary>
        /// Try to parse a Guid and create a ShGuid
        /// </summary>
        /// <param name="stringifiedGuid">A stringified guid</param>
        /// <param name="shGuid">An initialized ShGuid</param>
        /// <param name="settings"><see cref="ShGuidSettings"/> </param>
        /// <returns><c>true</c> if the initialization was successful</returns>
        public static bool TryParse(string stringifiedGuid, out ShGuid shGuid, ShGuidSettings settings = null)
        {
            shGuid = null;
            Guid guid;
            if (Guid.TryParse(stringifiedGuid, out guid))
            {
                shGuid = new ShGuid(guid, settings);
                return true;
            }

            return false;
        }
        #endregion

        #region Decode
        /// <summary>
        /// Decodes a ShGuid
        /// </summary>
        /// <param name="encodedShGuid"></param>
        /// <param name="settings"><see cref="ShGuidSettings"/> </param>
        /// <returns>A ShGuid</returns>
        public static ShGuid Decode(string encodedShGuid, ShGuidSettings settings = null)
        {
            settings = ShGuidSettings.CreateDefaultIfNull(settings);

            byte[] buffer = DecodeToByteArray(encodedShGuid, settings);
            var decodedGuid = new Guid(buffer);
            var shGuid = new ShGuid(decodedGuid, settings);

            return shGuid;
        }

        /// <summary>
        /// Try decode an encoded ShGuid and create a ShGuid
        /// </summary>
        /// <param name="encodedShGuid">An encoded ShGuid</param>
        /// <param name="shGuid">An initialized ShGuid</param>
        /// <param name="settings"><see cref="ShGuidSettings"/></param>
        /// <returns><c>True</c> if the initialization was successful</returns>
        public static bool TryDecode(string encodedShGuid, out ShGuid shGuid, ShGuidSettings settings = null)
        {
            shGuid = null;
            settings = ShGuidSettings.CreateDefaultIfNull(settings);

            Guid guid;
            try
            {
                var buffer = DecodeToByteArray(encodedShGuid, settings);
                var stringifiedGuid = Encoding.UTF8.GetString(buffer);

                guid = new Guid(buffer);
                shGuid = new ShGuid(guid, settings);
                return true;
            }
            catch(FormatException)
            {
                return false;
            }           
        }        
        #endregion

        #region Private Methods
        #region Encode
        /// <summary>
        /// Encode a stringified guid by converting the guid to byte array, then converting ToBase64String
        /// </summary>
        /// <param name="stringifiedGuid">A strinigified guid</param>
        /// /// <param name="settings"><see cref="ShGuidSettings"/> </param>
        /// <returns>A stringified ShGuid</returns>
        private string Encode(string stringifiedGuid, ShGuidSettings settings = null)
        {
            var guid = new Guid(stringifiedGuid);

            var shguid = Encode(guid, settings);
            return shguid;
        }
        
        /// <summary>
        /// Encode a guid by converting the guid to byte array, the converting ToBase64String
        /// </summary>
        /// <param name="guid"><see cref="Guid"/> </param>
        /// <param name="settings"><see cref="ShGuidSettings"/> </param>
        /// <returns></returns>
        private string Encode(Guid guid, ShGuidSettings settings = null)
        {
            settings = ShGuidSettings.CreateDefaultIfNull(settings);

            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace('/', settings.ReplaceSlashWith)
                .Replace('+', settings.ReplacePlusWith);

            if (settings.Trim)
                return encoded.Substring(0, 22);

            return encoded;
        }
        #endregion

        #region Decode
        /// <summary>
        /// Decodes a ShGuid
        /// </summary>
        /// <param name="encodedShGuid">An encoded ShGuid</param>
        /// <param name="settings"><see cref="ShGuidSettings"/></param>
        /// <returns></returns>
        private static byte[] DecodeToByteArray(string encodedShGuid, ShGuidSettings settings)
        {
            var replaced = encodedShGuid
                .Replace(settings.ReplaceSlashWith, '/')
                .Replace(settings.ReplacePlusWith, '+');
            return Convert.FromBase64String(replaced + "==");
        }
        #endregion
        #endregion

    }
}
