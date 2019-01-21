using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTechTest.Common;
using CSTechTest.Entities;
using CSTechTest.Services.Interfaces;
using log4net;

namespace CSTechTest.Services.Implementations
{
    /// <summary>
    /// The ATM service is meant to be a sort of "Orchestration" service that mimics the interface of an ATM and in turn uses other services such as the
    /// CashCard Service to validate the pin and the Transaction Service to perform WithDrawal and TopUp actions.
    /// This service would be used by a client like a console or web interface.
    /// </summary>
    public class ATMService : IATMService
    {
        private static readonly ILog log = LogeHelper.GetLogger();

        private readonly ICashCardService _cashCardService;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        
        public ATMService(ICashCardService cashCardService, ITransactionService transactionService, IAccountService accountService)
        {
            _cashCardService = cashCardService;
            _transactionService = transactionService;
            _accountService = accountService;
        }

        public bool IsPinValid(CashCard cashCard)
        {
            log.Info($"Validating pin for cash card with id {cashCard.Id}");
            return _cashCardService.IsPinValid(cashCard);
        }

        /// <summary>
        /// Tops up account linked to cash card with requested amount.
        /// </summary>
        /// <param name="cashCard"></param>
        /// <param name="amount"></param>
        public void TopUp(CashCard cashCard, decimal amount)
        {
            log.Info($"Topping up cash card with id {cashCard.Id} by amount {amount}");
            _transactionService.TopUp(cashCard, amount);
        }

        /// <summary>
        /// Attempts to withdraw requested amount from account linked to cash card.
        /// </summary>
        /// <param name="cashCard"></param>
        /// <param name="amount"></param>
        /// <exception cref="CSTechTest.Services.Exceptions.InsufficientBalanceException">Thrown if balance amount is less than amount requested</exception>
        public void WithDraw(CashCard cashCard, decimal amount)
        {
            log.Info($"Withdrawing amount {amount} from cash card with id {cashCard.Id}");
            _transactionService.WithDraw(cashCard, amount);
        }

        /// <summary>
        /// Gets Account balance for given cash card linked to an account.
        /// </summary>
        /// <param name="cashCard"></param>
        /// <returns></returns>
        /// <exception cref="CSTechTest.Repository.Exceptions.NoDataFoundException"></exception>
        public decimal GetBalance(CashCard cashCard)
        {
            log.Info($"Getting account balance from cash card with id {cashCard.Id}");
            var account = _accountService.GetAccount(cashCard);
            return account.Balance;
        }
    }
}
