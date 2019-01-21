using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTechTest.Entities;
using CSTechTest.Repository.Interfaces;
using CSTechTest.Services.Implementations;
using CSTechTest.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace CSTechTest.Tests.UnitTests
{
    [TestFixture]
    public class CashCardServiceTests
    {
        [Test]
        public void IsPinValid_ValidPin_ReturnsTrue()
        {
            //Arrange
            var mockCashCardRepository = new Mock<ICashCardRepository>();
            CashCard cashCard = new CashCard() {Id = "2", Pin = "1234"};

            mockCashCardRepository
                .Setup(x => x.GetPin(cashCard.Id))
                .Returns(cashCard.Pin);

            ICashCardService cashCardService = new CashCardService(mockCashCardRepository.Object);

            //Act
            var isPinValid = cashCardService.IsPinValid(cashCard);

            //Assert
            Assert.IsTrue(isPinValid);
        }

        [Test]
        public void IsPinValid_InvalidPin_ReturnsFalse()
        {
            //Arrange
            var mockCashCardRepository = new Mock<ICashCardRepository>();
            CashCard cashCard = new CashCard() { Id = "2", Pin = "1234" };

            mockCashCardRepository
                .Setup(x => x.GetPin(cashCard.Id))
                .Returns("DifferentPinInRepo");

            ICashCardService cashCardService = new CashCardService(mockCashCardRepository.Object);

            //Act
            var isPinValid = cashCardService.IsPinValid(cashCard);

            //Assert
            Assert.IsFalse(isPinValid);
        }

        [Test]
        public void IsPinValid_NoAccountFound_ReturnsFalse()
        {
            //Arrange
            var mockCashCardRepository = new Mock<ICashCardRepository>();
            CashCard cashCard = new CashCard() { Id = "2", Pin = "1234" };
            String returnedPin = null;

            mockCashCardRepository
                .Setup(x => x.GetPin(cashCard.Id))
                .Returns(returnedPin);

            ICashCardService cashCardService = new CashCardService(mockCashCardRepository.Object);

            //Act
            var isPinValid = cashCardService.IsPinValid(cashCard);

            //Assert
            Assert.IsFalse(isPinValid);
        }
    }
}
