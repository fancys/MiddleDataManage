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
            IList<PurchaseOrder> puchaseOrderList = new List<PurchaseOrder>();
            for (int i = 0; i < 10000; i++)
            {
                PurchaseOrder puchaseOrder = new PurchaseOrder();
                puchaseOrder.CardCode = "C00001";
                puchaseOrder.CreateDate = DateTime.Now;
                puchaseOrder.Creator = Guid.NewGuid().ToString();
                puchaseOrder.BusinessType = "C01";
                puchaseOrder.Comments = "This is Test";
                puchaseOrder.OMSDocEntry = 1;
                puchaseOrder.OMSDocDate = DateTime.Now;
                puchaseOrder.UpDator = Guid.NewGuid().ToString();

                for (int j = 0; j < 3; j++)
                {
                    PurchaseOrderItem item = new PurchaseOrderItem();
                    item.OMSDocEntry = 1;
                    item.OMSLineNum = j + 1;
                    item.ItemCode = "1000001";
                    item.Quantity = 10;
                    item.Price = 20.45M;
                    puchaseOrder.PurchaseOrderItems.Add(item);
                }
                puchaseOrderList.Add(puchaseOrder);
            }
            foreach (var item in puchaseOrderList)
            {
                  await _puchaseOrderDapperRepositry.Save(item);
            }

           // Assert.AreEqual(rt.SaveCode, 0);
           //1W单 子表3行 子表集合保存 耗时19s
        }
    }
}
