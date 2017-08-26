using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity;
using IntegratedManagement.IRepository.PurchaseModule;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.Param;

namespace IntegratedManagement.Repository.PurchaseModule
{
    public class PurchaseOrderRepository
    {
        public List<PurchaseOrder> GetPurchaseOrder(QueryParam queryParam)
        {
            return new List<PurchaseOrder>() {
                #region MyRegion
                new PurchaseOrder() {
                    DocEntry=1,
                    OMSDocEntry=1,
                   BusinessType = "01",
                    CardCode="C10001",

                    PurchaseOrderItems =new List<PurchaseOrderItem>() {
                        #region MyRegion
                        new PurchaseOrderItem() {
                            DocEntry=1,
                            OMSDocEntry=1,
                            OMSLineNum=1,
                            LineNum=1,
                            ItemCode="100001",
                            Quantity=10,
                            Price=10
                        }
                        #endregion
                    }
                },
                #endregion
                #region MyRegion
                new PurchaseOrder() {
                    DocEntry=1,
                    OMSDocEntry=1,
                    OMSDocDate=DateTime.Now,
                    BusinessType="01",
                    CardCode="C10001",

                    PurchaseOrderItems =new List<PurchaseOrderItem>() {
                        #region MyRegion
                        new PurchaseOrderItem() {
                            DocEntry=1,
                            OMSDocEntry=1,
                            OMSLineNum=1,
                            LineNum=1,
                            ItemCode="100001",
                            Quantity=10,
                            Price=10
                        }
                        #endregion
                    }
                }
                #endregion
            };
        }

        public PurchaseOrder GetPurchaseOrder(int DocEntry)
        {
            return new PurchaseOrder()
            {
                #region MyRegion
                DocEntry = 1,
                OMSDocEntry = 1,
                OMSDocDate = DateTime.Now,
                BusinessType = "01",
                CardCode = "C10001",

                PurchaseOrderItems = new List<PurchaseOrderItem>() {
                    #region MyRegion
                    new PurchaseOrderItem()
                    {
                        DocEntry=1,
                        OMSDocEntry=1,
                        OMSLineNum=1,
                        LineNum=1,
                        ItemCode="100001",
                        Quantity=10,
                        Price=10
                    }
                    #endregion
                }
                #endregion
            };
        }

        public Task<SaveResult> Save(PurchaseOrder PurchaseOrder)
        {
            throw new NotImplementedException();
        }
    }
}



