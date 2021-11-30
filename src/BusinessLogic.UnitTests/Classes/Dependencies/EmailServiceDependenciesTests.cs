using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Smtp;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = BusinessLogic.Classes.Dependencies.EmailServiceDependencies;

namespace BusinessLogic.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmailServiceDependenciesTests
    {
        private readonly Mock<IEmailFactory> _mockEmailFactory = new Mock<IEmailFactory>();
        private readonly Mock<IRepository<EmailLog>> _mockEmailLogRepository = new Mock<IRepository<EmailLog>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransactionServiceIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockEmailFactory.Object,
                    _mockEmailLogRepository.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenTranscationServiceIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockEmailFactory.Object,
                    _mockEmailLogRepository.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transactionService");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEmailFactoryIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   null,
                   _mockEmailLogRepository.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEmailFactoryIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   null,
                   _mockEmailLogRepository.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: emailFactory");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEmailLogRepositoryIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockEmailFactory.Object,
                   null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEmailLogRepositoryIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockEmailFactory.Object,
                   null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: emailLogRepository");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                _mockEmailFactory.Object,
                _mockEmailLogRepository.Object);

            Assert.IsNotNull(dependencies.EmailFactory);
            Assert.IsNotNull(dependencies.EmailLogRepository);
        }
    }
}
