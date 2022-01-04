using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.SuspenseNote
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetTests : BaseSuspenseNoteTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.SuspenseNotes.Find(It.IsAny<Expression<Func<Entities.SuspenseNote, bool>>>()))
                .Returns(new List<Entities.SuspenseNote>());
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAll(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.SuspenseNote>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAll(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetAll(1);

            // Assert
            Assert.IsNull(result);
        }
    }
}
