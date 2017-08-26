using IntegratedManageMent.Application.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.SalesModule
{
    public class SalesReturnsController : ApiController
    {

        private readonly ISalesReturnApp _ISalesReturnApp;
        public SalesReturnsController(ISalesReturnApp ISalesReturnApp)
        {
            _ISalesReturnApp = ISalesReturnApp;
        }
    }
}
