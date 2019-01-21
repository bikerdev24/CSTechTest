using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CSTechTest.Entities;
using CSTechTest.Services;
using CSTechTest.Services.Interfaces;

namespace CSTechTest.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Initialising application");
            Initialise();

            var container = CSTechTest.Services.ContainerBuilder.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var atmService1 = scope.Resolve<IATMService>();
                var atmService2 = scope.Resolve<IATMService>();
                var atmService3 = scope.Resolve<IATMService>();
                var atmService4 = scope.Resolve<IATMService>();

                System.Console.WriteLine("Validating Pin");

                var isPinValid = atmService1.IsPinValid(MemoryCache.Default.Get("C1") as CashCard);

                System.Console.WriteLine($"Is Pin Valid - {isPinValid}");

                System.Console.WriteLine("Running multiuple withdrawals and topups");

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => atmService1.WithDraw(MemoryCache.Default.Get("C1") as CashCard, 20)));
                tasks.Add(Task.Run(() => atmService1.TopUp(MemoryCache.Default.Get("C1") as CashCard, 30)));
                tasks.Add(Task.Run(() => atmService2.TopUp(MemoryCache.Default.Get("C1") as CashCard, 20)));
                tasks.Add(Task.Run(() => atmService3.WithDraw(MemoryCache.Default.Get("C1") as CashCard, 20)));
                tasks.Add(Task.Run(() => atmService4.TopUp(MemoryCache.Default.Get("C1") as CashCard, 20)));

                Task.WaitAll(tasks.ToArray());

                var balance = atmService1.GetBalance(MemoryCache.Default.Get("C1") as CashCard);

                System.Console.WriteLine($"After multiple simultaneous topups and withdrawals Balance should be 130 and is {balance}");
                System.Console.WriteLine("Press any key to exit");
                System.Console.ReadLine();
            }            
        }

        private static void Initialise()
        {
            log4net.Config.XmlConfigurator.Configure();
            LoadData();
        }

        #region LoadData
        private static void LoadData()
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
        #endregion        
    }
}
