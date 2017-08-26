using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegratedManagement.IRepository.SalesModule;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using System.Threading.Tasks;
using System.Collections.Generic;
using IntegratedManagement.RepositoryDapper.SalesModule;

namespace IntegratedManagement.MiddleBaseAPI.Tests.DapperRepository.SalesModule
{
    [TestClass]
    public class SalesOrderUTest
    {

        private readonly ISalesOrderRepository _ISalesOrderRepository;
        int n = 0;
        private List<SalesOrder> GetSalesOrderList()
        {
            return new List<SalesOrder>() {
                new SalesOrder() {
                    OMSDocEntry = 1+n,
                    OMSDocDate = DateTime.Now,
                    CardCode = "10001",
                    BusinessType = "10",
                    Comments = "test",
                    DocType = "C01",
                    PlatformCode = "001",
                    OrderPaied = 1000.13M,
                    Freight = 10.23M,
                    PayMethod = "02",
                    FrghtVendor = "test v",
                    SalesOrderItems = new List<SalesOrderItem>()
                    {
                        new SalesOrderItem() { ItemCode="10001",OMSDocEntry=1+n,OMSLineNum=1,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10002",OMSDocEntry=1+n,OMSLineNum=2,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10003",OMSDocEntry=1+n,OMSLineNum=3,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10004",OMSDocEntry=1+n,OMSLineNum=4,ItemPaied=1990,Price=12.12M,Quantity=20 },
                    }
                },
                new SalesOrder() {
                    OMSDocEntry = 2+n,
                    OMSDocDate = DateTime.Now,
                    CardCode = "10002",
                    BusinessType = "10",
                    Comments = "test",
                    DocType = "C01",
                    PlatformCode = "001",
                    OrderPaied = 1000.13M,
                    Freight = 10.23M,
                    PayMethod = "02",
                    FrghtVendor = "test v",
                    SalesOrderItems = new List<SalesOrderItem>()
                    {
                        new SalesOrderItem() { ItemCode="10011",OMSDocEntry=2+n,OMSLineNum=1,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10012",OMSDocEntry=2+n,OMSLineNum=2,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10013",OMSDocEntry=2+n,OMSLineNum=3,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10014",OMSDocEntry=2+n,OMSLineNum=4,ItemPaied=1990,Price=12.12M,Quantity=20 },
                    }
                },
                 new SalesOrder() {
                    OMSDocEntry = 3+n,
                    OMSDocDate = DateTime.Now,
                    CardCode = "10002",
                    BusinessType = "10",
                    Comments = "test",
                    DocType = "C01",
                    PlatformCode = "001",
                    OrderPaied = 1000.13M,
                    Freight = 10.23M,
                    PayMethod = "02",
                    FrghtVendor = "test v",
                    SalesOrderItems = new List<SalesOrderItem>()
                    {
                        new SalesOrderItem() { ItemCode="10021",OMSDocEntry=3+n,OMSLineNum=1,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10022",OMSDocEntry=3+n,OMSLineNum=2,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10023",OMSDocEntry=3+n,OMSLineNum=3,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10024",OMSDocEntry=3+n,OMSLineNum=4,ItemPaied=1990,Price=12.12M,Quantity=20 },
                    }
                },
                 new SalesOrder() {
                    OMSDocEntry = 4+n,
                    OMSDocDate = DateTime.Now,
                    CardCode = "10002",
                    BusinessType = "10",
                    Comments = "test",
                    DocType = "C01",
                    PlatformCode = "001",
                    OrderPaied = 1000.13M,
                    Freight = 10.23M,
                    PayMethod = "02",
                    FrghtVendor = "test v",
                    SalesOrderItems = new List<SalesOrderItem>()
                    {
                        new SalesOrderItem() { ItemCode="10031",OMSDocEntry=4+n,OMSLineNum=1,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10032",OMSDocEntry=4+n,OMSLineNum=2,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10033",OMSDocEntry=4+n,OMSLineNum=3,ItemPaied=1990,Price=12.12M,Quantity=20 },
                        new SalesOrderItem() { ItemCode="10034",OMSDocEntry=4+n,OMSLineNum=4,ItemPaied=1990,Price=12.12M,Quantity=20 },
                    }
                }
            };
        }

        public SalesOrderUTest( )
        {
            _ISalesOrderRepository = new SalesOrderDapperRepository();
        }

        [TestMethod]
        public async Task TestSaveSalesOrder()
        {

            var orderList = GetSalesOrderList();
            foreach (var order in orderList)
            {
                var saveRt = await _ISalesOrderRepository.Save(order);
                Assert.AreEqual(saveRt.Code, 0);
            }
           
        }
    }
}
