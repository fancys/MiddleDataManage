﻿using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PaymentReceivedModule.PaymentReceived
{
    public class PaymentItem :  IDocumentItemBase
    {
        public int DocEntry { get; set; }

        public int LineNum
        {
            get;set;
        }
    }
}
