using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTechTest.Repository.Interfaces;
using CSTechTest.Services.Interfaces;
using NUnit.Framework;
using Moq;
using CSTechTest.Services.Implementations;
using CSTechTest.Entities;
using CSTechTest.Repository.Exceptions;

namespace CSTechTest.Tests.UnitTests
{
    [TestFixture]
    public class AccountServiceTests
    {
        [Test]
        public void GetAccount_WhenInvalidCashCard_ThrowsNoDataFoundException()
        {
            //Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            Account accountToReturn = null;

            mockAccountRepository
                .Setup(x => x.GetAccount(It.IsAny<CashCard>()))
                .Returns(accountToReturn);

            IAccountService accountService = new AccountService(mockAccountRepository.Object);

            //Act
            //Assert
            Assert.Throws<NoDataFoundException<Account>>(() => accountService.GetAccount(new CashCard()));                        
        }
    }
}
