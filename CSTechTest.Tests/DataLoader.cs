using CSTechTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CSTechTest.Tests
{
    public class DataLoader
    {
        public static void LoadData()
        {
            //Load Cash Cards
            CashCard cashCard1 = new CashCard() { Id = "C1", Pin = "1236" };
            CashCard cashCard2 = new CashCard() { Id = "C2", Pin = "1234" };
            CashCard cashCard3 = new CashCard() { Id = "C3", Pin = "4223" };
            CashCard cashCard4 = new CashCard() { Id = "C4", Pin = "9955" };

            MemoryCache.Default.Add(new CacheItem(cashCard1.Id.ToString(), cashCard1), new CacheItemPolicy());
            MemoryCache.Default.Add(new CacheItem(cashCard2.Id.ToString(), cashCard2), new CacheItemPolicy());
            MemoryCache.Default.Add(new CacheItem(cashCard3.Id.ToString(), cashCard3), new CacheItemPolicy());
            MemoryCache.Default.Add(new CacheItem(cashCard4.Id.ToString(), cashCard4), new CacheItemPolicy());

            //Load Accounts
            Account account1 = new Account()
                { Id = "A1", Balance = 100, CashCards = new List<CashCard> { cashCard1, cashCard2 } };

            Account account2 = new Account()
                { Id = "A2", Balance = 250, CashCards = new List<CashCard> { cashCard2, cashCard3 } };

            MemoryCache.Default.Add(new CacheItem(account1.Id.ToString(), account1), new CacheItemPolicy());
            MemoryCache.Default.Add(new CacheItem(account2.Id.ToString(), account2), new CacheItemPolicy());
        }
    }
}
