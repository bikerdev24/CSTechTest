using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTechTest.Entities;

namespace CSTechTest.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccount(CashCard cashCard);
        void UpdateAccount(Account account);
    }
}
