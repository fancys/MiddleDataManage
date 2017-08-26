using IntegratedManagement.IRepository.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.Document;

namespace IntegratedManagement.Repository.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/16 14:23:32
	===============================================================================================================================*/
    public class SalesOrderRepository : ISalesOrderRepository
    {
        public async Task<List<SalesOrder>> Fetch(QueryParam param)
        {
            return new List<SalesOrder>(){
                #region MyRegion
                new SalesOrder() {
                        DocEntry=1,
                        OMSDocEntry = 1,
                        OMSDocDate = DateTime.Now,
                        CardCode="C10001",
                        OrderPaied = 12,
                        Freight = 1.4M,
                        PayMethod = "微信",
                        Comments = "包装严实",
                        PlatformCode = "C01",
                        DocType = "C01",
                        BusinessType ="CS",
                        SalesOrderItems = new List<SalesOrderItem>(){
                            #region MyRegion
                            new SalesOrderItem() {
                                DocEntry = 1,
                                OMSDocEntry = 1,
                                LineNum = 1,
                                OMSLineNum =1,
                                ItemCode = "100001",
                                Quantity = 10,
                                Price = 200.3M,
                                ItemPaied = 2000
                            }
                            #endregion
                        }
                    },
                #endregion
                
                #region MyRegion
                    new SalesOrder() {
                        DocEntry=1,
                        OMSDocEntry = 1,
                        OMSDocDate = DateTime.Now,
                        CardCode="C10002",
                        OrderPaied = 12,
                        Freight = 1.4M,
                        PayMethod = "微信",
                        Comments = "包装严实",
                        PlatformCode = "C02",
                        DocType = "C02",
                        BusinessType ="CS",
                        SalesOrderItems = new List<SalesOrderItem>(){
                            #region MyRegion
                            new SalesOrderItem() {
                                DocEntry = 1,
                                OMSDocEntry = 1,
                                LineNum = 1,
                                OMSLineNum =1,
                                ItemCode = "100002",
                                Quantity = 20,
                                Price = 40.3M,
                                ItemPaied = 4000
                            }
                            #endregion
                        }
                    }
                    #endregion
            };
        }
        public SalesOrder GetSalesOrder(int DocEntry)
        {
            return new SalesOrder() {
                        DocEntry=1,
                        OMSDocEntry = 1,
                        OMSDocDate = DateTime.Now,
                        CardCode="C10001",
                        OrderPaied = 12,
                        Freight = 1.4M,
                        PayMethod = "微信",
                        Comments = "包装严实",
                        PlatformCode = "C01",
                        DocType = "C01",
                        BusinessType ="CS",
                        SalesOrderItems = new List<SalesOrderItem>(){
                            new SalesOrderItem() {
                                DocEntry = 1,
                                OMSDocEntry = 1,
                                LineNum = 1,
                                OMSLineNum =1,
                                ItemCode = "100001",
                                Quantity = 10,
                                Price = 23.3M,
                                ItemPaied = 2000
                            }
                        }
                };
        }

        public Task<SaveResult> Save(SalesOrder SalesOrderList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateINSyncDataBatch(DocumentSync documentSyncData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateJESyncDataBatch(DocumentSync documentSyncData)
        {
            throw new NotImplementedException();
        }
    }
}
