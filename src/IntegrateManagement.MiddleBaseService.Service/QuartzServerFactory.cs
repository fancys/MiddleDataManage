using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.Service
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/26 17:16:27
	===============================================================================================================================*/
   public class QuartzServerFactory
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(QuartzServerFactory));

        /// <summary>
        /// Creates a new instance of an Quartz.NET server core.
        /// </summary>
        /// <returns></returns>
        public static QuartzServer CreateServer()
        {
            string typeName = Configuration.ServerImplementationType;

            Type t = Type.GetType(typeName, true);

            logger.Debug("Creating new instance of server type '" + typeName + "'");
            QuartzServer retValue = new QuartzServer();// (QuartzServer)Activator.CreateInstance(t);
            logger.Debug("Instance successfully created");
            return retValue;
        }
    }
}
