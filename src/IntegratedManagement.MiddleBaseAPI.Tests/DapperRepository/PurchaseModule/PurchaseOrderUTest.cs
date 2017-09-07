using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegratedManagement.RepositoryDapper.PurchaseModule;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace IntegratedManagement.MiddleBaseAPI.Tests.DapperRepository.PurchaseModule
{
    [TestClass]
    public class PurchaseOrderUTest
    {
        PurchaseOrderDapperRepository _puchaseOrderDapperRepositry;
        public PurchaseOrderUTest()
        {
            _puchaseOrderDapperRepositry = new PurchaseOrderDapperRepository();
        }
        [TestMethod]
        public async Task TestSavePurchaseOrder()
        {
         
        }
    }
}
