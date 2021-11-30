using BusinessLogic.Classes.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Classes.Cryptography
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MD5CryptographyServiceTests
    {
        [DataRow("1234567890", "e807f1fcf82d132f9bb018ca6738a19f")]
        [DataRow("ABCDEFGHIJ", "e86410fa2d6e2634fd8ac5f4b3afe7f3")]
        [DataRow("12345ABCDE", "fc85a7ce091aea86ef3463b9166e9b06")]
        [DataRow("A1B2C3D4E4", "23d9fbff3779ee284004c22a16fee960")]
        [DataRow("abcdefghij", "a925576942e94b2ef57a066101b48876")]
        [DataRow("^%$-*^*_£&", "17c04f9fe92698077109382048883c3c")]
        [DataRow("ALongStringWithNoSpacesThatImJustUsingForTestPurposes", "f4e2f1c69c3840c9ab03bc4dfde5e392")]
        [DataRow("HTRH TWJ4ij jioy42io jihrH WRH6j iw2w 4 byw h^$ UJu46£%£u6", "b0f89957e8425e88212490986d9ec755")]
        [TestMethod]
        public void CheckExpectedHashValues(string source, string hash)
        {
            // Arrange
            var service = new MD5CryptographyService();

            // Act
            var result = service.GetHash(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual(result, hash);
        }
    }
}
