using System;
using System.Linq;
using System.Runtime.Caching;
using CSTechTest.Common;
using CSTechTest.Entities;
using CSTechTest.Repository.Interfaces;
using log4net;

namespace CSTechTest.Repository.Implementations
{
    public class CashCardRepository : ICashCardRepository
    {
        private static readonly ILog log = LogeHelper.GetLogger();

        public string GetPin(string cashCardId)
        {
            var account =
                MemoryCache.Default.SingleOrDefault(x => (x.Value is CashCard) &&
                                                    (x.Value as CashCard).Id.Equals(cashCardId, StringComparison.InvariantCultureIgnoreCase)).Value as CashCard;
            return account?.Pin;            
        }
    }
}
