using System;
using System.Collections.Generic;
using CSTechTest.Common;
using CSTechTest.Entities;
using CSTechTest.Repository.Exceptions;
using CSTechTest.Repository.Interfaces;
using CSTechTest.Services.Interfaces;
using log4net;

namespace CSTechTest.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private static readonly ILog log = LogeHelper.GetLogger();
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account GetAccount(CashCard cashCard)
        {
            log.Info($"Get account for cash card with id {cashCard.Id}");
            var account = _accountRepository.GetAccount(cashCard);
            return account ?? throw new NoDataFoundException<Account>();
        }

        public void UpdateAccount(Account account)
        {
            log.Info($"Updating account id {account.Id}");
            _accountRepository.UpdateAccount(account);
        }
    }
}
