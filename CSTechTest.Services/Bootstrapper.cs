using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CSTechTest.Repository.Implementations;
using CSTechTest.Repository.Interfaces;
using CSTechTest.Services.Implementations;
using CSTechTest.Services.Interfaces;

namespace CSTechTest.Services
{
    public class ContainerBuilder
    {
        public static IContainer Configure()
        {
            var builder = new Autofac.ContainerBuilder();
            
            builder.RegisterType<CashCardRepository>().As<ICashCardRepository>();
            builder.RegisterType<CashCardService>().As<ICashCardService>();

            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            
            builder.RegisterType<TransactionService>().As<ITransactionService>();
            builder.RegisterType<ATMService>().As<IATMService>();
            
            return builder.Build();
        }
    }
}
