using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedManagement.MiddleBaseAPI.Providers
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 16:50:21
    *	accecc_token格式
	===============================================================================================================================*/
    public class MidSecureDataFormat: ISecureDataFormat<AuthenticationTicket>
    {
        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException();
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}