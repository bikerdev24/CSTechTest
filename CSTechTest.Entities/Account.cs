using System.Collections.Generic;

namespace CSTechTest.Entities
{
    public class Account
    {
        public string Id { get; set; }
        public List<CashCard> CashCards { get; set; }
        public decimal Balance { get; set; }
    }
}
