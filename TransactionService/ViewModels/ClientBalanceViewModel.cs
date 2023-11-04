﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.ViewModels
{
    public record ClientBalanceViewModel : IViewModel
    {
        public decimal Available { get; set; }
        public decimal WaitingFunds { get; set; }
    }
}
