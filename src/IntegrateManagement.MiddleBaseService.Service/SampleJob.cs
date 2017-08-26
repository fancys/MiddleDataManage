using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.Service
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/26 17:43:04
	===============================================================================================================================*/
    public class SampleJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SampleJob));
        public void Execute(IJobExecutionContext context)
        {
            logger.Info("SampleJob running...");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            logger.Info("SampleJob run finished.");
        }
    }
}
