using System;
using System.Linq;
using System.Runtime.Caching;
using CSTechTest.Common;
using CSTechTest.Entities;
using CSTechTest.Repository.Exceptions;
using CSTechTest.Repository.Interfaces;
using log4net;

namespace CSTechTest.Repository.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private static readonly ILog log = LogeHelper.GetLogger();

        public Account GetAccount(CashCard cashCard)
        {
            var account =
                MemoryCache.Default.SingleOrDefault(x => (x.Value is Account) &&
                                                (x.Value as Account).CashCards.Exists(y => y.Id.Equals(cashCard.Id, StringComparison.InvariantCultureIgnoreCase))).Value as Account;

            return account;
        }

        public void UpdateAccount(Account account)
        {
            MemoryCache.Default.Remove(account.Id);
            MemoryCache.Default.Add(new CacheItem(account.Id.ToString(), account), new CacheItemPolicy());
        }
    }
}
