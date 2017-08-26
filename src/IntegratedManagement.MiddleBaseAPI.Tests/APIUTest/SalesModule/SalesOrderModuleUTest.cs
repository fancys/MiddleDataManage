using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManageMent.Application.SalesModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace IntegratedManagement.MiddleBaseAPI.Tests.APIUTest.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/7/14 14:23:15
	===============================================================================================================================*/
    [TestClass]
    public class SalesOrderModuleUTest
    {
        public async Task<List<SalesOrder>> GetSalesOrders()
        {
         return  await  Task.Run(()=> {
               return new List<SalesOrder>()
               {
                   new SalesOrder() {
                       DocEntry = 1,OMSDocDate = DateTime.Now,
                       SalesOrderItems = new List<SalesOrderItem>() {
                           new SalesOrderItem() {
                               DocEntry = 1,LineNum = 1,ItemCode = "10001"
                           },
                           new SalesOrderItem() {
                               DocEntry = 1,LineNum = 2,ItemCode = "10002"
                           }
                       }
                   }
               };
            });
        }

        [TestMethod]
        public async Task GetSalesOrder()
        {
            //var mockRep = new Mock<ISalesOrderApp>();
            //mockRep.Setup(x => x.GetSalesOrderAsync(null)).Returns(GetSalesOrders());

            //var controller = new SalesOrdersController(mockRep.Object);
            //var rt = await controller.GetSalesOrders(null) as JsonResult<Result<SalesOrder>>;
            
            //Assert.IsNotNull(rt);
            //Assert.AreEqual(0, rt.Content.Code);
        }

    }
}
