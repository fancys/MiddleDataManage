using IntegratedManagement.IRepository.SalesModule;
using IntegratedManagement.RepositoryDapper.SalesModule;
using IntegratedManageMent.Application.SalesModule;
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
	*	Create by Fancy at 2017/6/26 14:53:28
	===============================================================================================================================*/
    public class SalesOrderJob : IJob
    {
        private readonly ISalesOrderApp salesOrderApp;
        private readonly ISalesOrderRepository salesOrderRepository = new SalesOrderDapperRepository();
        private static readonly ILog logger = LogManager.GetLogger(typeof(SalesOrderJob));

        public SalesOrderJob()
        {
            salesOrderApp = new SalesOrderApp(salesOrderRepository);
        }

        /// <summary>
        /// Called by the <see cref="IScheduler" /> when a <see cref="ITrigger" />
        /// fires that is associated with the <see cref="IJob" />.
        /// </summary>
        /// <remarks>
        /// The implementation may wish to set a  result object on the 
        /// JobExecutionContext before this method exits.  The result itself
        /// is meaningless to Quartz, but may be informative to 
        /// <see cref="IJobListener" />s or 
        /// <see cref="ITriggerListener" />s that are watching the job's 
        /// execution.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        public void Execute(IJobExecutionContext context)
        {
            logger.Info("SalesOrder Job running...");

            //Do something
            Thread.Sleep(TimeSpan.FromSeconds(5));

            logger.Info("SalesOrder Job finished.");
        }


      
    }
}
