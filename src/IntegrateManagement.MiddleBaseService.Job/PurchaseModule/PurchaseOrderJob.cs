using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.PurchaseModule;
using IntegratedManagement.RepositoryDapper.PurchaseModule;
using IntegratedManageMent.Application.PurchaseModule;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.Job.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/27 14:42:00
	===============================================================================================================================*/
    public class PurchaseOrderJob : IJob
    {
        private readonly IPurchaseOrderApp purchaseOrderApp;
        //private readonly IPurchaseOrderRepository puchaseOrderRepository = new PurchaseOrderDapperRepository();
        private static readonly ILog logger = LogManager.GetLogger(typeof(PurchaseOrderJob));


        public PurchaseOrderJob(IPurchaseOrderApp PurchaseOrderApp)
        {
            purchaseOrderApp = PurchaseOrderApp;
        }

        //public PurchaseOrderJob()
        //{
        //    purchaseOrderApp = new PurchaseOrderApp(puchaseOrderRepository);
        //}

        public async void Execute(IJobExecutionContext context)
        {
            logger.Info("PurchaseOrder Job  start  ...");
            
            //do something...
            QueryParam queryParam = new QueryParam();
            queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 20;
            var orderList = await purchaseOrderApp.GetPurchaseOrderAsync(queryParam);
            if (orderList.Count == 0)
                return;
            Thread.Sleep(TimeSpan.FromSeconds(5));

            logger.Info("PurchaseOrder Job finished  ...");
        }


        private async void HandPurchaseOrder()
        {
            QueryParam queryParam = new QueryParam();
            queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 20;
            var orderList = await purchaseOrderApp.GetPurchaseOrderAsync(queryParam);
            if (orderList.Count == 0)
                return;
        }
    }
}
