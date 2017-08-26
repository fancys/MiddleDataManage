using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.Job.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/27 14:39:03
	===============================================================================================================================*/
    public class SalesReturnJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SalesReturnJob));

        public void Execute(IJobExecutionContext context)
        {
            logger.Info("salesreturn job start  ...");
            //do something...

            Thread.Sleep(TimeSpan.FromSeconds(100));

            logger.Info("salesreturn job finished  ...");
        }
    }
}
