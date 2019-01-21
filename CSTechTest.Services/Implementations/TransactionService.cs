using System;
using CSTechTest.Common;
using CSTechTest.Entities;
using CSTechTest.Services.Exceptions;
using CSTechTest.Services.Interfaces;
using log4net;

namespace CSTechTest.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        //Class wide static lock to ensure that in case of multiple transaction service instances
        //access to an account is made available to only one thread at a time.
        private static readonly object accountLock = new object();
        private static readonly ILog log = LogeHelper.GetLogger();

        private readonly IAccountService _accountService;

        public TransactionService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Withdraw amount from an account.
        /// </summary>
        /// <param name="account">Amount to be withdrawn</param>
        /// <param name="amount">Account to be withdrawn from.</param>
        /// 
        public void WithDraw(CashCard cashCard, decimal amount)
        {
            if (amount < 0)
            {
                log.Error($"Negative withdrawal of amount {amount} attempted by cash card id {cashCard.Id}");
                throw new ArgumentException("Amount to withdraw cannot be negative");
            }

            if (amount == 0) //Nothing to withdraw
                return;

            lock (accountLock)
            {
                //Get account for a cash card
                var account = _accountService.GetAccount(cashCard);
                
                if (account.Balance >= amount)
                {
                    account.Balance = account.Balance - amount;
                }
                else //If balance is less than amount requested for withdrawal then raise InsufficientBalanceException
                {
                    log.Info($"Insufficient balance when withdrawing amount {amount} from cash card with id {cashCard.Id}");
                    throw new InsufficientBalanceException() {Balance = account.Balance, AmountRequested = amount};
                }
                                
                _accountService.UpdateAccount(account);
            }            
        }

        public void TopUp(CashCard cashCard, decimal amount)
        {
            if (amount < 0)
            {
                log.Error($"Negative topup of amount {amount} attempted by cash card id {cashCard.Id}");
                throw new ArgumentException("Amount to topup cannot be negative");
            }

            if (amount == 0) //Nothing to topup
                return;

            lock (accountLock)
            {
                //Get account for a cash card
                var account = _accountService.GetAccount(cashCard);

                //Top up account balance
                account.Balance = account.Balance + amount;

                _accountService.UpdateAccount(account);
            }            
        }
    }
}
