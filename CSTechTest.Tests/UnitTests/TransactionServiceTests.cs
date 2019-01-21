using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTechTest.Entities;
using CSTechTest.Repository.Interfaces;
using CSTechTest.Services.Exceptions;
using CSTechTest.Services.Implementations;
using CSTechTest.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace CSTechTest.Tests.UnitTests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        [Test]
        public void WithDraw_SufficientBalance_AccountBalanceUpdated()
        {
            //Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            Account accountToReturn = new Account(){Balance=100, Id="123"};

            mockAccountRepository
                .Setup(x => x.GetAccount(It.IsAny<CashCard>()))
                .Returns(accountToReturn);

            IAccountService accountService = new AccountService(mockAccountRepository.Object);
            ITransactionService transactionService = new TransactionService(accountService);

            //Act
            transactionService.WithDraw(new CashCard(), 10);

            //Assert
            mockAccountRepository
                .Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Once());
        }

        [Test]
        public void WithDraw_InsufficientBalance_ThrowsInsufficientBalanceException()
        {
            //Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            Account accountToReturn = new Account() { Balance = 100, Id = "123" };

            mockAccountRepository
                .Setup(x => x.GetAccount(It.IsAny<CashCard>()))
                .Returns(accountToReturn);

            IAccountService accountService = new AccountService(mockAccountRepository.Object);
            ITransactionService transactionService = new TransactionService(accountService);

            //Act            
            //Assert
            Assert.Throws<InsufficientBalanceException>(() => transactionService.WithDraw(new CashCard(), 110));
        }

        [Test]
        public void WithDraw_NegativeAmount_ThrowsArgumentException()
        {
            //Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            Account accountToReturn = new Account() { Balance = 100, Id = "123" };

            mockAccountRepository
                .Setup(x => x.GetAccount(It.IsAny<CashCard>()))
                .Returns(accountToReturn);

            IAccountService accountService = new AccountService(mockAccountRepository.Object);
            ITransactionService transactionService = new TransactionService(accountService);

            //Act            
            //Assert
            Assert.Throws<ArgumentException>(() => transactionService.WithDraw(new CashCard(), -50));
        }

        [Test]
        public void TopUp_NegativeAmount_ThrowsArgumentException()
        {
            //Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            Account accountToReturn = new Account() { Balance = 100, Id = "123" };

            mockAccountRepository
                .Setup(x => x.GetAccount(It.IsAny<CashCard>()))
                .Returns(accountToReturn);

            IAccountService accountService = new AccountService(mockAccountRepository.Object);
            ITransactionService transactionService = new TransactionService(accountService);

            //Act            
            //Assert
            Assert.Throws<ArgumentException>(() => transactionService.TopUp(new CashCard(), -50));
        }
    }
}
