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
	*	Create by Fancy at 2017/6/27 14:42:39
	===============================================================================================================================*/
    public class PurchaseReturnJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PurchaseReturnJob));
        public void Execute(IJobExecutionContext context)
        {
            logger.Info("PurchaseReturn Job  start  ...");
            //do something...

            Thread.Sleep(TimeSpan.FromSeconds(5));

            logger.Info("PurchaseReturn Job finished  ...");
        }
    }
}
