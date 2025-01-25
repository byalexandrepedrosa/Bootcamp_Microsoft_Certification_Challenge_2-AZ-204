using NUnit.Framework;
using System;

namespace httpValidateCpf.Tests
{
    [TestFixture]
    public class ValidateCpfTests
    {
        [Test]
        public void ValidateCpf_EmptyString_ReturnsFalse()
        {
            // Arrange
            string cpf = "";

            // Act
            bool result = azfuncvalidacpf.ValidateCpf(cpf);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateCpf_NullString_ReturnsFalse()
        {
            // Arrange
            string ?cpf = null;

            // Act
            bool result = azfuncvalidacpf.ValidateCpf(cpf);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateCpf_InvalidCpf_ReturnsFalse()
        {
            // Arrange
            string cpf = "1234567890";

            // Act
            bool result = azfuncvalidacpf.ValidateCpf(cpf);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateCpf_ValidCpf_ReturnsTrue()
        {
            // Arrange
            string cpf = "12345678901";

            // Act
            bool result = azfuncvalidacpf.ValidateCpf(cpf);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateCpf_RepeatedDigits_ReturnsFalse()
        {
            // Arrange
            string cpf = "11111111111";

            // Act
            bool result = azfuncvalidacpf.ValidateCpf(cpf);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateCpf_CheckDigits_ReturnsTrue()
        {
            // Arrange
            string cpf = "12345678901";

            // Act
            bool result = azfuncvalidacpf.ValidateCpf(cpf);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
