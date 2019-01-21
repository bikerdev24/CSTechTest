using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTechTest.Services.Contracts.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public List<CashCard> CashCards { get; set; }
        public decimal Balance { get; set; }
    }
}
