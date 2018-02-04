using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShGuidwardNeticles;

namespace ShGuidwardTests
{
    [TestClass]
    public class ShGuidTests
    {
        #region Constructor Tests
        [TestMethod]
        public void ShGuid_DefaultParameterlessConstructor_CheckEmptyShGuidDefaultSettings()
        {
            // Arrange            
            var expectedGuid = Guid.Empty;
            var expectedShortGuid = "AAAAAAAAAAAAAAAAAAAAAA";
            var expectedLength = 22; 

            // Act
            var shGuid = new ShGuid();

            // Assert            
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_DefaultSettingsConstructor_CheckTrim()
        {
            // Arrange
            var expectedGuid = Guid.Empty;
            var expectedShortGuid = "AAAAAAAAAAAAAAAAAAAAAA==";
            var expectedLength = 24;

            var settings = new ShGuidSettings('_', '-', false);
            // Act
            var shGuid = new ShGuid(settings);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_GuidConstructor_CheckInitializedGuidDefaultSettings()
        {
            // Arrange
            var stringifiedGuid = "e9ab78e3-3645-4605-a161-01709994c1bb";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedShortGuid = "43ir6UU2BUahYQFwmZTBuw";
            var expectedLength = 22;

            // Act
            var shGuid = new ShGuid(expectedGuid);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_GuidConstructor_CheckInitializedGuidAndReplaceSettings()
        {
            // Arrange
            var stringifiedGuid = "cdaed56d-8712-414d-b346-01905d0026fe";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedShortGuid = "bdWuzRKHTUGzRgGQXQAm_g";
            var expectedLength = 22;            

            // Act
            var shGuid = new ShGuid(expectedGuid);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_StringifiedGuidConstructor_CheckInitializedGuidDefaultSettings()
        {
            // Arrange
            var stringifiedGuid = "e9ab78e3-3645-4605-a161-01709994c1bb";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedShortGuid = "43ir6UU2BUahYQFwmZTBuw";
            var expectedLength = 22;

            // Act
            var shGuid = new ShGuid(stringifiedGuid);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_StringifiedGuidConstructor_CheckInitializedGuidAndReplaceSettings()
        {
            // Arrange
            var stringifiedGuid = "cdaed56d-8712-414d-b346-01905d0026fe";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedShortGuid = "bdWuzRKHTUGzRgGQXQAm_g";
            var expectedLength = 22;

            // Act
            var shGuid = new ShGuid(stringifiedGuid);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ShGuid_StringifiedGuidConstructor_ThrowsFormatException()
        {
            // Arrange
            var testString = "how's the unibrow?";

            // Act
            var shGuid = new ShGuid(testString);

            // Throws
        }
        #endregion

        #region Parse/Decode Tests
        [TestMethod]
        public void ShGuid_TryParse_CheckShGuidDefaultSettings()
        {
            // Arrange
            var stringifiedGuid = "0058c2d5-806d-465c-8560-313db6c054c0";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedShortGuid = "1cJYAG2AXEaFYDE9tsBUwA";
            var expectedLength = 22;

            ShGuid actualShGuid;

            // Act
            var actualResult = ShGuid.TryParse(stringifiedGuid, out actualShGuid);

            // Assert
            Assert.IsTrue(actualResult);
            Assert.AreEqual<Guid>(expectedGuid, actualShGuid.Guid);
            Assert.AreEqual(expectedShortGuid, actualShGuid.ShortGuid);
            Assert.AreEqual(expectedLength, actualShGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_TryParse_NegativeParse()
        {
            // Arrange
            var stringifiedGuid = "dogs >= cats";

            ShGuid actualShGuid;

            // Act
            var actualResult = ShGuid.TryParse(stringifiedGuid, out actualShGuid);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void ShGuid_Decode_CheckShGuidDefaultSettings()
        {
            // todo finish this and write a test for a negative decode
            // Arrange
            var expectedShortGuid = "1cJYAG2AXEaFYDE9tsBUwA";
            var stringifiedGuid = "0058c2d5-806d-465c-8560-313db6c054c0";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedLength = 22;

            // Act
            var actualShGuid = ShGuid.Decode(expectedShortGuid);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, actualShGuid.Guid);
            Assert.AreEqual(expectedShortGuid, actualShGuid.ShortGuid);
            Assert.AreEqual(expectedLength, actualShGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_Decode_CheckInitializedGuidAndReplaceSettings()
        {
            // Arrange
            var expectedShortGuid = "bdWuzRKHTUGzRgGQXQAm_g";
            var stringifiedGuid = "cdaed56d-8712-414d-b346-01905d0026fe";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedLength = 22;

            // Act
            var shGuid = new ShGuid(expectedGuid);

            // Assert
            Assert.AreEqual<Guid>(expectedGuid, shGuid.Guid);
            Assert.AreEqual(expectedShortGuid, shGuid.ShortGuid);
            Assert.AreEqual(expectedLength, shGuid.ShortGuid.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ShGuid_Decode_ThrowsFormatException()
        {
            // Arrange
            var testString = "lupethefiasco.blogspot.com congratulations ladies, the next one's for you";

            // Act
            var shGuid = ShGuid.Decode(testString);

            // Throws
        }

        [TestMethod]
        public void ShGuid_TryDecode_CheckInitializedDefaultSettings()
        {
            // Arrange
            var expectedShortGuid = "1cJYAG2AXEaFYDE9tsBUwA";
            var stringifiedGuid = "0058c2d5-806d-465c-8560-313db6c054c0";
            var expectedGuid = new Guid(stringifiedGuid);
            var expectedLength = 22;

            ShGuid actualShGuid;

            // Act
            var actualResult = ShGuid.TryDecode(expectedShortGuid, out actualShGuid);

            // Assert
            Assert.IsTrue(actualResult);
            Assert.AreEqual<Guid>(expectedGuid, actualShGuid.Guid);
            Assert.AreEqual(expectedShortGuid, actualShGuid.ShortGuid);
            Assert.AreEqual(expectedLength, actualShGuid.ShortGuid.Length);
        }

        [TestMethod]
        public void ShGuid_TryDecode_NegativeDecode()
        {
            // Arrange
            var expectedShortGuid = ":0 ~yawn";

            ShGuid actualShGuid;

            // Act
            var actualResult = ShGuid.TryDecode(expectedShortGuid, out actualShGuid);

            // Assert
            Assert.IsFalse(actualResult);
        }
        #endregion
    }
}
