using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTechTest.Services.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public decimal Balance { get; set; }
        public decimal AmountRequested { get; set; }
    }
}
