using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 17:50:43
    *	
	===============================================================================================================================*/
    public class AccessToken
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string refresh_token { get; set; }

        public string expires_in { get; set; }

        public string scope { get; set; }
    }
}
