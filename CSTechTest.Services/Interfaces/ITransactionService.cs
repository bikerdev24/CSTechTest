﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTechTest.Entities;

namespace CSTechTest.Services.Interfaces
{
    public interface ITransactionService
    {
        void WithDraw(CashCard cashCard, decimal amount);
        void TopUp(CashCard cashCard, decimal amount);
    }
}