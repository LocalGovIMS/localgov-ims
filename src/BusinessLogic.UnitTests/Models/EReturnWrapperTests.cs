using BusinessLogic.Enums;
using BusinessLogic.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Models
{
    [TestClass]
    public class EReturnWrapperTests
    {
        private EReturnWrapper _eReturnWrapper;

        public EReturnWrapperTests()
        {
            _eReturnWrapper = new EReturnWrapper()
            {
                EReturn = new Entities.EReturn()
                {
                    EReturnCashes = new List<Entities.EReturnCash>
                    {
                        new Entities.EReturnCash() { Total = 1 },
                        new Entities.EReturnCash() { Total = 2 },
                        new Entities.EReturnCash() { Total = 3 }
                    },
                    EReturnCheques = new List<Entities.EReturnCheque>
                    {
                        new Entities.EReturnCheque() { Amount = 5 },
                        new Entities.EReturnCheque() { Amount = 10 },
                        new Entities.EReturnCheque() { Amount = 15 }
                    }
                }
            };
        }

        [TestMethod]
        public void Amount_returns_sum_of_cash_totals_for_cash_EReturn_type()
        {
            // Arrange
            _eReturnWrapper.EReturn.TypeId = (int)EReturnType.Cash;

            // Act
            var amount = _eReturnWrapper.Amount;

            // Assert
            amount.Should().Be(_eReturnWrapper.EReturn.EReturnCashes.Sum(x => x.Total));

        }

        [TestMethod]
        public void Amount_returns_sum_of_cheque_amounts_for_cheque_EReturn_type()
        {
            // Arrange
            _eReturnWrapper.EReturn.TypeId = (int)EReturnType.Cheque;

            // Act
            var amount = _eReturnWrapper.Amount;

            // Assert
            amount.Should().Be(_eReturnWrapper.EReturn.EReturnCheques.Sum(x => x.Amount));
        }

        [TestMethod]
        public void Amount_returns_zero_for_unknown_EReturn_type()
        {
            // Arrange

            // Act
            var amount = _eReturnWrapper.Amount;

            // Assert
            amount.Should().Be(0);
        }
    }
}
